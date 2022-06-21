using System;

namespace Pizza_na_telefon
{
    public class Funtions
    {
        public void CustomConsoleWriteLine(string text, string  text_color = "white", bool is_centered = false)
        {
            switch (text_color)
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            
            if (is_centered)
            {
                Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine(text);
            }

            Console.ResetColor();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var functions = new Funtions();
            functions.CustomConsoleWriteLine("hi", "red");
            functions.CustomConsoleWriteLine("co tam", "", true);
        }
    }
}
