using System;

namespace LabCSharp3PrototypePattern
{
  public class SquareMatrix : ICloneable, IComparable<SquareMatrix>
  {
    private static Random s_random = new Random();
    private int[,] _matrix;
    public int Size { get; private set; }

    public SquareMatrix(int size)
    {
      if (size <= 0) throw new MatrixSizeException("Размер матрицы был указан некорректно");
      Size = size;
      _matrix = new int[size, size];
      GenerateSquareMatrix();
    }

    private void GenerateSquareMatrix()
    {
      for (int row = 0; row < Size; ++row)
      {
        for (int column = 0; column < Size; ++column)
        {
          _matrix[row, column] = s_random.Next(1000);
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

      for (int row = 0; row < matrixOne.Size; ++row)
      {
        for (int column = 0; column < matrixOne.Size; ++column)
        {
          result._matrix[row, column] = matrixOne._matrix[row, column] + matrixTwo._matrix[row, column];
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

      for (int row = 0; row < matrixOne.Size; ++row)
      {
        for (int column = 0; column < matrixOne.Size; ++column)
        {
          result._matrix[row, column] = matrixOne._matrix[row, column] - matrixTwo._matrix[row, column];
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

      for (int row = 0; row < matrixOne.Size; ++row)
      {
        for (int column = 0; column < matrixOne.Size; ++column)
        {
          result._matrix[row, column] = 0;
          for (int index = 0; index < matrixOne.Size; ++index)
          {
            result._matrix[row, column] += matrixOne._matrix[row, index] * matrixTwo._matrix[index, column];
          }
        }
      }

      return result;
    }

    public static int operator !(SquareMatrix matrix)
    {
      return CalculateDeterminant(matrix._matrix, matrix.Size);
    }

    public static double[,] operator ~(SquareMatrix matrix)
    {
      int determinant = !matrix;
      if (determinant == 0)
      {
        throw new MatrixDeterminantZeroException("Обратной матрицы не существует, так как определитель равен нулю.");
      }

      double[,] inverseMatrix = new double[matrix.Size, matrix.Size];
      int[,] adjugateMatrix = GetAdjugateMatrix(matrix._matrix, matrix.Size);

      for (int row = 0; row < matrix.Size; row++)
      {
        for (int column = 0; column < matrix.Size; column++)
        {
          inverseMatrix[row, column] = (double)adjugateMatrix[row, column] / determinant;
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
      for (int row = 0; row < matrixOne.Size; ++row)
      {
        for (int column = 0; column < matrixOne.Size; ++column)
        {
          sumOne += matrixOne._matrix[row, column];
          sumTwo += matrixTwo._matrix[row, column];
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
      for (int row = 0; row < matrixOne.Size; ++row)
      {
        for (int column = 0; column < matrixOne.Size; ++column)
        {
          sumOne += matrixOne._matrix[row, column];
          sumTwo += matrixTwo._matrix[row, column];
        }
      }

      return sumOne < sumTwo;
    }

    public static bool operator >= (SquareMatrix matrixOne, SquareMatrix matrixTwo)
    {
      return matrixOne > matrixTwo || matrixOne == matrixTwo;
    }

    public static bool operator <= (SquareMatrix matrixOne, SquareMatrix matrixTwo)
    {
      return matrixOne < matrixTwo || matrixOne == matrixTwo;
    }

    public static bool operator == (SquareMatrix matrixOne, SquareMatrix matrixTwo)
    {
      if (ReferenceEquals(matrixOne, matrixTwo))
        return true;

      if (matrixOne is null || matrixTwo is null)
        return false;

      if (matrixOne.Size != matrixTwo.Size)
        return false;

      for (int row = 0; row < matrixOne.Size; ++row)
      {
        for (int column = 0; column < matrixOne.Size; ++column)
        {
          if (matrixOne._matrix[row, column] != matrixTwo._matrix[row, column])
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
      for (int row = 0; row < Size; ++row)
      {
        for (int column = 0; column < Size; ++column)
        {
          result += _matrix[row, column] + "\t";
        }
        result += "\n";
      }
      return result;
    }

    public int CompareTo(SquareMatrix other)
    {
      if (other == null) return 1;

      int thisSum = 0, otherSum = 0;
      for (int row = 0; row < Size; ++row)
      {
        for (int column = 0; column < Size; ++column)
        {
          thisSum += _matrix[row, column];
          otherSum += other._matrix[row, column];
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
      for (int row = 0; row < Size; ++row)
      {
        for (int column = 0; column < Size; ++column)
        {
          hash = hash * 23 + _matrix[row, column].GetHashCode();
        }
      }
      return hash;
    }

    public object Clone()
    {
      SquareMatrix clone = new SquareMatrix(Size);
      for (int row = 0; row < Size; ++row)
      {
        for (int column = 0; column < Size; ++column)
        {
          clone._matrix[row, column] = _matrix[row, column];
        }
      }
      return clone;
    }

    private static int CalculateDeterminant(int[,] matrix, int size)
    {
      if (size == 1)
        return matrix[0, 0];

      if (size == 2)
        return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

      int determinant = 0;
      int sign = 1;

      for (int column = 0; column < size; column++)
      {
        int[,] subMatrix = GetSubMatrix(matrix, size, 0, column);
        determinant += sign * matrix[0, column] * CalculateDeterminant(subMatrix, size - 1);
        sign = -sign;
      }

      return determinant;
    }

    private static int[,] GetAdjugateMatrix(int[,] matrix, int size)
    {
      int[,] adjugateMatrix = new int[size, size];

      for (int row = 0; row < size; row++)
      {
        for (int column = 0; column < size; column++)
        {
          int[,] subMatrix = GetSubMatrix(matrix, size, row, column);
          int sign = ((row + column) % 2 == 0) ? 1 : -1;
          adjugateMatrix[column, row] = sign * CalculateDeterminant(subMatrix, size - 1);
        }
      }

      return adjugateMatrix;
    }

    private static int[,] GetSubMatrix(int[,] matrix, int size, int rowToRemove, int columnToRemove)
    {
      int[,] subMatrix = new int[size - 1, size - 1];
      int row = 0, column = 0;

      for (int i = 0; i < size; i++)
      {
        if (i == rowToRemove) continue;

        column = 0;
        for (int j = 0; j < size; j++)
        {
          if (j == columnToRemove) continue;
          subMatrix[row, column] = matrix[i, j];
          column++;
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
        Console.WriteLine("Введите размер матриц:");
        int size = int.Parse(Console.ReadLine());

        SquareMatrix matrix1 = new SquareMatrix(size);
        SquareMatrix matrix2 = new SquareMatrix(size);

        Console.WriteLine("Матрица 1:");
        Console.WriteLine(matrix1);

        Console.WriteLine("Матрица 2:");
        Console.WriteLine(matrix2);

        while (true)
        {
          Console.WriteLine("\nВыберите операцию:");
          Console.WriteLine("1. Сложение матриц");
          Console.WriteLine("2. Вычитание матриц");
          Console.WriteLine("3. Умножение матриц");
          Console.WriteLine("4. Нахождение определителя матрицы 1");
          Console.WriteLine("5. Нахождение обратной матрицы для матрицы 1");
          Console.WriteLine("6. Клонирование матрицы 1");
          Console.WriteLine("7. Сравнение матриц");
          Console.WriteLine("8. Выход");

          int choice = int.Parse(Console.ReadLine());

          switch (choice)
          {
            case 1:
              SquareMatrix sum = matrix1 + matrix2;
              Console.WriteLine("Результат сложения:");
              Console.WriteLine(sum);
              break;

            case 2:
              SquareMatrix difference = matrix1 - matrix2;
              Console.WriteLine("Результат вычитания:");
              Console.WriteLine(difference);
              break;

            case 3:
              SquareMatrix product = matrix1 * matrix2;
              Console.WriteLine("Результат умножения:");
              Console.WriteLine(product);
              break;

            case 4:
              int determinant = !matrix1;
              Console.WriteLine("Определитель матрицы 1: " + determinant);
              break;

            case 5:
              double[,] inverse = ~matrix1;
              Console.WriteLine("Обратная матрица для матрицы 1:");
              for (int row = 0; row < inverse.GetLength(0); row++)
              {
                for (int column = 0; column < inverse.GetLength(1); column++)
                {
                  Console.Write(inverse[row, column] + "\t");
                }
                Console.WriteLine();
              }
              break;

            case 6:
              SquareMatrix clone = (SquareMatrix)matrix1.Clone();
              Console.WriteLine("Клон матрицы 1:");
              Console.WriteLine(clone);
              break;

            case 7:
              Console.WriteLine("\nВыберите операцию сравнения:");
              Console.WriteLine("1. Матрица 1 > Матрица 2");
              Console.WriteLine("2. Матрица 1 < Матрица 2");
              Console.WriteLine("3. Матрица 1 >= Матрица 2");
              Console.WriteLine("4. Матрица 1 <= Матрица 2");
              Console.WriteLine("5. Матрица 1 == Матрица 2");
              Console.WriteLine("6. Матрица 1 != Матрица 2");

              int compareChoice = int.Parse(Console.ReadLine());

              switch (compareChoice)
              {
                case 1:
                  Console.WriteLine("Матрица 1 > Матрица 2: " + (matrix1 > matrix2));
                  break;

                case 2:
                  Console.WriteLine("Матрица 1 < Матрица 2: " + (matrix1 < matrix2));
                  break;

                case 3:
                  Console.WriteLine("Матрица 1 >= Матрица 2: " + (matrix1 >= matrix2));
                  break;

                case 4:
                  Console.WriteLine("Матрица 1 <= Матрица 2: " + (matrix1 <= matrix2));
                  break;

                case 5:
                  Console.WriteLine("Матрица 1 == Матрица 2: " + (matrix1 == matrix2));
                  break;

                case 6:
                  Console.WriteLine("Матрица 1 != Матрица 2: " + (matrix1 != matrix2));
                  break;

                default:
                  Console.WriteLine("Неверный выбор. Попробуйте снова.");
                  break;
              }
              break;

            case 8:
              return;

            default:
              Console.WriteLine("Неверный выбор. Попробуйте снова.");
              break;
          }
        }
      }
      catch (MatrixSizeException ex)
      {
        Console.WriteLine("Ошибка размера матрицы: " + ex.Message);
      }
      catch (MatrixDeterminantZeroException ex)
      {
        Console.WriteLine("Ошибка определителя: " + ex.Message);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Произошла ошибка: " + ex.Message);
      }
    }
  }
}