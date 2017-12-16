using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MindMasters2 {
    public static class ConsoleHelper {

        static LinkedList<ConsoleScreenLayout> screenQueue = new LinkedList<ConsoleScreenLayout>();

        static bool _readDefPalette;
        static ConsolePalette _defPalette;

        public static void StartLayout(ConsoleScreenLayout layout) {
            Console.TreatControlCAsInput = true;
            layout.RunScreen();

            while (screenQueue.Count > 0) {
                var next = screenQueue.First.Value;
                screenQueue.RemoveFirst();
                next.RunScreen();
            }

        }

        public static void QueueLayout(ConsoleScreenLayout layout) {
            screenQueue.AddLast(layout);
        }

        public static void QueueNextLayout(ConsoleScreenLayout layout) {
            screenQueue.AddFirst(layout);
        }

        public static void WithColorScheme(ConsolePalette scheme, Action task) {
            var temp = ConsolePalette.GetCurrent();
            scheme.Apply();
            task();
            temp.Apply();
        }

        public static ConsolePalette GetCurrentPalette() {
            return new ConsolePalette(Console.ForegroundColor, Console.BackgroundColor);
        }

        public static bool IsKeyInfoCharInput(ConsoleKeyInfo info) {
            //Null chars are invalid
            if (info.KeyChar == default(char)) return false;
            //Anything with alt or control isn't consodered a letter
            if ((info.Modifiers & (ConsoleModifiers.Alt | ConsoleModifiers.Control)) != 0) return false;

            //Filtering individual keys

            //On a standard keyboard, this is true for everything between the numpad and the letter area
            if (info.Key < ConsoleKey.D0) {
                switch (info.Key) {
                    case ConsoleKey.Enter: //technically a character, but will require special handling
                    case ConsoleKey.Backspace:
                    case ConsoleKey.Delete:
                    case ConsoleKey.Tab:
                    case ConsoleKey.Spacebar:
                        return true;
                }
            }

            return true;
        }

        public static ConsolePalette GetDefaultPalette() {
            if (_readDefPalette) return _defPalette;

            //For whatever reason, System.Console stores the default console colors privately. I would rather use reflection
            //instead of Win32Native, so we're going with this

            //Calling GetBufferInfo makes sure the private values we need in Console are set. This method handles lazy evaluation used to populate some
            //private variables, so it's perfectly safe for us to call. We could also check _haveReadDefaultColors to see if we actually need to do this,
            //but we would end up using this anyway if that's false, so we're saving on reflection.
            //We're looking for the method with no parameters
            typeof(Console).GetMethod("GetBufferInfo", BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { }, null).Invoke(null, null);

            //The private field we need is now set, so we can check the value safely
            byte dwColors = (byte)typeof(Console).GetField("_defaultColors", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

            //Convert the value into 4 bit colors, then cache the value so we never have to do this again...
            _defPalette = new ConsolePalette((ConsoleColor)(dwColors & 15), (ConsoleColor)((dwColors & 240) >> 4));
            _readDefPalette = true;

            return _defPalette;
        }
    }
}
