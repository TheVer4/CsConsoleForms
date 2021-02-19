using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleForms {
    public sealed class Spinner<T> : Component, IControllable where T : IRotatable {
        private List<T> data;
        private int contentWidth;
        private int choosen;

        public Spinner(string text) : base(text) {
            data = new List<T>();
        }

        public void PutAssortment(List<T> rotatables) {
            this.data = rotatables;
            this.contentWidth = rotatables.Select(x => x.ToString()).Max(x => x.Length);
        }
        
        private void ShowChoosen() {
            if(data.Count == 0) return;
            int startX = Location.X + GetWidth() - contentWidth;
            Console.SetCursorPosition(startX, Location.Y);
            Console.Write(new String(' ', contentWidth));
            Console.CursorLeft = startX;
            bool cursorState = Console.CursorVisible;
            Console.CursorVisible = false;
            Console.Write(data[choosen].ToStringRepresentation());
            data[choosen].OnChoose();
            Console.CursorVisible = cursorState;
        }

        public T GetChoosen() {
            if(data.Count == 0) return default;
            return data[choosen];
        }
        
        public void OnUpPressed() { }

        public void OnDownPressed() { }

        public void OnLeftPressed() {
            int max = data.Count - 1;
            if (choosen > 0)
                choosen--;
            else
                choosen = max;
            ShowChoosen();
        }

        public void OnRightPressed() {
            int max = data.Count - 1;
            if (choosen < max)
                choosen++;
            else
                choosen = 0;
            ShowChoosen();
        }

        public void OnEnterPressed() {
            ConsoleKeyInfo cki;
            do {
                cki = Console.ReadKey(false);
                switch (cki.Key) {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        this.OnLeftPressed();
                        break;
                    case ConsoleKey.RightArrow: 
                    case ConsoleKey.D:
                        this.OnRightPressed();
                        break;
                    default:
                        continue;
                }
            } while (cki.Key != ConsoleKey.Escape);
            OnEscapePressed();
        }

        public void OnEscapePressed() { }
        public override int GetWidth() {
            return base.GetWidth() + contentWidth;
        }
    }
}