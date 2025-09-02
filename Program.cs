using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp22
{
    class MyArr
    {
        private int[] arr;
        public int Length;

        public MyArr(int Size)
        {
            arr = new int[Size];
            Length = Size;
        }

        public int this[int index]
        {
            get
            {
                CheckIndex(index);
                return arr[index];
            }

            set
            {
                CheckIndex(index);
                arr[index] = value;
            }
        }

        public int this[double index]
        {
            get
            {
                CheckIndex(index);
                int intIndex = (int)Math.Round(index);
                return arr[intIndex];
            }

            set
            {
                CheckIndex(index);
                int intIndex = (int)Math.Round(index);
                arr[intIndex] = value;
            }
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= Length)
                throw new IndexOutOfRangeException($"Индекс {index} выходит за границы массива (0-{Length - 1})");
        }

        private void CheckIndex(double index)
        {
            int intIndex = (int)Math.Round(index);
            if (intIndex < 0 || intIndex >= Length)
                throw new IndexOutOfRangeException($"Индекс {index} (округлено до {intIndex}) выходит за границы массива (0-{Length - 1})");
        }

        public void FillWithRandomNumbers(int minValue = 1, int maxValue = 100)
        {
            Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                arr[i] = random.Next(minValue, maxValue);
            }
        }

        public void PrintArray()
        {
            Console.WriteLine("Содержимое массива:");
            for (int i = 0; i < Length; i++)
            {
                Console.WriteLine($"[{i}] = {arr[i]}");
            }
        }

        public void UpdateElement(int index, int value)
        {
            CheckIndex(index);
            arr[index] = value;
            Console.WriteLine($"Элемент по индексу {index} изменен на значение {value}");
        }

        public void AddElement(int value)
        {
            int newLength = Length + 1;
            int[] newArr = new int[newLength];

            for (int i = 0; i < Length; i++)
            {
                newArr[i] = arr[i];
            }

            newArr[newLength - 1] = value;

            arr = newArr;
            Length = newLength;

            Console.WriteLine($"Добавлен новый элемент со значением {value}");
        }

        public void RemoveElement(int index)
        {
            CheckIndex(index);

            int newLength = Length - 1;
            int[] newArr = new int[newLength];

            for (int i = 0; i < index; i++)
            {
                newArr[i] = arr[i];
            }

            for (int i = index; i < newLength; i++)
            {
                newArr[i] = arr[i + 1];
            }

            arr = newArr;
            Length = newLength;

            Console.WriteLine($"Элемент по индексу {index} удален");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int size = GetArraySize();
            MyArr array = new MyArr(size);

            array.FillWithRandomNumbers();
            array.PrintArray();

            bool continueLoop = true;
            while (continueLoop)
            {
                try
                {
                    int operation = ChooseOperation();

                    switch (operation)
                    {
                        case 1:
                            double viewIndex = GetIndexAsDouble(array.Length, "просмотра");
                            Console.WriteLine($"Значение по индексу {viewIndex} (округлено до {(int)Math.Round(viewIndex)}): {array[viewIndex]}");
                            break;

                        case 2:
                            double updateIndex = GetIndexAsDouble(array.Length, "изменения");
                            Console.Write("Введите новое значение: ");
                            int newValue;
                            while (!int.TryParse(Console.ReadLine(), out newValue))
                            {
                                Console.Write("Пожалуйста, введите целое число: ");
                            }
                            array.UpdateElement((int)Math.Round(updateIndex), newValue);
                            array.PrintArray();
                            break;

                        case 3:
                            Console.Write("Введите значение для добавления: ");
                            int addValue;
                            while (!int.TryParse(Console.ReadLine(), out addValue))
                            {
                                Console.Write("Пожалуйста, введите целое число: ");
                            }
                            array.AddElement(addValue);
                            array.PrintArray();
                            break;

                        case 4:
                            double removeIndex = GetIndexAsDouble(array.Length, "удаления");
                            array.RemoveElement((int)Math.Round(removeIndex));
                            array.PrintArray();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                continueLoop = AskToContinue();
            }

            Console.WriteLine("Программа завершена. Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static int GetArraySize()
        {
            Console.Write("Введите количество элементов массива: ");
            int size;
            while (!int.TryParse(Console.ReadLine(), out size) || size <= 0)
            {
                Console.Write("Пожалуйста, введите целое положительное число: ");
            }
            return size;
        }

        static double GetIndexAsDouble(int maxIndex, string operation)
        {
            Console.Write($"Введите индекс элемента для {operation} (целое или дробное число): ");
            double index;
            while (!double.TryParse(Console.ReadLine(), out index) ||
                   Math.Round(index) < 0 || Math.Round(index) >= maxIndex)
            {
                Console.Write($"Пожалуйста, введите число от 0 до {maxIndex - 1}: ");
            }
            return index;
        }

        static int ChooseOperation()
        {
            Console.WriteLine("\nВыберите операцию:");
            Console.WriteLine("1 - Просмотр элемента по индексу");
            Console.WriteLine("2 - Изменение элемента по индексу");
            Console.WriteLine("3 - Добавление нового элемента");
            Console.WriteLine("4 - Удаление элемента по индексу");

            Console.Write("Ваш выбор: ");
            int operation;
            while (!int.TryParse(Console.ReadLine(), out operation) || operation < 1 || operation > 4)
            {
                Console.Write("Пожалуйста, введите число от 1 до 4: ");
            }
            return operation;
        }

        static bool AskToContinue()
        {
            Console.Write("\nХотите продолжить? (да/нет): ");
            string response = Console.ReadLine().ToLower();
            return response == "да" || response == "д" || response == "yes" || response == "y";
        }
    }
}