using Wall_e.api;

namespace Wall_e.infra;

public class ConsoleIo : IO
{
    public void DoWrite(string text)
    {
        Console.WriteLine(text);
    }

    public string? DoRead()
    {
        return Console.ReadLine();
    }
}