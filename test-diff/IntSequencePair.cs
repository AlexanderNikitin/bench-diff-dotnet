using System;
namespace test_diff
{
    public class IntSequencePair:AbstractSequencePair<int[]>
    {
        public IntSequencePair(int[] s1, int[] s2):base(s1, s2)
        {
        }

        public override bool Equal(int index1, int index2)
        {
            unchecked {
                return s1[index1] == s2[index2];
            }
        }

        protected override int GetLength(int[] s)
        {
            return s.Length;
        }
    }
}

