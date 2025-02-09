using Microsoft.VisualStudio.TestPlatform.Utilities;
using лаба_9;
namespace TestProject
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestTemperatureSetter()
        {
            var weather = new Weather();
            weather.Temperature = 25.0;
            Assert.AreEqual(25.0, weather.Temperature);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestTemperatureSetterBelowAbsoluteZero()
        {
            var weather = new Weather();
            weather.Temperature = -300.0;
        }

        [TestMethod]
        public void TestHumiditySetter()
        {
            var weather = new Weather();
            weather.Humidity = 50;
            Assert.AreEqual(50, weather.Humidity);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestHumiditySetterOutOfRange()
        {
            var weather = new Weather();
            weather.Humidity = 150;
        }

        [TestMethod]
        public void TestPressureSetter()
        {
            var weather = new Weather();
            weather.Pressure = 760;
            Assert.AreEqual(760, weather.Pressure);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestPressureSetterNegative()
        {
            var weather = new Weather();
            weather.Pressure = -10;
        }

        [TestMethod]
        public void TestShowAttributes()
        {
            var weather = new Weather(25.0, 50, 760);
            string expected = "Температура: 25 °C, Влажность: 50 %, Давление: 760 мм рт. ст.";
            Assert.AreEqual(expected, weather.ShowAttributes());
        }

        [TestMethod]
        public void TestGetDewPoint()
        {
            var weather = new Weather(25.0, 50, 760);
            double dewPoint = weather.GetDewPoint();
            Assert.IsTrue(dewPoint > 0);
        }

        [TestMethod]
        public void TestUnaryOperatorMinus()
        {
            var weather = new Weather(25.0, 50, 760);
            var invertedWeather = -weather;
            Assert.AreEqual(-25.0, invertedWeather.Temperature);
        }

        [TestMethod]
        public void TestUnaryOperatorNot()
        {
            var weather = new Weather(25.0, 85, 760);
            Assert.IsTrue(!weather);
        }

        [TestMethod]
        public void TestExplicitOperatorBool()
        {
            var weather = new Weather(25.0, 50, 770);
            Assert.IsTrue((bool)weather);
        }

        [TestMethod]
        public void TestImplicitOperatorDouble()
        {
            var weather = new Weather(25.0, 50, 760);
            double humindex = weather;
            Assert.IsTrue(humindex > 0);
        }

        [TestMethod]
        public void TestEquals()
        {
            var weather1 = new Weather(25.0, 50, 760);
            var weather2 = new Weather(25.0, 50, 760);
            Assert.IsTrue(weather1.Equals(weather2));
        }

        [TestMethod]
        public void TestGetHashCode()
        {
            var weather1 = new Weather(25.0, 50, 760);
            var weather2 = new Weather(25.0, 50, 760);
            Assert.AreEqual(weather1.GetHashCode(), weather2.GetHashCode());
        }
        [TestMethod]
        public void TestDefaultConstructor()
        {
            var weatherArray = new WeatherArray();
            Assert.AreEqual(0, weatherArray.Length);
        }

        [TestMethod]
        public void TestParameterizedConstructor()
        {
            var weatherArray = new WeatherArray(5);
            Assert.AreEqual(5, weatherArray.Length);
        }

        [TestMethod]
        public void TestCopyConstructor()
        {
            var originalArray = new WeatherArray(5);
            var copiedArray = new WeatherArray(originalArray);
            Assert.AreEqual(originalArray.Length, copiedArray.Length);
            for (int i = 0; i < originalArray.Length; i++)
            {
                Assert.AreEqual(originalArray[i].Temperature, copiedArray[i].Temperature);
                Assert.AreEqual(originalArray[i].Humidity, copiedArray[i].Humidity);
                Assert.AreEqual(originalArray[i].Pressure, copiedArray[i].Pressure);
            }
        }

        [TestMethod]
        public void TestIndexerGet()
        {
            var weatherArray = new WeatherArray(5);

            var weather = weatherArray[0];

            Assert.IsNotNull(weather);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestIndexerGetOutOfRange()
        {
            var weatherArray = new WeatherArray(5);
            var weather = weatherArray[10];
        }

        [TestMethod]
        public void TestIndexerSet()
        {
            var weatherArray = new WeatherArray(5);
            var newWeather = new Weather(30.0, 50, 770);
            weatherArray[0] = newWeather;
            Assert.AreEqual(newWeather.Temperature, weatherArray[0].Temperature);
            Assert.AreEqual(newWeather.Humidity, weatherArray[0].Humidity);
            Assert.AreEqual(newWeather.Pressure, weatherArray[0].Pressure);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestIndexerSetOutOfRange()
        {
            var weatherArray = new WeatherArray(5);
            var newWeather = new Weather(30.0, 50, 770);
            weatherArray[10] = newWeather;
        }

    }
}

