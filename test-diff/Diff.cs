namespace test_diff {
    public class Diff<T> : IDiff<T> where T : struct, IEquatable<T> {
        private static readonly CacheHolder NULL = new(0, Side.NONE, null);

        public Diff() {
        }

        public int[][] Compare(SequencePair sequencePair) {
            unchecked {
                int n = sequencePair.Length1 + 1;
                int m = sequencePair.Length2 + 1;

                CacheHolder[][] cache = {
                new CacheHolder[m],
                new CacheHolder[m]
            };
                Array.Fill(cache[0], NULL);
                cache[1][0] = NULL;

                for (int i = 1; i < n; i++) {
                    CacheHolder[] prevCacheLine = cache[(i + 1) % 2];
                    CacheHolder[] currentCacheLine = cache[i % 2];

                    for (int j = 1; j < m; j++) {
                        int prevJ = j - 1;

                        if (sequencePair.Equal(i - 1, prevJ)) {
                            CacheHolder prevIPrevJ = prevCacheLine[prevJ];
                            currentCacheLine[j] = new CacheHolder(prevIPrevJ.Count + 1, Side.C, prevIPrevJ);
                        } else {
                            CacheHolder prevICurJ = prevCacheLine[j];
                            CacheHolder curIPrevJ = currentCacheLine[prevJ];

                            int a = prevICurJ.Count;
                            int b = curIPrevJ.Count;
                            if (a == b && i < j || a > b) {
                                currentCacheLine[j] = new CacheHolder(a, Side.A, prevICurJ);
                            } else {
                                currentCacheLine[j] = new CacheHolder(b, Side.B, curIPrevJ);
                            }
                        }
                    }
                }

                int[][] result = new int[Math.Min(sequencePair.Length1, sequencePair.Length2)][];
                int index = result.Length;
                CacheHolder? cur = cache[sequencePair.Length1 % 2][sequencePair.Length2];
                for (int i = sequencePair.Length1, j = sequencePair.Length2;
                     i > 0 && j > 0;) {
                    switch (cur?.Side) {
                        case Side.C:
                            result[--index] = new int[] { --i, --j };
                            break;
                        case Side.A:
                            i--;
                            break;
                        case Side.B:
                            j--;
                            break;
                    }
                    cur = cur?.Prev;
                }
                if (index > 0) {
                    int[][] temp = new int[result.Length - index][];
                    Array.Copy(result, index, temp, 0, result.Length - index);
                    result = temp;
                }
                return result;
            }
        }

        private static class Side {
            public const int A = 1;
            public const int B = 2;
            public const int C = 3;
            public const int NONE = 140;
        }

        private record CacheHolder(int Count, int Side, CacheHolder? Prev) {
        }
    }
}