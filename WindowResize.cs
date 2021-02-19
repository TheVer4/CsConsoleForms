using System;
using System.Threading;

namespace ConsoleForms {
    
    public delegate void OnSizeChange(object o, WindowResize.WindowResizeEventArgs a);
    public static class WindowResize {
        private static bool isEnabled;
        private static Thread listner;
        public static OnSizeChange OnSizeChange;

        static WindowResize() {
            listner = new Thread(EventListnerWork);
            OnSizeChange = new OnSizeChange((o, args) => {});

            listner.Start();
        }

        public static bool IsEnabled() {
            return isEnabled;
        }

        //TODO Create multicast event handler
        
        
        static void ConsoleResizeEvent(int height, int width) {
            OnSizeChange.Invoke(null, new WindowResizeEventArgs(width, height));
        }

        static void EventListnerWork()
        {
            isEnabled = true;
            int height = Console.WindowHeight;
            int width = Console.WindowWidth;
            while (isEnabled)
            {
                if (height != Console.WindowHeight || width != Console.WindowWidth)
                {
                    height = Console.WindowHeight;
                    width = Console.WindowWidth;
                    ConsoleResizeEvent(height,width);
                }

                Thread.Sleep(10); 
            }

        }

        public class WindowResizeEventArgs : EventArgs {
            public int ConsoleWidth { get; } 
            public int ConsoleHeight { get; }


            public WindowResizeEventArgs(int consoleWidth, int consoleHeight) {
                ConsoleWidth = consoleWidth;
                ConsoleHeight = consoleHeight;
            }
        }
    }
}