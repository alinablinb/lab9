using лаба_9;

using System;

namespace лаба_9
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Создание массива с ручным вводом
                WeatherArray manualArray = CreateManualWeatherArray();
                Console.WriteLine("Массив с ручным вводом:");
                foreach (var attribute in manualArray.GetWeatherAttributes())
                {
                    Console.WriteLine(attribute);
                }

                // Создание массива со случайной генерацией
                WeatherArray randomArray = new WeatherArray(5);
                Console.WriteLine("\nМассив со случайной генерацией:");
                foreach (var attribute in randomArray.GetWeatherAttributes())
                {
                    Console.WriteLine(attribute);
                }

                // Создание копии массива (глубокое копирование)
                WeatherArray copiedArray = new WeatherArray(randomArray);
                Console.WriteLine("\nКопия массива со случайной генерацией:");
                foreach (var attribute in copiedArray.GetWeatherAttributes())
                {
                    Console.WriteLine(attribute);
                }


                // Нахождение амплитуды температур
                Console.WriteLine($"\nАмплитуда температур в массиве: {CalculateTemperatureAmplitude(randomArray)} °C");

                // Подсчет количества созданных объектов и коллекций
                Console.WriteLine($"\nКоличество созданных объектов Weather: {Weather.GetObjectCount()}");
                Console.WriteLine($"Количество созданных коллекций WeatherArray: {WeatherArray.GetCollectionCount()}");

                Console.WriteLine("\nДемонстрация операций части 2:");
                Weather weather1 = new Weather(25.0, 85, 770);
                Weather weather2 = new Weather(15.0, 70, 750);

                // Унарные операции
                Weather invertedWeather = -weather1;
                Console.WriteLine($"Исходная погода: {weather1.ShowAttributes()}");
                Console.WriteLine($"Погода с обратной температурой: {invertedWeather.ShowAttributes()}");

                bool isHumidityHigh = !weather1;
                Console.WriteLine($"Влажность выше 80%: {isHumidityHigh}");

                // Операции приведения типа
                bool isPressureHigh = (bool)weather1;
                Console.WriteLine($"Давление выше 760 мм рт. ст.: {isPressureHigh}");

                double humindex = weather1;
                Console.WriteLine($"Ощущаемая температура: {humindex} °C");

                // Бинарные операции
                Weather weather3 = weather1 - 5.0;
                Console.WriteLine($"Погода после уменьшения температуры на 5 °C: {weather3.ShowAttributes()}");

                Weather weather4 = weather2 * 10.0;
                Console.WriteLine($"Погода после увеличения параметров на 10%: {weather4.ShowAttributes()}");

                // Сравнение объектов
                Console.WriteLine($"weather1 и weather2 равны: {weather1.Equals(weather2)}");

                // Демонстрация работы индексатора
                Console.WriteLine("\nДемонстрация работы индексатора:");
                try
                {
                    // Получение объекта с существующим индексом
                    Console.WriteLine($"Элемент с индексом 1: {randomArray[1].ShowAttributes()}, Точка росы: {randomArray[1].GetDewPoint()} °C");

                    // Получение объекта с несуществующим индексом
                    Console.WriteLine($"Элемент с индексом 10: {randomArray[10].ShowAttributes()}");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    // Запись объекта с существующим индексом
                    randomArray[1] = new Weather(30.0, 50, 770);
                    Console.WriteLine($"Измененный элемент с индексом 1: {randomArray[1].ShowAttributes()}, Точка росы: {randomArray[1].GetDewPoint()} °C");

                    // Запись объекта с несуществующим индексом
                    randomArray[10] = new Weather(40.0, 70, 780);
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        // Метод для создания массива с ручным вводом данных
        static WeatherArray CreateManualWeatherArray()
        {
            int size = ReadPositiveInt("Введите количество элементов в массиве: ");
            WeatherArray manualArray = new WeatherArray(size);

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"\nВведите данные для элемента массива {i + 1}:");
                double temperature = ReadDoubleWithRetry("Температура (°C): ");
                int humidity = ReadIntWithRetry("Влажность (%): ", 0, 100);
                int pressure = ReadIntWithRetry("Давление (мм рт. ст.): ", 0, int.MaxValue);

                manualArray[i] = new Weather(temperature, humidity, pressure);
            }

            return manualArray;
        }

        // Метод для ввода положительного целого числа
        static int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    int value = int.Parse(Console.ReadLine());
                    if (value <= 0)
                        throw new Exception("Размер массива должен быть положительным числом.");
                    return value;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: неверный формат числа. Пожалуйста, введите целое число.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        // Метод для ввода double с повторением в случае ошибки
        static double ReadDoubleWithRetry(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    double value = double.Parse(Console.ReadLine());

                    // Проверка на абсолютный ноль
                    if (value <= -273.7)
                    {
                        throw new Exception("Температура не может быть ниже абсолютного нуля (-273.7 °C).");
                    }

                    return value;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: неверный формат числа. Пожалуйста, введите число с плавающей точкой.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        // Метод для ввода int с повторением в случае ошибки и проверкой диапазона
        static int ReadIntWithRetry(string prompt, int minValue, int maxValue)
        {
            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    int value = int.Parse(Console.ReadLine());
                    if (value < minValue || value > maxValue)
                        throw new Exception($"Значение должно быть в диапазоне от {minValue} до {maxValue}.");
                    return value;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: неверный формат числа. Пожалуйста, введите целое число.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        // Функция для нахождения амплитуды температур
        static double CalculateTemperatureAmplitude(WeatherArray weatherArray)
        {
            if (weatherArray == null || weatherArray.Length == 0)
                throw new ArgumentException("Массив пуст.");

            double minTemp = weatherArray[0].Temperature;
            double maxTemp = weatherArray[0].Temperature;

            for (int i = 1; i < weatherArray.Length; i++)
            {
                if (weatherArray[i].Temperature < minTemp)
                    minTemp = weatherArray[i].Temperature;
                if (weatherArray[i].Temperature > maxTemp)
                    maxTemp = weatherArray[i].Temperature;
            }

            return maxTemp - minTemp;
        }
    }
}