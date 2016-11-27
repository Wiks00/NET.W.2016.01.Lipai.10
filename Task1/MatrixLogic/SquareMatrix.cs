using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class SquareMatrix<T> : AbstractMatrix<T>, ICloneable where T : struct
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
                if (i < 0 && i >= Size && j < 0 && j >= Size)
                    throw new ArgumentOutOfRangeException();

                structualStrorage[i, j] = value;
                NotifyPropertyChanged(this, new MatrixEventArgs<T>(i, j, value));
            }
        }

        public SquareMatrix()
        {
            structualStrorage = new T[emptyMatrixSize, emptyMatrixSize];
        }

        public SquareMatrix(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException();
            structualStrorage = new T[length, length];
        }

        public SquareMatrix(params T[] values) : this(values.AsEnumerable())
        {
        }

        public SquareMatrix(T[,] values) : this(values.Cast<T>())
        {
        }

        public SquareMatrix(IEnumerable<T> collection)
        {
            if (ReferenceEquals(collection, null))
                throw new ArgumentNullException();

            if (!CheckForСompatibility(collection))
                throw new ArgumentException();

            int size = (int) Math.Sqrt(collection.Count());

            structualStrorage = new T[size, size];

            int i = 0;
            int j = 0;

            foreach (var value in collection)
            {
                if (j == Size)
                {
                    i++;
                    j = 0;
                    if (i == Size)
                    {
                        j = 0;
                        i = 0;
                    }
                }

                structualStrorage[i, j] = value;

                j++;
            }
        }

        public SquareMatrix<T> Clone()
        {
            return new SquareMatrix<T>(structualStrorage);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}