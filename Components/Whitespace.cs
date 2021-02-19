using System;
using System.Text;

namespace ConsoleForms {
    public sealed class Whitespace : Component {
        public Whitespace(int width, int height) 
            : base(CreateWhiteSpace(width, height)) { }

        private static string CreateWhiteSpace(int width, int height) {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < height; i++)
                builder.AppendLine(new String(' ', width));
            return builder.ToString();
        }
    }
}