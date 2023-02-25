using System.Diagnostics;
using test_diff;



const int LENGTH = 10000;
int[] a1 = new int[LENGTH];
int[] a2 = new int[LENGTH];
Random random = new Random();
int[][]? values = null;
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();
for (int i = 0; i < 20; i++) {
    for (int j = 0; j < LENGTH; j++) {
        a1[j] = random.Next(LENGTH / 100);
        a2[j] = random.Next(LENGTH / 100);
    }
    IDiff<int> diff = new Diff<int>();
    int[][] temp = diff.Compare(new IntSequencePair(a1, a2));
    if (values == null || temp.Length > values.Length) {
        values = temp;
    }
}
stopWatch.Stop();
Console.WriteLine(values?.Length);
Console.WriteLine(stopWatch.ElapsedMilliseconds);