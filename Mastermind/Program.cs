using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Reflection;

namespace MindMasters2 {
    class Program {

        static void Main(string[] args) {
            
            ConsoleHelper.StartLayout(new WelcomeScreen());
        }
    }
}
