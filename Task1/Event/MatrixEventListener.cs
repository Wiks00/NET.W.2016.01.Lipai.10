using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Event
{
    public class MatrixEventListener
    {
        public void Register<T>(AbstractMatrix<T> matrix) where T : struct
        {
            matrix.PropertyChanged += Message;
        }

        public void Unregister<T>(AbstractMatrix<T> matrix) where T : struct
        {
            matrix.PropertyChanged -= Message;
        }

        private static void Message<T>(object sender, MatrixEventArgs<T> eventArgs)
        {
            Console.WriteLine("Value in {1},{2} index changed to: {0}", eventArgs.Value, eventArgs.I, eventArgs.J);
        }
    }
}