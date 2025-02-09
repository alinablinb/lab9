using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лаба_9
{
    public class WeatherArray
    {
        private Weather[] arr; // Одномерный массив элементов типа Weather
        private static int collectionCount = 0; // Счетчик созданных коллекций

        // Свойство для получения длины массива
        public int Length => arr.Length;

        // Конструктор без параметров
        public WeatherArray()
        {
            arr = new Weather[0];
            collectionCount++;
        }

        // Конструктор с параметрами, заполняющий элементы случайными значениями
        public WeatherArray(int size)
        {
            Random random = new Random();
            arr = new Weather[size];
            for (int i = 0; i < size; i++)
            {
                double temperature = Math.Round(random.NextDouble() * 50 - 20, 1); // Температура от -20 до 30 °C
                int humidity = random.Next(0, 101); // Влажность от 0 до 100%
                int pressure = random.Next(700, 800); // Давление от 700 до 800 мм рт. ст.
                arr[i] = new Weather(temperature, humidity, pressure);
            }
            collectionCount++;
        }

        // Конструктор копирования (глубокое копирование)
        public WeatherArray(WeatherArray other)
        {
            arr = new Weather[other.arr.Length];
            for (int i = 0; i < other.arr.Length; i++)
            {
                arr[i] = new Weather(other.arr[i].Temperature, other.arr[i].Humidity, other.arr[i].Pressure);
            }
            collectionCount++;
        }

        // Метод для просмотра элементов массива
        public string[] GetWeatherAttributes()
        {
            string[] attributes = new string[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                attributes[i] = $"Элемент {i+1}: {arr[i].ShowAttributes()}, Точка росы: {arr[i].GetDewPoint()} °C";
            }
            return attributes;
        }

        // Индексатор для доступа к элементам коллекции
        public Weather this[int index]
        {
            get
            {
                if (index < 0 || index >= arr.Length)
                    throw new IndexOutOfRangeException("Индекс выходит за пределы массива.");
                return arr[index];
            }
            set
            {
                if (index < 0 || index >= arr.Length)
                    throw new IndexOutOfRangeException("Индекс выходит за пределы массива.");
                arr[index] = value;
            }
        }

        // Статический метод для получения количества созданных коллекций
        public static int GetCollectionCount()
        {
            return collectionCount;
        }
    }
}