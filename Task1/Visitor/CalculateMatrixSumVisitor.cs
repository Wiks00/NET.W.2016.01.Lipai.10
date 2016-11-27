using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class CalculateMatrixSumVisitor<T> : IMatrixVisitor<T> where T : struct
    {
        public SquareMatrix<T> Visit(SquareMatrix<T> matrix, AbstractMatrix<T> other)
        {
            if (ReferenceEquals(matrix, null) || ReferenceEquals(other, null))
                throw new ArgumentNullException();

            if (matrix.Size != other.Size)
                throw new ArgumentException();

            var temp = new SquareMatrix<T>(matrix.Size);

            ByPass(matrix, other, temp);

            return temp;
        }

        public AbstractMatrix<T> Visit(SymmetricMatrix<T> matrix, AbstractMatrix<T> other)
        {
            if (ReferenceEquals(matrix, null) || ReferenceEquals(other, null))
                throw new ArgumentNullException();

            if (matrix.Size != other.Size)
                throw new ArgumentException();

            AbstractMatrix<T> temp;

            if (other is SymmetricMatrix<T> || other is DiagonalMatrix<T>)
                temp = new SymmetricMatrix<T>();
            else
                temp = new SquareMatrix<T>();

            ByPass(matrix, other, temp);

            return temp;
        }

        public AbstractMatrix<T> Visit(DiagonalMatrix<T> matrix, AbstractMatrix<T> other)
        {
            if (ReferenceEquals(matrix, null) || ReferenceEquals(other, null))
                throw new ArgumentNullException();

            if (matrix.Size != other.Size)
                throw new ArgumentException();

            AbstractMatrix<T> temp;

            if (other is DiagonalMatrix<T>)
                temp = new DiagonalMatrix<T>();
            else if (other is SymmetricMatrix<T>)
                temp = new SymmetricMatrix<T>();
            else
                temp = new SquareMatrix<T>();

            ByPass(matrix, other, temp);

            return temp;
        }

        private void ByPass(AbstractMatrix<T> lhs, AbstractMatrix<T> rhs, AbstractMatrix<T> result)
        {
            for (int i = 0; i < lhs.Size; i++)
            {
                for (int j = 0; j < lhs.Size; j++)
                {
                    result[i, j] = AddHelper(lhs[i, j], rhs[i, j]);
                }
            }
        }

        private T AddHelper(dynamic a1, T a2) => a1 + a2;
    }
}