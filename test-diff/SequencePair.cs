using System;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace test_diff
{
    public interface SequencePair
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        unsafe bool Equal(int index1, int index2);

        int Length1 { get; }

        int Length2 { get; }
    }
}

