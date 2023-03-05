using System;
namespace test_diff
{
    public interface IDiff {
        int[][] Compare(SequencePair sequencePair);
    }
}

