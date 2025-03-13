using System;

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
      GenerateSquareMatrix();
    }

    public void GenerateSquareMatrix()
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
          result.matrix[i, j] = 0;
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
      return CalculateDeterminant(matrix.matrix, matrix.Size);
    }

    private static int CalculateDeterminant(int[,] mat, int size)
    {
      if (size == 1)
        return mat[0, 0];

      if (size == 2)
        return mat[0, 0] * mat[1, 1] - mat[0, 1] * mat[1, 0];

      int det = 0;
      int sign = 1;

      for (int i = 0; i < size; i++)
      {
        int[,] subMatrix = GetSubMatrix(mat, size, 0, i);
        det += sign * mat[0, i] * CalculateDeterminant(subMatrix, size - 1);
        sign = -sign;
      }

      return det;
    }

    public static double[,] operator ~(SquareMatrix matrix)
    {
      int det = !matrix;
      if (det == 0)
      {
        throw new InvalidOperationException("Обратной матрицы не существует, так как определитель равен нулю.");
      }

      double[,] inverseMatrix = new double[matrix.Size, matrix.Size];
      int[,] adjugateMatrix = GetAdjugateMatrix(matrix.matrix, matrix.Size);

      for (int i = 0; i < matrix.Size; i++)
      {
        for (int j = 0; j < matrix.Size; j++)
        {
          inverseMatrix[i, j] = (double)adjugateMatrix[i, j] / det;
        }
      }

      return inverseMatrix;
    }

    private static int[,] GetAdjugateMatrix(int[,] mat, int size)
    {
      int[,] adjugateMatrix = new int[size, size];

      for (int i = 0; i < size; i++)
      {
        for (int j = 0; j < size; j++)
        {
          int[,] subMatrix = GetSubMatrix(mat, size, i, j);
          int sign = ((i + j) % 2 == 0) ? 1 : -1;
          adjugateMatrix[j, i] = sign * CalculateDeterminant(subMatrix, size - 1);
        }
      }

      return adjugateMatrix;
    }

    private static int[,] GetSubMatrix(int[,] mat, int size, int rowToRemove, int colToRemove)
    {
      int[,] subMatrix = new int[size - 1, size - 1];
      int row = 0, col = 0;

      for (int i = 0; i < size; i++)
      {
        if (i == rowToRemove) continue;

        col = 0;
        for (int j = 0; j < size; j++)
        {
          if (j == colToRemove) continue;
          subMatrix[row, col] = mat[i, j];
          col++;
        }
        row++;
      }

      return subMatrix;
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