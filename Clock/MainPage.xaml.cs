using System.Drawing;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;


namespace Clock;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        Timer();
    }

    public async void Timer()
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        while (await timer.WaitForNextTickAsync()) // Для ежесекундного обновления
        {
            var time = DateTime.Now; // Достаем текущее время
            DisplayClock(time); // Вызываем функцию вывода часов
        }
    }

    // Функция получает число и возвращает Сетку, которую уже можно вывести на экран
    public Grid GenerateDigitView(int number)
    {
        Grid digitGrid = new(); // Создаем новую сетку

        for (int i = 0; i < 7; i++)
            digitGrid.RowDefinitions.Add(new RowDefinition()); // Создаем у нее 7 строк
        for (int i = 0; i < 5; i++)
            digitGrid.ColumnDefinitions.Add(new ColumnDefinition()); // И 5 столбцов


        bool[,] digitMatrix = GenerateMatrixForDigit(number); // Вызываем функцию конвертации числа в матрицу
        for (int row = 0; row < digitMatrix.GetLength(0); row++)
        {
            for (int col = 0; col < digitMatrix.GetLength(1); col++)
            {
                if (digitMatrix[row, col] == true) // Если в матрице на пересечении col столбца и row строки находится true, то этот квадрат нужно вывести
                {
                    var rectangle = new BoxView // Создаем квадрат
                    { 
                        Color = Colors.Black,
                        WidthRequest = 20,
                        HeightRequest = 20
                    };
                    digitGrid.Add(rectangle, col, row); // Добавляем в сетку на пересечение col столбца и row строки созданный квадрат
                }
            }
        }
        digitGrid.Padding = new Thickness(10); // Добавляем отступы для числа

        return digitGrid;
    }

    public void DisplayClock(DateTime time)
    {
        // Отчищаем экран от предыдущего времени
        DigitsFlexLayout.Children.Clear();

        // Разбиваем полученное время на 6 чисел
        int Hour1 = time.Hour / 10;
        int Hour2 = time.Hour % 10;
        int Min1 = time.Minute / 10;
        int Min2 = time.Minute % 10;
        int Sec1 = time.Second / 10;
        int Sec2 = time.Second % 10;

        // Выводим Часы
        DisplayDigit(Hour1);
        DisplayDigit(Hour2);

        // Выводим Точки
        DisplayDots();

        // Выводим Минуты
        DisplayDigit(Min1);
        DisplayDigit(Min2);

        // Выводим Точки
        DisplayDots();

        // Выводим Секунды
        DisplayDigit(Sec1);
        DisplayDigit(Sec2);

    }

    // Функция вывода числа на экран
    public void DisplayDigit(int digit)
    {
        Grid grid = GenerateDigitView(digit); // Вызываем функция преобразования числа в Grid
        DigitsFlexLayout.Children.Add(grid); // Добавляем полученный Grid в DigitsFlexLayout
    }

    public void DisplayDots()
    {
        // Создаем новый Grid для отображения двоеточия
        var colonGrid = new Grid();

        for (int i = 0; i < 3; i++)
            colonGrid.RowDefinitions.Add(new RowDefinition()); // Добавляем в сетку Grid 3 строки (2 для точек и 1 для пробела)

        // Создаем две BoxView для отображения точек
        var dot1 = new BoxView
        {
            Color = Colors.Black, // Цвет точки
            WidthRequest = 15, // Ширина
            HeightRequest = 15 // Высота
        };
        var dot2 = new BoxView
        {
            Color = Colors.Black,
            WidthRequest = 15,
            HeightRequest = 15
        };

        // Создаем BoxView для отображения пробела между точками
        var space = new BoxView
        {
            Color = Colors.Aquamarine, // Делай пробел под цвет фона
            WidthRequest = 15,
            HeightRequest = 15
        };

        // Добавляем созданные элементы в Grid (что добавляем, столбец, строка)
        colonGrid.Add(dot1, 0, 0);
        colonGrid.Add(space, 0, 1);
        colonGrid.Add(dot2, 0, 2);

        // Добавляем Grid с двоеточием в DigitsFlexLayout
        DigitsFlexLayout.Children.Add(colonGrid);
    }


    // Функция конвертации числа в графическое представление 
    public bool[,] GenerateMatrixForDigit(int digit)
    {
        bool[,] digitMatrix = new bool[7, 5]; // Создаем матрицу из 7 строк и 5 столбцов

        switch (digit)
        {
            case 0:
                digitMatrix = new bool[,]
                {
                {true, true, true, true, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, true}
                };
                break;
            case 1:
                digitMatrix = new bool[,]
                {
                {false, false, true, false, false},
                {false, true, true, false, false},
                {true, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {true, true, true, true, true}
                };
                break;
            case 2:
                digitMatrix = new bool[,]
                {
                {true, true, true, true, true},
                {false, false, false, false, true},
                {false, false, false, false, true},
                {true, true, true, true, true},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, true, true, true, true}
                };
                break;
            case 3:
                digitMatrix = new bool[,]
                {
                {true, true, true, true, true},
                {false, false, false, false, true},
                {false, false, false, false, true},
                {true, true, true, true, true},
                {false, false, false, false, true},
                {false, false, false, false, true},
                {true, true, true, true, true}
                };
                break;
            case 4:
                digitMatrix = new bool[,]
                {
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, true},
                {false, false, false, false, true},
                {false, false, false, false, true},
                {false, false, false, false, true}
                };
                break;
            case 5:
                digitMatrix = new bool[,]
                {
                {true, true, true, true, true},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, true, true, true, true},
                {false, false, false, false, true},
                {false, false, false, false, true},
                {true, true, true, true, true}
                };
                break;
            case 6:
                digitMatrix = new bool[,]
                {
                {true, true, true, true, true},
                {true, false, false, false, false},
                {true, false, false, false, false},
                {true, true, true, true, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, true}
                };
                break;
            case 7:
                digitMatrix = new bool[,]
                {
                {true, true, true, true, true},
                {false, false, false, false, true},
                {false, false, false, false, true},
                {false, false, false, false, true},
                {false, false, false, false, true},
                {false, false, false, false, true},
                {false, false, false, false, true}
                };
                break;
            case 8:
                digitMatrix = new bool[,]
                {
                {true, true, true, true, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, true}
                };
                break;
            case 9:
                digitMatrix = new bool[,]
                {
                {true, true, true, true, true},
                {true, false, false, false, true},
                {true, false, false, false, true},
                {true, true, true, true, true},
                {false, false, false, false, true},
                {false, false, false, false, true},
                {true, true, true, true, true}
                };
                break;
        }
        return digitMatrix;
    }

}


