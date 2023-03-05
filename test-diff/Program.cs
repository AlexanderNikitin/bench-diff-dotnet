using System.Diagnostics;
using test_diff;

Random random = new Random();
int[][]? values = null;
Stopwatch stopWatch = Stopwatch.StartNew();
foreach(var kvp in dataGen(10000, 100, 20))
{
    IDiff diff = new Diff();
    int[][] temp = diff.Compare(new IntSequencePair(kvp.Value, kvp.Key));
    if (values == null || temp.Length > values.Length)
    {
        values = temp;
    }
}
stopWatch.Stop();
Console.WriteLine(values?.Length);
Console.WriteLine(stopWatch.ElapsedMilliseconds);
Console.ReadKey();


IEnumerable<KeyValuePair<int[], int[]>> dataGen(int length, int ratio, int iterations)
{
    IList<KeyValuePair<int[], int[]>> result = new List<KeyValuePair<int[], int[]>>();
    int maxRandom = length / ratio;
    for(int i = 0; i < iterations; i++)
    {
        int[] s1 = new int[length];
        int[] s2 = new int[length];
        result.Add(new KeyValuePair<int[], int[]>(s1, s2));
        for(int j = 0; j < length;j++)
        {
            s1[j] = random.Next(maxRandom);
            s2[j] = random.Next(maxRandom);
        }
    }
    return result;
}