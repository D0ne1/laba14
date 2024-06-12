using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Lorry : Auto
    {
        private double capacity; //грузоподъёмность

        public double Capacity
        {
            get => capacity;
            set
            {
                if (value < 0) capacity = 0;
                else capacity = value;
            }
        }

        public Lorry() : base() //конструктор без параметров
        {
            Capacity = 0;
        }

        public Lorry(string brand, string color, double yoi, double cost, double clearance, double capacity) : base(brand, color, yoi, cost, clearance)//конструктор с параметрами
        {
            Capacity = capacity;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Lorry a) return this.Brand == a.Brand
                    && this.Color == a.Color
                    && this.Yoi == a.Yoi
                    && this.Cost == a.Cost
                    && this.Clearance == a.Clearance
                    && this.Capacity == a.Capacity;
            return false;
        }
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return base.ToString() + $", грузоподъёмность {Capacity}";
        }
        [ExcludeFromCodeCoverage]
        public override void Init()
        {
            base.Init();

            Console.Write("Введите грузоподъмность: ");
            Capacity = InputDoubleNumber();
        }
        [ExcludeFromCodeCoverage]
        public override void RandomInit()
        {
            base.RandomInit();
            Capacity = rnd.Next(100, 200);
        }
        [ExcludeFromCodeCoverage]
        public override void Show()
        {
            Console.WriteLine($"Lorry; Бренд: {Brand}, цвет: {Color}, год выпуска: {Yoi}, стоимость: {Cost}, дорожный просвет: {Clearance}, грузоподъёмность {Capacity}");
        }
        public object Clone()
        {
            return new Lorry(Brand, Color, Yoi, Cost, Clearance, Capacity);
        }
        public Auto Get
        {
            get => new Auto(brand, color, yoi, cost, clearance);
        }
        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = base.GetHashCode();
                hash = hash * 23 + Capacity.GetHashCode();
                return hash;
            }
        }
    }
}
