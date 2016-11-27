using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public static class MatrixExtensions
    {
        public static AbstractMatrix<T> Sum<T>(this AbstractMatrix<T> matrix, AbstractMatrix<T> other) where T : struct
        {
            var visitor = new CalculateMatrixSumVisitor<T>();
            return matrix.Accept(visitor, other);
        }
    }
}