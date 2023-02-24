using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace test_diff
{
    public interface SequencePair {
        bool Equal(int index1, int index2);

        int Length1 { get; }

        int Length2 { get; }
    }
}

