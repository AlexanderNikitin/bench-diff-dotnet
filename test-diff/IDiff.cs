using System;
namespace test_diff
{
    public interface IDiff<T> where T : struct, IEquatable<T> {
        int[][] Compare(SequencePair sequencePair);
    }
}

