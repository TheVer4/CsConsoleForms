using System;

namespace ConsoleForms {
    public sealed class DefaultSpinnerElement : IRotatable {
        private string text;
        private Action action;
        
        public DefaultSpinnerElement(string text) {
            this.text = text;
        }
        
        public DefaultSpinnerElement(string text, Action action) {
            this.text = text;
            this.action = action;
        }

        public string ToStringRepresentation() {
            return this.text;
        }

        public void OnChoose() {
            action();
        }
    }
}