using System;
using System.Collections.Generic;
using System.Drawing;
using ConsoleForms.Layouts;

namespace ConsoleForms {
    public class Activity {
        
        public HorisontalAlignment horisontal { get; set; } = HorisontalAlignment.Center;
        public VerticalAlignment vertical { get; set; } = VerticalAlignment.Center;
        private BaseLayout Layout;
        
        public Activity(BaseLayout layout) {
            Layout = layout;
        }

        public void Draw() { 
            Layout.Location = new Point(GetXCoord(), GetYCoord());
            Layout.Draw();
        }

        public int GetWidth() {
            return Console.WindowWidth;
        }

        public int GetHeight() {
            return Console.WindowHeight;
        }

        private int GetXCoord() {
            int coord = 0;
            switch (vertical) {
                case VerticalAlignment.Left:
                    coord = 0;
                    break;
                case VerticalAlignment.Right:
                    coord = GetWidth() - Layout.GetWidth();
                    break;
                case VerticalAlignment.Center:
                    coord = (GetWidth() - Layout.GetWidth()) / 2;
                    break;
            }
            return coord;
        }
        
        private int GetYCoord() {
            int coord = 0;
            switch (horisontal) {
                case HorisontalAlignment.Top:
                    coord = 0;
                    break;
                case HorisontalAlignment.Bottom:
                    coord = GetHeight() - Layout.GetHeight();
                    break;
                case HorisontalAlignment.Center:
                    coord = (GetHeight() - Layout.GetHeight()) / 2;
                    break;
            }
            return coord;
        }
        
        public enum VerticalAlignment {
            Left,
            Right,
            Center
        }
        
        public enum HorisontalAlignment {
            Top,
            Bottom,
            Center
        }
    }
}