using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace LabCSharp3PrototypePattern
{

  public class SquareMatrix
  {
    public static Random rnd = new Random();
    public int[,] matrix;
    public int Size;

    public SquareMatrix(int size)
    {
      if (size <= 0) throw new MatrixSizeException("Размер матрицы был указан некорректно");
      this.Size = size;
      this.matrix = new int[size, size];
      generateSquareMatrix();
    }

    public void generateSquareMatrix()
    {
      for (int i = 0; i < Size; ++i)
      {
        for (int j = 0; j < Size; ++j)
        {
          matrix[i, j] = rnd.Next(1000);
        }
      }
    }


    public static SquareMatrix operator +(SquareMatrix matrixOne, SquareMatrix matrixTwo)
    {

      if (matrixOne.Size != matrixTwo.Size)
      {
        throw new MatrixSizeException("Матрицы должны быть одного размера");
      }

      SquareMatrix result = new SquareMatrix(matrixOne.Size);

      for (int i = 0; i < matrixOne.Size; ++i)
      {
        for (int j = 0; j < matrixOne.Size; ++j)
        {
          result.matrix[i, j] = matrixOne.matrix[i, j] + matrixTwo.matrix[i, j];
        }
      }

      return result;
    }

    public static SquareMatrix operator -(SquareMatrix matrixOne, SquareMatrix matrixTwo)
    {

      if (matrixOne.Size != matrixTwo.Size)
      {
        throw new MatrixSizeException("Матрицы должны быть одного размера");
      }

      SquareMatrix result = new SquareMatrix(matrixOne.Size);

      for (int i = 0; i < matrixOne.Size; ++i)
      {
        for (int j = 0; j < matrixOne.Size; ++j)
        {
          result.matrix[i, j] = matrixOne.matrix[i, j] - matrixTwo.matrix[i, j];
        }
      }

      return result;
    }

    public static SquareMatrix operator *(SquareMatrix matrixOne, SquareMatrix matrixTwo)
    {

      if (matrixOne.Size != matrixTwo.Size)
      {
        throw new MatrixSizeException("Матрицы должны быть одного размера");
      }

      SquareMatrix result = new SquareMatrix(matrixOne.Size);

      for (int i = 0; i < matrixOne.Size; ++i)
      {
        for (int j = 0; j < matrixOne.Size; ++j)
        {
          for (int k = 0; k < matrixOne.Size; ++k)
          {
            result.matrix[i, j] += matrixOne.matrix[i, k] * matrixTwo.matrix[k, j];
          }
        }
      }

      return result;
    }

    public static int operator !(SquareMatrix matrix)
    {
      return matrix.CalculateDeterminant(matrix.matrix, matrix.Size);
    }

    private int CalculateDeterminant(int[,] mat, int size)
    {
      if (size == 1)
        return mat[0, 0];

      if (size == 2)
        return mat[0, 0] * mat[1, 1] - mat[0, 1] * mat[1, 0];

      int det = 0;
      int sign = 1;

      for (int i = 0; i < size; i++)
      {
        int[,] subMatrix = new int[size - 1, size - 1];
        for (int j = 1; j < size; j++)
        {
          int col = 0;
          for (int k = 0; k < size; k++)
          {
            if (k == i) continue;
            subMatrix[j - 1, col] = mat[j, k];
            col++;
          }
        }

        det += sign * mat[0, i] * CalculateDeterminant(subMatrix, size - 1);
        sign = -sign; 
      }

      return det;
    }
  }

  public class MatrixSizeException : Exception
  {
    public MatrixSizeException(string message) : base(message) { }
  }

  internal class Program
  {
    static void Main(string[] args)
    {
    }
  }
}
