using System;

namespace ConsoleForms {
    public sealed class Option : Component {
        public Action onClick { get; }
        public bool IsSelected { get; set; }
        
        public Option(string text, Action onClick) : base(text) {
            this.onClick = onClick;
        }
        public override void Draw() {
            if (!IsSelected) {
                base.Draw();
            }
            else {
                Console.BackgroundColor = Properties.selectedBackground;
                Console.ForegroundColor = Properties.selectedForeground;
                base.Draw();
                Console.BackgroundColor = Properties.defaultBackground;
                Console.ForegroundColor = Properties.defaultForeground;
            }
        }
    }
}