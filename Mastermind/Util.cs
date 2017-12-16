using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MindMasters2 {
    public static class Util {



    }

    public struct ConsolePalette {
        public ConsoleColor ForeColor { get; }
        public ConsoleColor BackColor { get; }

        public ConsolePalette(ConsoleColor front, ConsoleColor back) {
            ForeColor = front;
            BackColor = back;
        }

        public static ConsolePalette GetCurrent() {
            return new ConsolePalette(Console.ForegroundColor, Console.BackgroundColor);
        }

        public static ConsolePalette GetDefault() {
            return ConsoleHelper.GetDefaultPalette();
        }

        public void Apply() {
            Console.ForegroundColor = ForeColor;
            Console.BackgroundColor = BackColor;
        }

    }

    public struct Rectangle {
        public int LocationX { get { return Location.X; } }
        public int LocationY { get { return Location.Y; } }
        public int Width { get { return Size.Width; } }
        public int Height { get { return Size.Height; } }

        public Point Location { get; }
        public Size Size { get; }

        public Rectangle(int width, int height) {
            Location = new Point();
            Size = new Size(width, height);
        }

        public Rectangle(int width, int height, int locX, int locY) {
            Location = new Point(locX, locY);
            Size = new Size(width, height);
        }

        public static bool operator ==(Rectangle right, Rectangle left) {
            return right.Location == left.Location && right.Size == left.Size;
        }

        public static bool operator !=(Rectangle right, Rectangle left) {
            return !(right == left);
        }

        public override bool Equals(object obj) {
            return obj is Rectangle && this == (Rectangle)obj;
        }

        public override int GetHashCode() {
            return unchecked(Location.GetHashCode() ^ Size.GetHashCode());
        }
    }

    public struct Point {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y) {
            X = x;
            Y = y;
        }

        public Point Offset(Point diff) {
            return Offset(diff.X, diff.Y);
        }

        public Point Offset(int x, int y) {
            return new Point(X + x, Y + y);
        }

        public override bool Equals(object obj) {
            return obj is Point && this == (Point)obj;
        }

        public override int GetHashCode() {
            return unchecked(X ^ Y);
        }

        public static bool operator ==(Point left, Point right) {
            return left.X == right.X && left.Y == left.Y;
        }

        public static bool operator !=(Point left, Point right) {
            return !(left == right);
        }
    }

    public struct Size {
        public int Width { get; }
        public int Height { get; }

        public Size(int width, int height) {
            Width = width;
            Height = height;
        }

        public override bool Equals(object obj) {
            return obj is Size && this == (Size)obj;
        }

        public override int GetHashCode() {
            return unchecked(Width ^ Height);
        }

        public static bool operator ==(Size left, Size right) {
            return left.Width == right.Width && left.Height == left.Height;
        }

        public static bool operator !=(Size left, Size right) {
            return !(left == right);
        }

    }

}
