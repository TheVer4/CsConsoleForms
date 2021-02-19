using System.Text;

namespace ConsoleForms {
    public sealed class TextBox : Component {
        
        public TextBox(params string[] lines) : base(FormatText(lines)) { }

        private static string FormatText(params string[] lines) {
            StringBuilder textBuilder = new StringBuilder();
            foreach (string line in lines)
                textBuilder.AppendLine(line);
            return textBuilder.ToString();
        }
    }
}