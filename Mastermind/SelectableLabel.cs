using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMasters2 {
    public class SelectableLabel : ConsoleLayoutComponent {

        string _text;
        public string Text {
            get { return _text; }
            set {
                _text = value;
                Refresh();
            }
        }

        bool _selected;
        public bool Selected {
            get { return _selected; }
            set {
                _selected = value;
                Refresh();
            }
        }

        bool _enabled = true;
        public bool Enabled {
            get { return _enabled; }
            set {
                _enabled = value;
                Refresh();
            }
        }

        ConsolePalette _selectedColor = ConsolePalette.GetDefault();
        public ConsolePalette SelectedColors {
            get { return _selectedColor; }
            set {
                _selectedColor = value;
                Refresh();
            }
        }

        ConsolePalette _disabledColor = ConsolePalette.GetDefault();
        public ConsolePalette DisabledColors {
            get { return _disabledColor; }
            set {
                _disabledColor = value;
                Refresh();
            }
        }


        public SelectableLabel(string text, Rectangle bounds, ConsolePalette normal, ConsolePalette selected) : base(bounds, normal) {
            _text = text;
            _selectedColor = selected;
            Refresh();
        }

        public override ConsolePalette GetCurrentPalette() {
            if (!Enabled) return DisabledColors;
            if (Selected) return SelectedColors;
            return Colors;
        }

        public override void WriteSurface() {
            Console.SetCursorPosition(Bounds.LocationX, Bounds.LocationY);
            Console.WriteLine(Text);
        }
    }
}
