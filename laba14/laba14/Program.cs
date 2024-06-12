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
                        var whereResult = QueryWhere(factory);
                        PrintCars("Where", whereResult);
                        break;
                    case "2":
                        HandleUnionExceptIntersect(factory);
                        break;
                    case "3":
                        var (sumCost, maxCost, minCost, avgCost) = QueryAggregation(factory);
                        PrintAggregationResults(sumCost, maxCost, minCost, avgCost);
                        break;
                    case "4":
                        var groupedCars = QueryGroupBy(factory);
                        PrintGroupedCars(groupedCars);
                        break;
                    case "5":
                        var carsByBrandCount = QueryLet(factory);
                        PrintBrandCounts(carsByBrandCount);
                        break;
                    case "6":
                        var joinedCars = QueryJoin(factory);
                        PrintJoinedCars(joinedCars);
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
                        var whereResult = QueryWhere(myCollection);
                        PrintCars("Where", whereResult);
                        break;
                    case "2":
                        var countResult = QueryCount(myCollection);
                        Console.WriteLine($"Количество элементов: {countResult}");
                        break;
                    case "3":
                        var (sumCost, maxCost, minCost, avgCost) = QueryAggregation(myCollection);
                        PrintAggregationResults(sumCost, maxCost, minCost, avgCost);
                        break;
                    case "4":
                        var groupedCars = QueryGroupBy(myCollection);
                        PrintGroupedCars(groupedCars);
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
                workshop1.AddCar(new Auto("BMW", "Red", 2015, 20000, 250));
                workshop2.AddCar(new Auto("Audi", "Blue", 2016, 25000, 240));
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
                myCollection.Add(new Auto("Brand" + (i % 3), "Color" + i, 2000 + i, i * 1000, 150 + i));
            }

            return myCollection;
        }

        // Methods for Factory and Workshops

        public static List<Auto> QueryWhere(Factory factory)
        {
            var allCars = factory.Workshops.SelectMany(w => w.Cars).ToList();

            // a) С использованием LINQ запросов
            var query1a = from car in allCars
                          where car.Brand == "BMW"
                          select car;

            // b) С использованием методов расширения
            var query1b = allCars.Where(car => car.Brand == "BMW");

            return query1a.ToList(); // Или query1b.ToList()
        }

        public static void HandleUnionExceptIntersect(Factory factory)
        {
            var workshop3 = new Workshop();
            for (int i = 0; i < 15; i++)
            {
                workshop3.AddCar(new Car());
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

            Console.WriteLine("Результаты операций над множествами:");
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
        public static (double sumCost, double maxCost, double minCost, double avgCost) QueryAggregation(Factory factory)
        {
            var allCars = factory.Workshops.SelectMany(w => w.Cars).ToList();

            // a) С использованием LINQ запросов
            var sumCost1 = (from car in allCars
                            select car.Cost).Sum();

            var maxCost1 = (from car in allCars
                            select car.Cost).Max();

            var minCost1 = (from car in allCars
                            select car.Cost).Min();

            var avgCost1 = (from car in allCars
                            select car.Cost).Average();

            // b) С использованием методов расширения
            var sumCost2 = allCars.Sum(car => car.Cost);
            var maxCost2 = allCars.Max(car => car.Cost);
            var minCost2 = allCars.Min(car => car.Cost);
            var avgCost2 = allCars.Average(car => car.Cost);

            return (sumCost1, maxCost1, minCost1, avgCost1); // Или (sumCost2, maxCost2, minCost2, avgCost2)
        }

        public static IEnumerable<IGrouping<string, Auto>> QueryGroupBy(Factory factory)
        {
            var allCars = factory.Workshops.SelectMany(w => w.Cars).ToList();

            // a) С использованием LINQ запросов
            var groupedCarsByBrand1 = from car in allCars
                                      group car by car.Brand into carGroup
                                      select carGroup;

            // b) С использованием методов расширения
            var groupedCarsByBrand2 = allCars.GroupBy(car => car.Brand);

            return groupedCarsByBrand1; // Или groupedCarsByBrand2
        }

        public static IEnumerable<dynamic> QueryLet(Factory factory)
        {
            var allCars = factory.Workshops.SelectMany(w => w.Cars).ToList();

            // a) С использованием LINQ запросов
            var carsByBrandCount1 = from car in allCars
                                    group car by car.Brand into carGroup
                                    let count = carGroup.Count()
                                    select new { Brand = carGroup.Key, Count = count };

            // b) С использованием методов расширения
            var carsByBrandCount2 = allCars.GroupBy(car => car.Brand)
                                           .Select(carGroup => new { Brand = carGroup.Key, Count = carGroup.Count() });

            return carsByBrandCount1; // Или carsByBrandCount2
        }
        public static IEnumerable<dynamic> QueryJoin(Factory factory)
        {
            var workshop1 = factory.Workshops.Dequeue(); 
            var workshop2 = factory.Workshops.Dequeue(); 

            // a) С использованием LINQ запросов
            var joinedCars1 = from car1 in workshop1.Cars
                              join car2 in workshop2.Cars
                              on car1.Brand equals car2.Brand
                              select new { Brand = car1.Brand, Cost1 = car1.Cost, Cost2 = car2.Cost };

            // b) С использованием методов расширения
            var joinedCars2 = workshop1.Cars.Join(workshop2.Cars,
                                                  car1 => car1.Brand,
                                                  car2 => car2.Brand,
                                                  (car1, car2) => new { Brand = car1.Brand, Cost1 = car1.Cost, Cost2 = car2.Cost });

            return joinedCars1; // Или joinedCars2
        }

        public static List<Auto> QueryWhere(MyCollection<Auto> collection)
        {
            // a) С использованием LINQ запросов
            var query1a = from car in collection
                          where car.Brand.Contains("Brand1")
                          select car;

            // b) С использованием методов расширения
            var query1b = collection.Where(car => car.Brand.Contains("Brand1"));

            return query1a.ToList(); // Или query1b.ToList()
        }

        public static int QueryCount(MyCollection<Auto> collection)
        {
            // a) С использованием LINQ запросов
            var count1a = (from car in collection
                           select car).Count();

            // b) С использованием методов расширения
            var count1b = collection.Count();

            return count1a; // Или count1b
        }

        public static (double sumCost, double maxCost, double minCost, double avgCost) QueryAggregation(MyCollection<Auto> collection)
        {
            // a) С использованием LINQ запросов
            var sumCost1 = (from car in collection select car.Cost).Sum();
            var maxCost1 = (from car in collection select car.Cost).Max();
            var minCost1 = (from car in collection select car.Cost).Min();
            var avgCost1 = (from car in collection select car.Cost).Average();

            // b) С использованием методов расширения
            var sumCost2 = collection.Sum(car => car.Cost);
            var maxCost2 = collection.Max(car => car.Cost);
            var minCost2 = collection.Min(car => car.Cost);
            var avgCost2 = collection.Average(car => car.Cost);

            return (sumCost1, maxCost1, minCost1, avgCost1); // Или (sumCost2, maxCost2, minCost2, avgCost2)
        }

        public static IEnumerable<IGrouping<string, Auto>> QueryGroupBy(MyCollection<Auto> collection)
        {
            // a) С использованием LINQ запросов
            var groupedCarsByBrand1 = from car in collection
                                      group car by car.Brand into carGroup
                                      select carGroup;

            // b) С использованием методов расширения
            var groupedCarsByBrand2 = collection.GroupBy(car => car.Brand);

            return groupedCarsByBrand1; 
        }

        public static void PrintCars(string queryName, List<Auto> cars)
        {
            Console.WriteLine($"Результаты запроса {queryName}:");
            foreach (Auto car in cars)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine();
        }

        public static void PrintAggregationResults(double sumCost, double maxCost, double minCost, double avgCost)
        {
            Console.WriteLine("Результаты агрегирования данных:");
            Console.WriteLine($"Сумма стоимости всех автомобилей: {sumCost}");
            Console.WriteLine($"Максимальная стоимость автомобиля: {maxCost}");
            Console.WriteLine($"Минимальная стоимость автомобиля: {minCost}");
            Console.WriteLine($"Средняя стоимость автомобиля: {avgCost}");
            Console.WriteLine();
        }

        public static void PrintGroupedCars(IEnumerable<IGrouping<string, Auto>> groupedCars)
        {
            Console.WriteLine("Результаты группировки данных:");
            foreach (var group in groupedCars)
            {
                Console.WriteLine($"Бренд: {group.Key}");
                foreach (var car in group)
                {
                    Console.WriteLine($"- {car.ToString()}");
                }
            }
            Console.WriteLine();
        }

        public static void PrintBrandCounts(IEnumerable<dynamic> carsByBrandCount)
        {
            Console.WriteLine("Результаты запроса с оператором let:");
            foreach (var item in carsByBrandCount)
            {
                Console.WriteLine($"Бренд: {item.Brand}, Количество: {item.Count}");
            }
            Console.WriteLine();
        }

        public static void PrintJoinedCars(IEnumerable<dynamic> joinedCars)
        {
            Console.WriteLine("Результаты соединения данных:");
            foreach (var item in joinedCars)
            {
                Console.WriteLine($"Бренд: {item.Brand}, Стоимость 1: {item.Cost1}, Стоимость 2: {item.Cost2}");
            }
            Console.WriteLine();
        }
    }
}