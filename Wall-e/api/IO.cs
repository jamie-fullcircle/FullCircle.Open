using Wall_e.domain;

namespace Wall_e.api;

public interface IO
{
    protected void DoWrite(string text);
    protected string? DoRead();

    public void WriteLine(string text)
    {
        DoWrite($"{DateTime.Now:HH:mm:ss} | {text}");
    }

    public Program.EnCommand? ReadCommand()
    {
        var rawNormalised = DoRead()?.ToLowerInvariant();

        switch (rawNormalised)
        {
            case "hello": return Program.EnCommand.Hello;
            case "paint": return Program.EnCommand.Paint;
            default: return null;
        }
    }
}