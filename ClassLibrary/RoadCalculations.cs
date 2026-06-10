using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class RoadCalculations : Roadworks //Класс-наследник: вычисления дорожных работ
    {
        public static Stack<RoadCalculations> listStack = new Stack<RoadCalculations>(); //Лист более важных дорожных работ (если кол-во дней до окончания работы < 10) 
        int strengthFactor; //коэффициент прочности
        string weather; //погодные условия
        int countDays; //Количество дней до окончания работы

        public int StrengthFactor //Свойство коэффициента прочности
        {
            get { return strengthFactor; }
            set { strengthFactor = value; }
        }
        public string Weather //Свойство погодных условий
        {
            get { return weather; }
            set { weather = value; }
        }
        public int CountDays //Свойство количества дней до окончания работы
        {
            get { return countDays; }
            set { countDays = value; }
        }
        public RoadCalculations(string name, double width, double length, double weight, string surface, int factor, string weather, int count) : base(name, width, length, weight, surface) //Констуктор класса-наследника
        {
            StrengthFactor = factor;
            Weather = weather;
            CountDays = count;

            if (CountDays >= 0 && CountDays <= 10)
            {
                listStack.Push(this);
            }
        }
        public RoadCalculations() { }
        public override double CalculateQ() //Переопределённый метод расчета полной массы дорожного покрытия
        {
            if (StrengthFactor >= 5 && StrengthFactor <= 8)
            {
                return base.CalculateQ() * 1.1;
            }
            else if (StrengthFactor == 3 || StrengthFactor == 4 || StrengthFactor == 9 || StrengthFactor == 10)
            {
                return base.CalculateQ() * 1.6;
            }
            else
            {
                return base.CalculateQ() * 1.9;
            }
        }
        public override string GetInfo() //Переопределённый метод получения информации
        {
            return base.GetInfo() + $"\nКоэффициент прочноости: {StrengthFactor}. Погодные условия: {Weather}." +
                $"\nКоличество дней до окончания работы: {CountDays}";
        }
    }
}
