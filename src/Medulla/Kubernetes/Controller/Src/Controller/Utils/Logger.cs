
using System;
namespace Utils;

public sealed class Logger {
    public static void Message(string msg) {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine(msg, Console.ForegroundColor);
        Console.ForegroundColor = currentColor;
    }


}
