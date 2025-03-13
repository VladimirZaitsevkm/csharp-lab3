using System;

namespace LabCSharp3PrototypePattern
{
  public class SquareMatrix : ICloneable, IComparable<SquareMatrix>
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

    public static double[,] operator ~(SquareMatrix matrix)
    {
      int det = !matrix;
      if (det == 0)
      {
        throw new MatrixDeterminantZeroException("Обратной матрицы не существует, так как определитель равен нулю.");
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

    public static bool operator >(SquareMatrix matrixOne, SquareMatrix matrixTwo)
    {
      if (matrixOne.Size != matrixTwo.Size)
      {
        throw new MatrixSizeException("Матрицы должны быть одного размера");
      }

      int sumOne = 0, sumTwo = 0;
      for (int i = 0; i < matrixOne.Size; ++i)
      {
        for (int j = 0; j < matrixOne.Size; ++j)
        {
          sumOne += matrixOne.matrix[i, j];
          sumTwo += matrixTwo.matrix[i, j];
        }
      }

      return sumOne > sumTwo;
    }

    public static bool operator <(SquareMatrix matrixOne, SquareMatrix matrixTwo)
    {
      if (matrixOne.Size != matrixTwo.Size)
      {
        throw new MatrixSizeException("Матрицы должны быть одного размера");
      }

      int sumOne = 0, sumTwo = 0;
      for (int i = 0; i < matrixOne.Size; ++i)
      {
        for (int j = 0; j < matrixOne.Size; ++j)
        {
          sumOne += matrixOne.matrix[i, j];
          sumTwo += matrixTwo.matrix[i, j];
        }
      }

      return sumOne < sumTwo;
    }

    public static bool operator >=(SquareMatrix matrixOne, SquareMatrix matrixTwo)
    {
      return matrixOne > matrixTwo || matrixOne == matrixTwo;
    }

    public static bool operator <=(SquareMatrix matrixOne, SquareMatrix matrixTwo)
    {
      return matrixOne < matrixTwo || matrixOne == matrixTwo;
    }

    public static bool operator ==(SquareMatrix matrixOne, SquareMatrix matrixTwo)
    {
      if (ReferenceEquals(matrixOne, matrixTwo))
        return true;

      if (matrixOne is null || matrixTwo is null)
        return false;

      if (matrixOne.Size != matrixTwo.Size)
        return false;

      for (int i = 0; i < matrixOne.Size; ++i)
      {
        for (int j = 0; j < matrixOne.Size; ++j)
        {
          if (matrixOne.matrix[i, j] != matrixTwo.matrix[i, j])
            return false;
        }
      }

      return true;
    }

    public static bool operator !=(SquareMatrix matrixOne, SquareMatrix matrixTwo)
    {
      return !(matrixOne == matrixTwo);
    }

    public static explicit operator int(SquareMatrix matrix)
    {
      return !matrix;
    }

    public static explicit operator double[,](SquareMatrix matrix)
    {
      return ~matrix;
    }

    public static bool operator true(SquareMatrix matrix)
    {
      return !matrix != 0;
    }

    public static bool operator false(SquareMatrix matrix)
    {
      return !matrix == 0;
    }

    public override string ToString()
    {
      string result = "";
      for (int i = 0; i < Size; ++i)
      {
        for (int j = 0; j < Size; ++j)
        {
          result += matrix[i, j] + "\t";
        }
        result += "\n";
      }
      return result;
    }

    public int CompareTo(SquareMatrix other)
    {
      if (other == null) return 1;

      int thisSum = 0, otherSum = 0;
      for (int i = 0; i < Size; ++i)
      {
        for (int j = 0; j < Size; ++j)
        {
          thisSum += matrix[i, j];
          otherSum += other.matrix[i, j];
        }
      }

      return thisSum.CompareTo(otherSum);
    }

    public override bool Equals(object obj)
    {
      if (obj == null || GetType() != obj.GetType())
        return false;

      SquareMatrix other = (SquareMatrix)obj;
      return this == other;
    }

    public override int GetHashCode()
    {
      int hash = 17;
      for (int i = 0; i < Size; ++i)
      {
        for (int j = 0; j < Size; ++j)
        {
          hash = hash * 23 + matrix[i, j].GetHashCode();
        }
      }
      return hash;
    }

    public object Clone()
    {
      SquareMatrix clone = new SquareMatrix(Size);
      for (int i = 0; i < Size; ++i)
      {
        for (int j = 0; j < Size; ++j)
        {
          clone.matrix[i, j] = matrix[i, j];
        }
      }
      return clone;
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

  public class MatrixDeterminantZeroException : Exception
  {
    public MatrixDeterminantZeroException(string message) : base(message) { }
  }

  internal class Program
  {
    static void Main(string[] args)
    {
      try
      {
        SquareMatrix matrix1 = new SquareMatrix(3);
        SquareMatrix matrix2 = new SquareMatrix(3);

        Console.WriteLine("Matrix 1:");
        Console.WriteLine(matrix1);

        Console.WriteLine("Matrix 2:");
        Console.WriteLine(matrix2);

        SquareMatrix sum = matrix1 + matrix2;
        Console.WriteLine("Sum:");
        Console.WriteLine(sum);

        SquareMatrix product = matrix1 * matrix2;
        Console.WriteLine("Product:");
        Console.WriteLine(product);

        int determinant = !matrix1;
        Console.WriteLine("Determinant of Matrix 1: " + determinant);

        double[,] inverse = ~matrix1;
        Console.WriteLine("Inverse of Matrix 1:");
        for (int i = 0; i < inverse.GetLength(0); i++)
        {
          for (int j = 0; j < inverse.GetLength(1); j++)
          {
            Console.Write(inverse[i, j] + "\t");
          }
          Console.WriteLine();
        }

        SquareMatrix clone = (SquareMatrix)matrix1.Clone();
        Console.WriteLine("Clone of Matrix 1:");
        Console.WriteLine(clone);
      }
      catch (MatrixSizeException ex)
      {
        Console.WriteLine("Matrix Size Exception: " + ex.Message);
      }
      catch (MatrixDeterminantZeroException ex)
      {
        Console.WriteLine("Matrix Determinant Zero Exception: " + ex.Message);
      }
      catch (Exception ex)
      {
        Console.WriteLine("An error occurred: " + ex.Message);
      }
    }
  }
}