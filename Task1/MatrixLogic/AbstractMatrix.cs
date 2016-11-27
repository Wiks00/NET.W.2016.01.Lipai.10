using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public abstract class AbstractMatrix<T> : IEquatable<AbstractMatrix<T>>, IEnumerable<T> where T : struct

    {
        protected T[,] structualStrorage;
        protected int emptyMatrixSize = 4;

        public abstract T this[int i, int j] { get; set; }
        public int Size => structualStrorage.GetLength(0);
        public int Length => structualStrorage.Length;


        public event EventHandler<MatrixEventArgs<T>> PropertyChanged = delegate { };

        protected virtual void NotifyPropertyChanged(object sender, MatrixEventArgs<T> e)
        {
            var temp = PropertyChanged;
            temp?.Invoke(this, e);
            Console.Beep();
            if (PropertyChanged.GetInvocationList().Length == 0)
                PropertyChanged(this, e);
        }

        public override int GetHashCode()
        {
            return structualStrorage.Cast<T>().Sum(item => item.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj) || obj.GetType() != GetType())
                return false;
            return ReferenceEquals(this, obj) || Equals(obj as AbstractMatrix<T>);
        }

        public bool Equals(AbstractMatrix<T> other)
        {
            if (ReferenceEquals(null, other))
                return false;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; i < Size; j++)
                {
                    if (other[i, j].Equals(structualStrorage[i, j]))
                        return false;
                }
            }
            return true;
        }

        public void Cleare()
        {
            structualStrorage = new T[emptyMatrixSize, emptyMatrixSize];
        }

        public static bool IsSquareMatrix(AbstractMatrix<T> matrix)
        {
            return matrix.structualStrorage.GetLength(0) == matrix.structualStrorage.GetLength(1);
        }

        public static bool IsDiagonalMatrix(AbstractMatrix<T> matrix)
        {
            T diagonalValue = matrix[0, 0];

            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; i < matrix.Size; j++)
                {
                    if (i != j && !matrix[i, j].Equals(default(T)))
                        return false;

                    if (i == j && !matrix[i, j].Equals(diagonalValue))
                        return false;
                }
            }
            return true;
        }

        public static bool IsSymmetricMatrix(AbstractMatrix<T> matrix)
        {
            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; i < matrix.Size; j++)
                {
                    if (i != j && !matrix[i, j].Equals(matrix[j, i]))
                        return false;

                    if (i == j)
                        continue;
                }
            }
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return structualStrorage.Cast<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public AbstractMatrix<T> Accept(IMatrixVisitor<T> visitor, AbstractMatrix<T> other)
        {
            return visitor.Visit((dynamic) this, other);
        }

        protected static bool CheckForСompatibility(IEnumerable<T> collection)
        {
            double collectionSize = Math.Sqrt(collection.Count());
            int truncate = (int) collectionSize;
            int fract = (int) Math.Ceiling(collectionSize - truncate);

            return collectionSize <= 1 || !fract.Equals(0);
        }
    }
}