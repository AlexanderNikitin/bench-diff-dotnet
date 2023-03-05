using System;
using System.Runtime.CompilerServices;

namespace test_diff
{
    public interface IDiff
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        unsafe int[][] Compare(SequencePair sequencePair);
    }
}

