using System.Drawing;
using System.Linq;

namespace ConsoleForms.Layouts {
    public class VerticalLayout : BaseLayout {
        protected override void BeforeAdding(Entity component) {
            component.Location = new Point(
                component.Location.X, 
                component.Location.Y + 
                components
                    .Sum(x => x.GetHeight()));
        }

        protected override void AfterAdding(Entity component) {
            
        }

        public override int GetWidth() {
            return components
                .Max(x => x.GetWidth());
        }

        public override int GetHeight() {
            return components
                .Sum(x => x.GetHeight());
        }
    }
}