using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using ClassLibrary1;

namespace laba14
{
    public class Program
    {
        public static void Main()
        {
            // Инициализация коллекций 
            Factory factory = InitializeFactory();
            MyCollection<Auto> myCollection = InitializeMyCollection();
            // Текстовое меню
            while (true)
            {
                Console.WriteLine("Выберите коллекцию для запросов:");
                Console.WriteLine("1. Коллекция Factory и Workshops");
                Console.WriteLine("2. Коллекция MyCollection");
                Console.WriteLine("0. Выход");

                string collectionChoice = Console.ReadLine();

                switch (collectionChoice)
                {
                    case "1":
                        HandleFactoryMenu(factory);
                        break;
                    case "2":
                        HandleMyCollectionMenu(myCollection);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                        break;
                }
            }
        }

        public static void HandleFactoryMenu(Factory factory)
        {
            while (true)
            {
                Console.WriteLine("Выберите запрос для Factory и Workshops:");
                Console.WriteLine("1. Where");
                Console.WriteLine("2. Union/Except/Intersect");
                Console.WriteLine("3. Агрегирование данных (Sum, Max, Min, Average)");
                Console.WriteLine("4. Группировка данных (Group by)");
                Console.WriteLine("5. Получение нового типа (с использованием оператора let)");
                Console.WriteLine("6. Соединение данных");
                Console.WriteLine("0. Назад");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var (whereResultLinq, whereResultExt) = QueryWhere(factory);
                        PrintCars("Where (LINQ)", whereResultLinq);
                        PrintCars("Where (методы расширения)", whereResultExt);
                        break;
                    case "2":
                        HandleUnionExceptIntersect(factory);
                        break;
                    case "3":
                        var (sumCostLinq, maxCostLinq, minCostLinq, avgCostLinq, sumCostExt, maxCostExt, minCostExt, avgCostExt) = QueryAggregation(factory);
                        Console.WriteLine("Linq");
                        PrintAggregationResults(sumCostLinq, maxCostLinq, minCostLinq, avgCostLinq);
                        Console.WriteLine("Методы расширения");
                        PrintAggregationResults(sumCostExt, maxCostExt, minCostExt, avgCostExt);
                        break;
                    case "4":
                        var (groupedCarsLinq, groupedCarsExt) = QueryGroupBy(factory);
                        PrintGroupedCars("Группировка (LINQ)", groupedCarsLinq);
                        PrintGroupedCars("Группировка (методы расширения)", groupedCarsExt);
                        break;
                    case "5":
                        var (carsByBrandCountLinq, carsByBrandCountExt) = QueryLet(factory);
                        PrintBrandCounts("Let (LINQ)", carsByBrandCountLinq);
                        PrintBrandCounts("Let (методы расширения)", carsByBrandCountExt);
                        break;
                    case "6":
                        var (joinedCarsLinq, joinedCarsExt) = QueryJoin(factory);
                        PrintJoinedCars("Join (LINQ)", joinedCarsLinq);
                        PrintJoinedCars("Join (методы расширения)", joinedCarsExt);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                        break;
                }
            }
        }

