using System.Drawing;

namespace ConsoleForms {
    public abstract class Entity {
        public Point Location { get; set; }

        protected Entity Parent { get; private set; }
        
        protected internal virtual void SetParentLayout(Entity layout) {
            this.Parent = layout;
        }
        public abstract void Draw();
        public abstract int GetWidth();
        public abstract int GetHeight();
    }
}