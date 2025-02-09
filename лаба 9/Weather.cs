using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace лаба_9
{
    public class Weather
    {
        // Закрытые атрибуты        
        private double temperature;
        private int humidity;
        private int pressure;

        private static int objectCount = 0;

        public Weather()
        {
            temperature = 0.0;
            humidity = 0;
            pressure = 0;
            objectCount++;
        }

        public Weather(double temperature, int humidity, int pressure)
        {
            Temperature = temperature;
            Humidity = humidity;
            Pressure = pressure;
            objectCount++; // Увеличиваем счетчик объектов при создании нового
        }

        public double Temperature
        {
            get => temperature;
            set
            {
                if (value <= -273.7)
                    throw new Exception("Температура не может быть ниже абсолютного нуля.");
                temperature = value;
            }
        }

        public int Humidity
        {
            get => humidity;
            set
            {
                if (value < 0 || value > 100)
                    throw new Exception("Влажность должна быть в диапазоне от 0 до 100.");
                humidity = value;
            }
        }

        public int Pressure
        {
            get => pressure;
            set
            {
                if (value < 0)
                    throw new Exception("Давление не может быть отрицательным.");
                pressure = value;
            }
        }

        public string ShowAttributes()
        {
            return $"Температура: {Temperature} °C, Влажность: {Humidity} %, Давление: {Pressure} мм рт. ст.";
        }

        public static int GetObjectCount()
        {
            return objectCount;
        }

        public static double CalculateDewPoint(double temperature, int humidity)
        {
            double a = 17.27;
            double b = 237.7;
            double alpha = ((a * temperature) / (b + temperature)) + Math.Log(humidity / 100.0);
            double dewPoint = (b * alpha) / (a - alpha);
            return Math.Round(dewPoint, 4);
        }

        public double GetDewPoint()
        {
            return CalculateDewPoint(Temperature, Humidity);
        }

        // Унарные операции
        public static Weather operator -(Weather w)
        {
            return new Weather(-w.Temperature, w.Humidity, w.Pressure);
        }

        public static bool operator !(Weather w)
        {
            return w.Humidity > 80;
        }

        // Операции приведения типа
        public static explicit operator bool(Weather w)
        {
            return w.Pressure > 760;
        }

        public static implicit operator double(Weather w)
        {
            // Формула для ощущаемой температуры (humindex)
            double humindex = w.Temperature + 0.5555 * (6.11 * Math.Exp(5417.7530 * ((1 / 273.16) - (1 / (w.GetDewPoint() + 273.16)))) - 10);
            return Math.Round(humindex, 2);
        }

        // Бинарные операции
        public static Weather operator -(Weather w, double d)
        {
            // Температура уменьшается на d
            return new Weather(w.Temperature - d, w.Humidity, w.Pressure);
        }

        public static Weather operator *(Weather w, double percent)
        {
            // Все параметры изменяются на заданное число процентов
            double factor = 1 + 10 / 100.0;
            return new Weather(
                w.Temperature * factor,
                (int)(w.Humidity * factor),
                (int)(w.Pressure * factor)
            );
        }

        // Метод Equals
        public override bool Equals(object obj)
        {
            if (obj is Weather other)
            {
                return Temperature == other.Temperature &&
                       Humidity == other.Humidity &&
                       Pressure == other.Pressure;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Temperature, Humidity, Pressure);
        }
    }
}