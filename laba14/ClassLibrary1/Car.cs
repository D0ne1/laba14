using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Car : Auto, IToColor
    {
        private double nos; //number of seats (количество мест)
        private double maxspeed; // максимальная скорость

        public double Nos
        {
            get => nos;
            set
            {
                if (value < 0 || value > 7) nos = 0; //было принято, что в легковом авто не может быть 8 и более мест
                else nos = value;
            }
        }
        public double MaxSpeed
        {
            get => maxspeed;
            set
            {
                if (value < 0) maxspeed = 0;
                else maxspeed = value;
            }
        }

        public Car() : base() //конструктор без параметров
        {
            Nos = 0;
            MaxSpeed = 0;
        }

        public Car(string brand, string color, double yoi, double cost, double clearance, double nos, double maxspeed) : base(brand, color, yoi, cost, clearance) //конструктор с параметрами
        {
            Nos = nos;
            MaxSpeed = maxspeed;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Car a) return this.Brand == a.Brand
                    && this.Color == a.Color
                    && this.Yoi == a.Yoi
                    && this.Cost == a.Cost
                    && this.Clearance == a.Clearance
                    && this.Nos == a.Nos
                    && this.MaxSpeed == a.MaxSpeed;
            return false;
        }
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return base.ToString() + $", количество мест: {Nos}, максимальная скорость: {MaxSpeed}";
        }

        public void ChangeColor()
        {
            Color = "blue";
        }
        [ExcludeFromCodeCoverage]
        public override void Init()
        {
            base.Init();

            Console.Write("Введите количество мест: ");
            Nos = InputDoubleNumber();

            Console.Write("Введите максимальную скорость: ");
            MaxSpeed = InputDoubleNumber();
        }
        [ExcludeFromCodeCoverage]
        public override void RandomInit()
        {
            base.RandomInit();
            Nos = rnd.Next(1, 7);
            MaxSpeed = rnd.Next(100, 400);
        }
        public object Clone()
        {
            return new Car(Brand, Color, Yoi, Cost, Clearance, Nos, MaxSpeed);
        }
        [ExcludeFromCodeCoverage]
        public override void Show()
        {
            Console.WriteLine($"Car; Бренд: {Brand}, цвет: {Color}, год выпуска: {Yoi}, стоимость: {Cost}, дорожный просвет: {Clearance}, количество мест: {Nos}, максимальная скорость {MaxSpeed}");
        }
        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = base.GetHashCode();
                hash = hash * 23 + (MaxSpeed.GetHashCode() + Nos.GetHashCode());
                return hash;
            }
        }
    }
}
