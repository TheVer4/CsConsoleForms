using System;
using System.Text;

namespace ConsoleForms {
    public sealed class TextInput : Component {
        private int inputWidth;

        public TextInput(string text, int inputWidth) : base(text) {
            this.inputWidth = inputWidth;
        }

        public override int GetWidth() {
            return base.GetWidth() + inputWidth + 1;
        }

        public string ReadUserInput() {
            bool cursorState = Console.CursorVisible;
            Console.CursorVisible = true;
            string result = ReadLine();
            Console.CursorVisible = cursorState;
            return result;
        }

        private string ReadLine() {
            int startX = Location.X + GetWidth() - inputWidth;
            Console.SetCursorPosition(startX, Location.Y);
            ConsoleKeyInfo cki;
            StringBuilder builder = new StringBuilder();
            do {
                cki = Console.ReadKey(true);
                Console.CursorLeft = startX;
                Console.Write(new String(' ', inputWidth));
                Console.CursorLeft = startX;
                char c = cki.KeyChar;
                if (cki.Key == ConsoleKey.Backspace && builder.Length > 0)
                    builder.Remove(builder.Length - 1, 1);
                else 
                    builder.Append(c);
                if(builder.Length < inputWidth)
                    Console.Write(builder.ToString());
                else 
                    Console.Write(builder
                        .ToString()
                        .Substring(builder.Length - inputWidth, inputWidth));
            } while (cki.Key != ConsoleKey.Enter);
            return builder.ToString();
        }
    }
}