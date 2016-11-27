using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public sealed class MatrixEventArgs<T>
    {
        public int I { get; private set; }
        public int J { get; private set; }
        public T Value { get; private set; }

        public MatrixEventArgs(int i, int j, T value)
        {
            I = i;
            J = j;
            Value = value;
        }
    }
}