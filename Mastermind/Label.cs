using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMasters2 {
    public class Label : ConsoleLayoutComponent {

        bool _ignoreNewLines = true;
        public bool IgnoreNewLines {
            get { return _ignoreNewLines; }
            set {
                _ignoreNewLines = value;
                Refresh();
            }
        }

        string _text;
        public string Text {
            get { return _text; }
            set {
                _text = value;
                Refresh();
            }
        }

        public override void WriteSurface() {
            string[] words;

            if (IgnoreNewLines) {
                words = Text.Split(' ', '\n');
            } else {
                words = Text.Split(' ');
            }

            bool newline = false;
            Console.SetCursorPosition(Bounds.LocationX, Bounds.LocationY);
            for (int i = 0; i < words.Length; i++) {

            }


        }
    }
}
