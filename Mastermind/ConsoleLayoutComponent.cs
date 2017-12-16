using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MindMasters2 {
    public class ConsoleLayoutComponent {

        Rectangle _bounds;
        public Rectangle Bounds {
            get { return _bounds; }
            set {
                if (value == _bounds) return;
                Clear();
                _bounds = value;
                Refresh();
            }
        }

        ConsolePalette _colors = ConsolePalette.GetDefault();
        public ConsolePalette Colors {
            get { return _colors; }
            set {
                _colors = value;
                Refresh();
            }
        }

        public ConsoleLayoutComponent() {
            Refresh();
        }

        public ConsoleLayoutComponent(Rectangle bounds, ConsolePalette colors) {
            _colors = colors;
            _bounds = bounds;
            Refresh();
        }

        public ConsoleLayoutComponent(Rectangle bounds) {
            Bounds = bounds;
        }

        public virtual void WriteSurface() { }


        public virtual void Clear() {
            //My two options are doing it the slow and easy way or writing 200 lines of boilerplate with P/invoke
            //and pointers for a couple nanoseconds of improvement. Easy and slow it is.
            Point rightBottom = new Point(Bounds.LocationX + Bounds.Width, Bounds.LocationY + Bounds.Height);
            Console.SetCursorPosition(Bounds.LocationX, Bounds.LocationY);
            for (int Y = Bounds.LocationY; Y < rightBottom.Y; Y++) {
                for (int X = Bounds.LocationX; X < rightBottom.X; X++) {
                    Console.Write(' ');
                }
                Console.SetCursorPosition(Bounds.LocationX, Y + 1);
            }
        }

        public virtual ConsolePalette GetCurrentPalette() {
            return Colors;
        }

        public virtual void Refresh() {
            ConsoleHelper.WithColorScheme(GetCurrentPalette(), () => {
                Clear();
                WriteSurface();
            });
        }

    }
}
