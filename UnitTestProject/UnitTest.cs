using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using ClassLibrary;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1() //Тест метода расчёта полной массы дорожного покрытия
        {
            Roadworks work = new Roadworks();
            work.RoadWidth = 1;
            work.RoadLength = 1000;
            work.Weight = 1;

            double expected = work.RoadWidth * work.RoadLength * work.Weight / 1000; //Ожидаемый результат
            double result = work.CalculateQ(); //Полученный результат
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestMethod2() //Тест метода отображения информации
        {
            Roadworks work = new Roadworks();
            work.RoadName = "Дорога 1";
            work.RoadWidth = 1;
            work.RoadLength = 1000;
            work.Weight = 1;
            work.RoadSurface = "Бетон";
            string expected = $"Название дороги: {work.RoadName}. Ширина дороги: {work.RoadWidth}. Длина дороги: {work.RoadLength}. \n" +
                $"Масса дорожного покрытия: {work.Weight}. Вид дорожного покрытия: {work.RoadSurface}"; //Ожидаемый результат
            string result = work.GetInfo(); //Полученный результат
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestMethod3() //Тест метода добавления дорожной работы в лист
        {
            Roadworks work = new Roadworks();
            work.RoadName = "";
            work.RoadWidth = 1;
            work.RoadLength = 1000;
            work.Weight = 1;
            work.RoadSurface = "Бетон";

            string expectedMessage = "Название дороги не может быть пустым"; //Ожидаемый результат

            var exception = Assert.ThrowsException<ArgumentException>(() => //Полученный результат
            {
                Roadworks.AddRoadWork(
                    work.RoadName.ToString(),
                    work.RoadWidth.ToString(),
                    work.RoadLength.ToString(),
                    work.Weight.ToString(),
                    work.RoadSurface.ToString()
                );
            });

            Assert.AreEqual(expectedMessage, exception.Message);
        }
        [TestMethod]
        public void TestMethod4() //Тест перегруженного метода расчета полной массы дорожного покрытия
        {
            RoadCalculations work = new RoadCalculations();
            work.RoadWidth = 1;
            work.RoadLength = 1000;
            work.Weight = 1;
            work.StrengthFactor = 6;

            double expected = work.RoadWidth * work.RoadLength * work.Weight / 1000 * 1.1; //Ожидаемый результат
            double result = work.CalculateQ(); //Полученный результат
            Assert.AreEqual(expected, result);
        }
    }
}
