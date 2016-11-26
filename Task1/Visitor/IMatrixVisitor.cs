using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public interface IMatrixVisitor<T> where T : struct 
    {
        SquareMatrix<T> Visit(SquareMatrix<T> matrix, AbstractMatrix<T> other);
        AbstractMatrix<T> Visit(SymmetricMatrix<T> matrix, AbstractMatrix<T> other);
        AbstractMatrix<T> Visit(DiagonalMatrix<T> matrix, AbstractMatrix<T> other);
    }
}
