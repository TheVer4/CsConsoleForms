using System.Collections.Generic;
using System.Drawing;

namespace ConsoleForms.Layouts {
    public abstract class BaseLayout : Entity {
        
        protected readonly List<Entity> components;

        protected BaseLayout() {
            this.components = new List<Entity>();
        }

        public void Add(Entity component) {
            BeforeAdding(component);
            component.SetParentLayout(this);
            this.components.Add(component);
            AfterAdding(component);
        }

        protected abstract void BeforeAdding(Entity component);
        protected abstract void AfterAdding(Entity component);

        public override void Draw() {
            foreach (var entity in components) {
                var component = (Component) entity;
                int x = component.Location.X + Location.X;
                int y = component.Location.Y + Location.Y;
                component.Location = new Point(x, y);
                component.Draw();
            }
        }

        public Entity this[int index] {
            get => components[index];
        }
    }
}