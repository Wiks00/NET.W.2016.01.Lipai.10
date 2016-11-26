using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class SymmetricMatrix<T> : AbstractMatrix<T>, ICloneable where T : struct
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
                if (i < 0 && i >= Size && i < 0 && i >= Size)
                    throw new ArgumentException();

                if (i == j)
                    structualStrorage[i, j] = value;
                else
                    SetSymmetricValue(i, j, value);

                NotifyPropertyChanged(this, new MatrixEventArgs<T>(i, j, value));
            }
        }

        public SymmetricMatrix()
        {
            structualStrorage = new T[emptyMatrixSize, emptyMatrixSize];
        }

        public SymmetricMatrix(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException();
            structualStrorage = new T[length, length];
        }

        public SymmetricMatrix(params T[] values) : this(values.AsEnumerable())
        {
        }

        public SymmetricMatrix(T[,] values) : this(values.Cast<T>())
        {
        }

        public SymmetricMatrix(IEnumerable<T> collection)
        {
            if (ReferenceEquals(collection, null))
                throw new ArgumentNullException();

            double collectionSize = (1 + Math.Sqrt(1 - 4 * (-collection.Count() * 2))) / 2;
            int truncate = (int)Math.Floor(collectionSize);
            int fract = (int)Math.Ceiling(collectionSize - truncate);

            if (fract != 0)
                throw new ArgumentException();

            int jLength = 1;

            structualStrorage = new T[truncate, truncate];

            var enumirator = collection.GetEnumerator();
            T item = default(T);

            for (int i = 1; i < Size; i++)
            {
                for (int j = 0; j < jLength;)
                {
                    if (enumirator.MoveNext())
                        item = enumirator.Current;

                    SetSymmetricValue(i, j, item);
                    j++;
                    if (j == jLength)
                    {
                        j = 0;
                        jLength++;
                        break;
                    }
                }
            }
        }

        public void SetDiagonal(IEnumerable<T> collection, int startIndex)
        {
            if (startIndex >= Size || startIndex < 0 || collection.Count() > Size - startIndex)
                throw new ArgumentOutOfRangeException();

            int i = startIndex;
            foreach (var value in collection)
            {
                structualStrorage[i, i] = value;
                i++;
            }
        }

        public void SetDiagonal(IEnumerable<T> collection) => SetDiagonal(collection, 0);

        public void SetDiagonal(params T[] values) => SetDiagonal(values.AsEnumerable());

        public void SetDiagonal(T value)
        {
            for (int i = 0; i < Size; i++)
            {
                structualStrorage[i, i] = value;
            }

        }

        private void SetSymmetricValue(int i , int j ,T value)
        {
            structualStrorage[i, j] = value;
            structualStrorage[j, i] = value;
        }

        public SymmetricMatrix<T> Clone()
        {
            return new SymmetricMatrix<T>();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
