using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Jeep : Auto, IToColor
    {
        public bool Fulldrive { get; set; } //полный привод
        public string Roadtype { get; set; } //тип бездорожья

        string[] roadtypearr = { "Гравийные дороги", "Лесные тропы", "Песчаные дюны и пустынные местности", "Горные тропы", "Болота и топи" };

        public Jeep() : base() //конструктор без параметров
        {
            Fulldrive = false;
            Roadtype = string.Empty;
        }
        public Jeep(string brand, string color, double yoi, double cost, double clearance, bool fulldrive, string roadtype) : base(brand, color, yoi, cost, clearance) //конструктор с параметрами
        {
            Fulldrive = fulldrive;
            Roadtype = roadtype;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Jeep a) return this.Brand == a.Brand
                    && this.Color == a.Color
                    && this.Yoi == a.Yoi
                    && this.Cost == a.Cost
                    && this.Clearance == a.Clearance
                    && this.Fulldrive == a.Fulldrive
                    && this.Roadtype == a.Roadtype;
            return false;
        }
        public void ChangeColor()
        {
            Color = "purple";
        }
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return base.ToString() + $", наличие полного привода: {Fulldrive}, тип бездорожья: {Roadtype}";
        }
        [ExcludeFromCodeCoverage]
        public override void Init()
        {
            base.Init();

            Console.Write("Есть ли полный привод? (y,n): ");
            string answer = Console.ReadLine();
            if (answer == "y") Fulldrive = true;
            else Fulldrive = false;

            Console.Write("Введите тип бездорожья: ");
            Roadtype = Console.ReadLine();
        }
        public object Clone()
        {
            return new Jeep(Brand, Color, Yoi, Cost, Clearance, Fulldrive, Roadtype);
        }
        [ExcludeFromCodeCoverage]
        public override void RandomInit()
        {
            base.RandomInit();
            Fulldrive = (rnd.Next(2) == 0);
            Roadtype = roadtypearr[rnd.Next(roadtypearr.Length)];
        }
        [ExcludeFromCodeCoverage]
        public override void Show()
        {
            Console.WriteLine($"Jeep; Бренд: {Brand}, цвет: {Color}, год выпуска: {Yoi}, стоимость: {Cost}, дорожный просвет: {Clearance}, полный привод {Fulldrive}, тип бездорожья {Roadtype}");
        }
        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = base.GetHashCode();
                hash = hash * 23 + (Fulldrive.GetHashCode() + Roadtype.GetHashCode());
                return hash;
            }
        }
    }
}
