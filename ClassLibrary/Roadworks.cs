using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Roadworks //Базовый класс дорожных работ
    {
        string roadName; //название дороги
        double roadWidth; //ширина дороги
        double roadLength; //длина дороги
        double weight; //масса дорожного покрытия на 1 кв. м.
        string roadSurface; //вид дорожного покрытия
        public string RoadName //Свойство названия дороги
        {
            get { return roadName; }
            set { roadName = value; }
        } 
        public double RoadWidth //Свойство ширины дороги
        {
            get { return roadWidth; }
            set { roadWidth = value; }
        } 
        public double RoadLength //Свойство длины дороги
        {
            get { return roadLength; }
            set { roadLength = value; }
        }
        public double Weight //Свойство массы дорожного покрытия на 1 кв. м.
        {
            get { return weight; }
            set { weight = value; }
        }
        public string RoadSurface //Свойство вида дорожного покрытия
        {
            get { return roadSurface; }
            set { roadSurface = value; }
        }

        static public List<Roadworks> listWorks = new List<Roadworks>(); //Лист дорожных работ

        public Roadworks(string name, double width, double length, double weight, string surface) //Конструктор базового класса Roadworks
        {
            RoadName = name;
            RoadWidth = width;
            RoadLength = length;
            Weight = weight;
            RoadSurface = surface;
        }
        public Roadworks() { }

        virtual public double CalculateQ() //Функция расчета полной массы дорожного покрытия
        {
            return RoadWidth * RoadLength * Weight / 1000;
        }
        virtual public string GetInfo() //Функция получения информации о дорожной работе
        {
            return $"Название дороги: {RoadName}. Ширина дороги: {RoadWidth}. Длина дороги: {RoadLength}. \n" +
                $"Масса дорожного покрытия: {Weight}. Вид дорожного покрытия: {RoadSurface}";
        }
        static public void AddRoadWork(string name, string width, string length, string weight, string surface) //Перегруженный метод добавления дорожной работы в лист дорожных работ
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Название дороги не может быть пустым");
            }

            if (!double.TryParse(width, out double CorrectWidth))
            {
                throw new ArgumentException("Неверный тип данных ширины дороги");
            }

            if (!double.TryParse(length, out double CorrectLength))
            {
                throw new ArgumentException("Неверный тип данных длины дороги");
            }

            if (!double.TryParse(weight, out double CorrectWeight))
            {
                throw new ArgumentException("Неверный тип данных массы дорожного покрытия");
            }

            if (string.IsNullOrEmpty(surface))
            {
                throw new ArgumentException("Тип дорожного покрытия не может быть пустым");
            }

            AddRoadWork(name, CorrectWidth, CorrectLength, CorrectWeight, surface);
        }
        static public void AddRoadWork(Roadworks work) //Метод добавления дорожной работы в лист дорожных работ
        {
            var works = from w in listWorks where w.RoadName.ToLower() == work.RoadName.ToLower() select w;
            if (works.Count() == 0)
            {
                listWorks.Add(work);
            }
            else
            {
                throw new ArgumentException("Такое название дороги уже существует");
            }
        }
        static public void AddRoadWork(string name, double width, double length, double weight, string surface) //Перегруженный метод добавления дорожной работы в лист дорожных работ
        {
            var works = from w in listWorks where w.RoadName.ToLower() == name.ToLower() select name;
            if (works.Count() == 0)
            {
                Roadworks work = new Roadworks(name, width, length, weight, surface);
                listWorks.Add(work);
            }
            else
            {
                throw new ArgumentException("Такое название дороги уже существует");
            }
        }
        static public void DeleteRoadWork(Roadworks work) //Метод удаления дорожной работы из листа
        {
            listWorks.Remove(work);
        }
        static public void DeleteRoadWork(int index) //Перегруженный метод удаления дорожной работы из листа
        {
            listWorks.RemoveAt(index);
        }
        static public void DeleteRoadWork(string name) //Перегруженный метод удаления дорожной работы из листа
        {
            var works = from w in listWorks where w.RoadName.ToLower() == name.ToLower() select w;

            foreach (var item in works)
            {
                DeleteRoadWork(item);
                return;
            }
        }
    }
}
