using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MindMasters2 {
    public class WelcomeScreen : ConsoleScreenLayout {

        

        public List<SelectableLabel> labels = new List<SelectableLabel>();

        int _selectedIndex;
        int selectedIndex {
            get { return _selectedIndex; }
            set {
                labels[_selectedIndex].Selected = false;
                labels[value].Selected = true;
                _selectedIndex = value;
            }
        }

        public override void RunScreen() {
            StartKeyLoop();

            Console.CursorVisible = false;

            labels.Add(new SelectableLabel("W-> New Game", new Rectangle(5, 1, 0, 8), new ConsolePalette(ConsoleColor.Black, ConsoleColor.Blue), new ConsolePalette(ConsoleColor.Red, ConsoleColor.Blue)));
            labels.Add(new SelectableLabel("E-> Continue", new Rectangle(5, 1, 0, 9), new ConsolePalette(ConsoleColor.Black, ConsoleColor.Blue), new ConsolePalette(ConsoleColor.Red, ConsoleColor.Blue)));
            labels.Add(new SelectableLabel("X-> Settings", new Rectangle(5, 1, 0, 10), new ConsolePalette(ConsoleColor.Black, ConsoleColor.Blue), new ConsolePalette(ConsoleColor.Red, ConsoleColor.Blue)));
            labels.Add(new SelectableLabel("Q-> Exit", new Rectangle(5, 1, 0, 11), new ConsolePalette(ConsoleColor.Black, ConsoleColor.Blue), new ConsolePalette(ConsoleColor.Red, ConsoleColor.Blue)));

            labels.ForEach((lbl) => Components.Add(lbl));

            labels[0].Selected = true;
            selectedIndex = 0;

        }
    }
}
