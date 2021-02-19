using System.Drawing;
using System.Linq;

namespace ConsoleForms.Layouts {
    public class HorisontalLayout : BaseLayout {
        
        
        protected override void BeforeAdding(Entity component) {
            component.Location = new Point(
                component.Location.X + 
                        components
                        .Sum(x => x.GetWidth()), 
                component.Location.Y);
        }
        protected override void AfterAdding(Entity component) {
            
        }
        
        public override int GetWidth() {
            return components
                .Sum(x => x.GetWidth());
        }

        public override int GetHeight() {
            return components
                .Max(x => x.GetHeight());
        }
    }
}