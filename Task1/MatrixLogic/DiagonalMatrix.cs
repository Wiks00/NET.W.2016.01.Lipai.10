using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class DiagonalMatrix<T> : AbstractMatrix<T>, ICloneable where T : struct
    {
        public override T this[int i, int j]
        {
            get
            {
                if (i < 0 && i >= Size && j < 0 && j >= Size)
                    throw new ArgumentOutOfRangeException();

                return structualStrorage[i, j];
            }
            set
            {
                if (i < 0 && i >= Size && i != j)
                    throw new ArgumentException();

                SetDiagonal(value);

                NotifyPropertyChanged(this, new MatrixEventArgs<T>(i, i, value));
            }
        }

        public DiagonalMatrix()
        {
            structualStrorage = new T[emptyMatrixSize, emptyMatrixSize];
        }

        public DiagonalMatrix(int length) : this(default(T), length)
        {
        }

        public DiagonalMatrix(T value, int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException();

            structualStrorage = new T[length, length];
            SetDiagonal(value);

        }

        public void SetDiagonal(T value)
        {
            for (int i = 0; i < Size; i++)
            {
                structualStrorage[i, i] = value;
            }

        }

        public DiagonalMatrix<T> Clone()
        {
            return new DiagonalMatrix<T>(this[0, 0], Size);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
