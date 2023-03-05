namespace test_diff {
    public class Diff : IDiff {
        private static readonly CacheHolder NULL = new(0, 0, null);
        public const int A = 1;
        public const int B = 2;
        public const int C = 3;

        public Diff() {
        }

        public unsafe int[][] Compare(SequencePair sequencePair) {
            int n = sequencePair.Length1 + 1;
            int m = sequencePair.Length2 + 1;

            CacheHolder[] prevCacheLine = new CacheHolder[m];
            CacheHolder[] currentCacheLine = new CacheHolder[m];

            CacheHolder[][] cache = {
                    prevCacheLine,
                    currentCacheLine
                };
            Array.Fill(cache[0], NULL);
            cache[1][0] = NULL;

            for (int i = 1, prevI = 0; i < n; prevI = i++) {
                CacheHolder curIPrevJ = currentCacheLine[0];
                for (int j = 1, prevJ = 0; j < m; prevJ = j++) {
                    CacheHolder res;
                    if (sequencePair.Equal(prevI, prevJ)) {
                        CacheHolder prevIPrevJ = prevCacheLine[prevJ];
                        res = new CacheHolder(prevIPrevJ.Count + 1, C, prevIPrevJ);
                    } else {
                        CacheHolder prevICurJ = prevCacheLine[j];
                        int a = prevICurJ.Count;
                        int b = curIPrevJ.Count;
                        res = a > b || a == b && i < j ?
                                new CacheHolder(a, A, prevICurJ) :
                                new CacheHolder(b, B, curIPrevJ);
                    }

                    currentCacheLine[j] = res;
                    curIPrevJ = res;
                }

                CacheHolder[] temp = prevCacheLine;
                prevCacheLine = currentCacheLine;
                currentCacheLine = temp;
            }

            int[][] result = new int[Math.Min(sequencePair.Length1, sequencePair.Length2)][];
            int index = result.Length;
            CacheHolder cur = cache[sequencePair.Length1 % 2][sequencePair.Length2];
            for (int i = sequencePair.Length1, j = sequencePair.Length2;
                 i > 0 && j > 0;) {
                switch (cur.Side) {
                    case C:
                        result[--index] = new int[] { --i, --j };
                        break;
                    case A:
                        i--;
                        break;
                    case B:
                        j--;
                        break;
                }
                cur = cur.Prev;
            }
            if (index > 0) {
                int[][] temp = new int[result.Length - index][];
                Array.Copy(result, index, temp, 0, result.Length - index);
                result = temp;
            }
            return result;
        }
        private record CacheHolder(int Count, int Side, CacheHolder? Prev) {
        }
    }
}