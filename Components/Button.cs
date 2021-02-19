using System;
using System.Text;

namespace ConsoleForms {
    public sealed class Button : Component {
        public Action onClick { get; }
        public bool IsSelected { get; set; }
        
        public Button(string text, Action onClick) : base(CreateButton(text)) {
            this.onClick = onClick;
        }
        private static string CreateButton(string text) {
            String hr = new String('═', text.Length + 2);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"╔{hr}╗");
            builder.AppendLine($"║ {text} ║");
            builder.AppendLine($"╚{hr}╝");
            return builder.ToString();
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