        public static void HandleMyCollectionMenu(MyCollection<Auto> myCollection)
        {
            while (true)
            {
                Console.WriteLine("Выберите запрос для MyCollection:");
                Console.WriteLine("1. Where");
                Console.WriteLine("2. Count");
                Console.WriteLine("3. Агрегирование данных (Sum, Max, Min, Average)");
                Console.WriteLine("4. Группировка данных (Group by)");
                Console.WriteLine("0. Назад");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var (whereResultLinq, whereResultExt) = QueryWhere(myCollection);
                        PrintCars("Where (LINQ)", whereResultLinq);
                        PrintCars("Where (методы расширения)", whereResultExt);
                        break;
                    case "2":
                        var (countResultLinq, countResultExt) = QueryCount(myCollection);
                        Console.WriteLine($"Количество элементов (LINQ): {countResultLinq}");
                        Console.WriteLine($"Количество элементов (методы расширения): {countResultExt}");
                        break;
                    case "3":
                        var (sumCostLinq, maxCostLinq, minCostLinq, avgCostLinq, sumCostExt, maxCostExt, minCostExt, avgCostExt) = QueryAggregation(myCollection);
                        Console.WriteLine("Linq");
                        PrintAggregationResults(sumCostLinq, maxCostLinq, minCostLinq, avgCostLinq);
                        Console.WriteLine("Методы расширения");
                        PrintAggregationResults(sumCostExt, maxCostExt, minCostExt, avgCostExt);
                        break;
                    case "4":
                        var (groupedCarsLinq, groupedCarsExt) = QueryGroupBy(myCollection);
                        PrintGroupedCars("Группировка (LINQ)", groupedCarsLinq);
                        PrintGroupedCars("Группировка (методы расширения)", groupedCarsExt);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                        break;
                }
            }
        }

        public static Factory InitializeFactory()
        {
            Factory factory = new Factory();
            Workshop workshop1 = new Workshop();
            Workshop workshop2 = new Workshop();

            for (int i = 0; i < 15; i++)
            {
                workshop1.AddCar(new Auto());
                workshop2.AddCar(new Auto());
            }

            factory.AddWorkshop(workshop1);
            factory.AddWorkshop(workshop2);

            return factory;
        }

        public static MyCollection<Auto> InitializeMyCollection()
        {
            MyCollection<Auto> myCollection = new MyCollection<Auto>();
            for (int i = 0; i < 10; i++)
            {
                Auto auto = new Auto();
                auto.RandomInit();
                myCollection.Add(auto);
            }

            return myCollection;
        }

        // Методы для Factory и Workshops

        public static (List<Auto> linq, List<Auto> ext) QueryWhere(Factory factory)
        {
            var allCars = factory.Workshops.SelectMany(w => w.Cars).ToList();

            // a) С использованием LINQ запросов
            var query1a = from car in allCars
                          where car.Brand == "BMW"
                          select car;

            // b) С использованием методов расширения
            var query1b = allCars.Where(car => car.Brand == "BMW");

            return (query1a.ToList(), query1b.ToList());
        }

        public static void HandleUnionExceptIntersect(Factory factory)
        {
            var workshop3 = new Workshop();
            for (int i = 0; i < 15; i++)
            {
                workshop3.AddCar(new Auto());
            }

            factory.AddWorkshop(workshop3);

            var allCars = factory.Workshops.SelectMany(w => w.Cars).ToList();
            var allCarsUpdated = factory.Workshops.SelectMany(w => w.Cars).ToList();

            // a) С использованием LINQ запросов
            var queryUnion1 = (from car in allCars
                               select car).Union(
                              from car in allCarsUpdated
                              select car);

            var queryExcept1 = (from car in allCars
                                select car).Except(
                               from car in allCarsUpdated
                               select car);

            var queryIntersect1 = (from car in allCars
                                   select car).Intersect(
                                  from car in allCarsUpdated
                                  select car);

            // b) С использованием методов расширения
            var queryUnion = allCars.Union(allCarsUpdated);
            var queryExcept = allCars.Except(allCarsUpdated);
            var queryIntersect = allCars.Intersect(allCarsUpdated);

            Console.WriteLine("Результаты операций над множествами (LINQ):");
            Console.WriteLine("Union:");
            foreach (var car in queryUnion1)
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("\nExcept:");
            foreach (var car in queryExcept1)
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("\nIntersect:");
            foreach (var car in queryIntersect1)
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("\nРезультаты операций над множествами (методы расширения):");
            Console.WriteLine("Union:");
            foreach (var car in queryUnion)
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("\nExcept:");
            foreach (var car in queryExcept)
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("\nIntersect:");
            foreach (var car in queryIntersect)
            {
                Console.WriteLine(car.ToString());
            }
        }

        public static (double sumCostLinq, double maxCostLinq, double minCostLinq, double avgCostLinq,
                       double sumCostExt, double maxCostExt, double minCostExt, double avgCostExt) QueryAggregation(Factory factory)
        {
            var allCars = factory.Workshops.SelectMany(w => w.Cars).ToList();

            // a) С использованием LINQ запросов
            var sumCost1 = (from car in allCars select car.Cost).Sum();
            var maxCost1 = (from car in allCars select car.Cost).Max();
            var minCost1 = (from car in allCars select car.Cost).Min();
            var avgCost1 = (from car in allCars select car.Cost).Average();

            // b) С использованием методов расширения
            var sumCost2 = allCars.Sum(car => car.Cost);
            var maxCost2 = allCars.Max(car => car.Cost);
            var minCost2 = allCars.Min(car => car.Cost);
            var avgCost2 = allCars.Average(car => car.Cost);

            return (sumCost1, maxCost1, minCost1, avgCost1, sumCost2, maxCost2, minCost2, avgCost2);
        }

        public static (IEnumerable<IGrouping<string, Auto>> linq, IEnumerable<IGrouping<string, Auto>> ext) QueryGroupBy(Factory factory)
        {
            var allCars = factory.Workshops.SelectMany(w => w.Cars).ToList();

            // a) С использованием LINQ запросов
            var groupedCars1 = from car in allCars
                               group car by car.Brand;

            // b) С использованием методов расширения
            var groupedCars2 = allCars.GroupBy(car => car.Brand);

            return (groupedCars1, groupedCars2);
        }
        

        public static (IEnumerable<object> linq, IEnumerable<object> ext) QueryLet(Factory factory)
        {
            var allCars = factory.Workshops.SelectMany(w => w.Cars).ToList();

            // a) С использованием LINQ запросов
            var carsByBrandCount1 = from car in allCars
                                    let brand = car.Brand
                                    group brand by brand into brandGroup
                                    select new { Brand = brandGroup.Key, Count = brandGroup.Count() };

            // b) С использованием методов расширения
            var carsByBrandCount2 = allCars
                .Select(car => car.Brand)
                .GroupBy(brand => brand)
                .Select(group => new { Brand = group.Key, Count = group.Count() });

            return (carsByBrandCount1, carsByBrandCount2);
        }

        public static (IEnumerable<object> linq, IEnumerable<object> ext) QueryJoin(Factory factory)
        {
            var allCars = factory.Workshops.SelectMany(w => w.Cars).ToList();
            var workshopIds = factory.Workshops.Select((w, index) => new { Workshop = w, Id = index }).ToList();

            // a) С использованием LINQ запросов
            var joinedCars1 = from workshop in workshopIds
                              from car in workshop.Workshop.Cars
                              select new { WorkshopId = workshop.Id, Car = car };

            // b) С использованием методов расширения
            var joinedCars2 = workshopIds
                .SelectMany(workshop => workshop.Workshop.Cars,
                            (workshop, car) => new { WorkshopId = workshop.Id, Car = car });

            return (joinedCars1, joinedCars2);
        }

        // Методы для MyCollection

        public static (List<Auto> linq, List<Auto> ext) QueryWhere(MyCollection<Auto> myCollection)
        {
            // a) С использованием LINQ запросов
            var query1a = from car in myCollection
                          where car.Brand == "BMW"
                          select car;

            // b) С использованием методов расширения
            var query1b = myCollection.Where(car => car.Brand == "BMW");

            return (query1a.ToList(), query1b.ToList());
        }

        public static (int linq, int ext) QueryCount(MyCollection<Auto> myCollection)
        {
            // a) С использованием LINQ запросов
            var count1 = (from car in myCollection select car).Count();

            // b) С использованием методов расширения
            var count2 = myCollection.Count();

            return (count1, count2);
        }

        public static (double sumCostLinq, double maxCostLinq, double minCostLinq, double avgCostLinq,
                       double sumCostExt, double maxCostExt, double minCostExt, double avgCostExt) QueryAggregation(MyCollection<Auto> myCollection)
        {
            // a) С использованием LINQ запросов
            var sumCost1 = (from car in myCollection select car.Cost).Sum();
            var maxCost1 = (from car in myCollection select car.Cost).Max();
            var minCost1 = (from car in myCollection select car.Cost).Min();
            var avgCost1 = (from car in myCollection select car.Cost).Average();

            // b) С использованием методов расширения
            var sumCost2 = myCollection.Sum(car => car.Cost);
            var maxCost2 = myCollection.Max(car => car.Cost);
            var minCost2 = myCollection.Min(car => car.Cost);
            var avgCost2 = myCollection.Average(car => car.Cost);

            return (sumCost1, maxCost1, minCost1, avgCost1, sumCost2, maxCost2, minCost2, avgCost2);
        }

        public static (IEnumerable<IGrouping<string, Auto>> linq, IEnumerable<IGrouping<string, Auto>> ext) QueryGroupBy(MyCollection<Auto> myCollection)
        {
            // a) С использованием LINQ запросов
            var groupedCars1 = from car in myCollection
                               group car by car.Brand;

            // b) С использованием методов расширения
            var groupedCars2 = myCollection.GroupBy(car => car.Brand);

            return (groupedCars1, groupedCars2);
        }

        // Методы вывода результатов

        public static void PrintCars(string queryType, IEnumerable<Auto> cars)
        {
            Console.WriteLine($"Результаты запроса ({queryType}):");
            foreach (var car in cars)
            {
                Console.WriteLine(car.ToString());
            }
        }

        public static void PrintAggregationResults(double sumCost, double maxCost, double minCost, double avgCost)
        {
            Console.WriteLine($"Sum: {sumCost}");
            Console.WriteLine($"Max: {maxCost}");
            Console.WriteLine($"Min: {minCost}");
            Console.WriteLine($"Average: {avgCost}");
        }

        public static void PrintGroupedCars(string queryType, IEnumerable<IGrouping<string, Auto>> groupedCars)
        {
            int count = 0;
            Console.WriteLine($"Результаты группировки ({queryType}):");
            foreach (var group in groupedCars)
            {
                foreach (var car in group)
                {
                    count++;
                }
                Console.WriteLine($"Бренд: {group.Key}, Количество элементов этого типа: {count}");
                foreach (var car in group)
                {
                    Console.WriteLine(car.ToString());
                }
            }
        }

        public static void PrintBrandCounts(string queryType, IEnumerable<object> carsByBrandCount)
        {
            Console.WriteLine($"Результаты запроса (оператор let) ({queryType}):");
            foreach (var item in carsByBrandCount)
            {
                Console.WriteLine($"Бренд: {item.GetType().GetProperty("Brand").GetValue(item)}, Количество: {item.GetType().GetProperty("Count").GetValue(item)}");
            }
        }

        public static void PrintJoinedCars(string queryType, IEnumerable<object> joinedCars)
        {
            Console.WriteLine($"Результаты соединения ({queryType}):");
            foreach (var item in joinedCars)
            {
                Console.WriteLine($"WorkshopId: {item.GetType().GetProperty("WorkshopId").GetValue(item)}, Car: {item.GetType().GetProperty("Car").GetValue(item)}");
            }
        }
    }
}