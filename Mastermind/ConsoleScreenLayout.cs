using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace MindMasters2 {
    public abstract class ConsoleScreenLayout {

        public virtual ObservableCollection<ConsoleLayoutComponent> Components { get; private set; } = new ObservableCollection<ConsoleLayoutComponent>();

        public event EventHandler<PreviewKeyEventArgs> PreviewKeyPressed;
        public event EventHandler<KeyEventArgs> KeyPressed;

        public int KeyLoopTimeout { get; set; } = 50;

        public ConsoleScreenLayout() {
            Components.CollectionChanged += (o, e) => Redraw();
            PreviewKeyPressed += OnPreviewKeyPressed;
            KeyPressed += OnKeyPressed;
        }

        public abstract void RunScreen();

        public virtual void StartKeyLoop() {

            while (true) {
                if (Console.KeyAvailable) {
                    var key = Console.ReadKey(true);
                    var eventArgs = new PreviewKeyEventArgs(key);

                    PreviewKeyPressed(this, eventArgs);

                    if (eventArgs.DisplayKey && key.KeyChar != default(char)) Console.Write(key.KeyChar);
                    if (!eventArgs.Handled) KeyPressed(this, new KeyEventArgs(key));
                } else {
                    Thread.Sleep(KeyLoopTimeout);
                }
            }
        }

        public virtual void Redraw() {
            foreach (var item in Components) {
                item.Refresh();
            }
        }

        public virtual void AddComponent(ConsoleLayoutComponent comp) {
            Components.Add(comp);
        }

        public virtual void OnKeyPressed(object sender, KeyEventArgs e) { }
        public virtual void OnPreviewKeyPressed(object sender, PreviewKeyEventArgs e) { }

    }

    public class KeyEventArgs : EventArgs {
        public ConsoleKeyInfo KeyPress { get; }

        public KeyEventArgs(ConsoleKeyInfo keyCombo) {
            KeyPress = keyCombo;
        }
    }

    public class PreviewKeyEventArgs : EventArgs {
        public ConsoleKeyInfo KeyPress { get; }
        public bool Handled { get; set; }
        public bool DisplayKey { get; set; } = true;

        public PreviewKeyEventArgs(ConsoleKeyInfo keyCombo) {
            KeyPress = keyCombo;
        }
    }

}

