using System;
using System.Drawing;
using System.Linq;

namespace ConsoleForms {
    public abstract class Component : Entity {
        public string Text { get; protected set; }

        public Component(string text) {
            Text = text;
        }

        public override int GetWidth() {
            return this.Text.Split('\n')
                .Select(x => x
                    .Replace("\n", "")
                    .Replace("\r", ""))
                .Max(x => x.Length);
        }

        public override int GetHeight() {
            if (Text.Contains('\n'))
                return this.Text.Count(x => x == '\n');
            return 1;
        }

        public override void Draw() {
            if(CheckScreenSpace())
                return; //TODO Caution! Very big components will not be drawn
            int i = 0;
            foreach (string line in Text.Split('\n').Where(x => !string.IsNullOrEmpty(x))) {
                Console.SetCursorPosition(Location.X, Location.Y + i++);
                Console.Write(line
                    .Replace("\r", "")
                    .Replace("\n", ""));
                
            }
        }

        private bool CheckScreenSpace() {
            return GetWidth() + Location.X > Console.WindowWidth;
        }

        public override string ToString() {
            return Text;
        }
    }
}