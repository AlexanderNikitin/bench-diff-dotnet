using System;
namespace test_diff
{
    public abstract class AbstractSequencePair<T>:SequencePair{
        protected T s1;
        protected T s2;


        public AbstractSequencePair(T s1, T s2)
        {
            this.s1 = s1;
            this.s2 = s2;
        }

        public int Length1 => GetLength(s1);

        public int Length2 => GetLength(s2);

        public abstract bool Equal(int index1, int index2);
        protected abstract int GetLength(T s);
    }
}

