using System;

namespace LabCSharp3PrototypePattern
{
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
      catch (Exception ex)
      {
        Console.WriteLine("Произошла ошибка: " + ex.Message);
      }
    }
  }
}