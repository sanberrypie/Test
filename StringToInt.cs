using System.Diagnostics;

int T = 100000000;
int R = 1;


Random random = new Random();
string[] testCases = new string[T];

for (int i = 0; i < T; i++)
{
    testCases[i] = random.Next(int.MinValue, int.MaxValue).ToString();
}

Stopwatch sw = new Stopwatch();
for (int i = 0; i < R; i++)
{
    DoTask(() =>
    {
        StringToInt(testCases[i]);
    }, "Custom");

    DoTask(() =>
    {
        int.Parse(testCases[i]);
    }, "Int.Parse");

    DoTask(() =>
    {
        Convert.ToInt32(testCases[i]);
    }, "Convert.ToInt32");

    DoTask(() =>
    {
        int.TryParse(testCases[i], out int result);
    }, "int.TryParse");
}


int StringToInt(string str)
{
    int result = 0;

    if (str[0] != '-')
        for (int i = 0; i < str.Length; i++)
            result = result * 10 + (str[i] - '0');
    else
    {
        for (int i = 1; i < str.Length; i++)
            result = result * 10 + (str[i] - '0');
        result *= -1;
    }

    return result;
}

void DoTask(Action action, string name)
{
    sw.Restart();
    for (int i = 0; i < T; i++)
    {
        action();
    }
    sw.Stop();
    Console.WriteLine($"{name}: {sw.Elapsed.TotalSeconds}s");
}
