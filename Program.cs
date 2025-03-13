using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
      for (int i = 0; i < Size; i++)
      {
        for (int j = 0; j < Size; j++)
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

      for (int i = 0; i < matrixOne.Size; i++)
      {
        for (int j = 0; j < matrixOne.Size; j++)
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

      for (int i = 0; i < matrixOne.Size; i++)
      {
        for (int j = 0; j < matrixOne.Size; j++)
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

      for (int i = 0; i < matrixOne.Size; i++)
      {
        for (int j = 0; j < matrixOne.Size; j++)
        {
          for (int k = 0; k < matrixOne.Size; k++)
          {
            result.matrix[i, j] += matrixOne.matrix[i, k] * matrixTwo.matrix[k, j];
          }
        }
      }

      return result;
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
