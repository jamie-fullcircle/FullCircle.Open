using PaintByNumbers.Paint;
using Wall_e.api;
using Wall_e.infra;

namespace Wall_e.domain;

public static class Program
{
    public enum EnCommand
    {
        Hello,
        Paint
    }

    private static readonly IO IO = new ConsoleIo();

    private static void Main(string[] args)
    {
        while (true)
        {
            var command = IO.ReadCommand();

            switch (command)
            {
                case EnCommand.Hello:
                    IO.WriteLine("Hello, World!");
                    break;
                case EnCommand.Paint:
                    Util.GetBitmap(new FileInfo("static/jamie.jpg"));
                    break;
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}