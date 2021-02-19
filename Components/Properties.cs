using System;

namespace ConsoleForms {
    public static class Properties {
        public static ConsoleColor defaultBackground { get; set; }
        public static ConsoleColor selectedBackground { get; set; }

        public static ConsoleColor defaultForeground { get; set; } = ConsoleColor.White;

        public static ConsoleColor selectedForeground { get; set; } = ConsoleColor.Red;
    }
}