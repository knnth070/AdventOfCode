var sw = System.Diagnostics.Stopwatch.StartNew();
int b, d, h;
bool f;
for (b = 109900; b <= 126900; b += 17)
{
    f = false;
    for (d = 2; d != b; d++)
    {
        if (b % d == 0)
        {
            f = true;
            break;
        }
    }
    if (f) h++;
}
sw.Stop();
Console.WriteLine($"h = {h}");
Console.WriteLine($"Elapsed = {sw.ElapsedMilliseconds} ms");