﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Factory
    {
        public Queue<Workshop> Workshops { get; set; }

        public Factory()
        {
            Workshops = new Queue<Workshop>();
        }

        public void AddWorkshop(Workshop workshop)
        {
            Workshops.Enqueue(workshop);
        }
    }

    public class Workshop
    {
        public string Name { get; set; }
        public List<Auto> Cars { get; set; }

        public Workshop()
        {
            Cars = new List<Auto>();
        }

        public void AddCar(Auto car)
        {
            car.RandomInit();
            Cars.Add(car);
        }
        public void Add(Auto auto)
        {
            Cars.Add(auto);
        }
    }
}
