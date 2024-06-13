using ClassLibrary1;
using laba14;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {

        public Factory factory = new Factory();
        public MyCollection<Auto> myCollection = new MyCollection<Auto>();

        [TestInitialize]
        public void Initialize()
        {
            Workshop workshop1 = new Workshop();
            Workshop workshop2 = new Workshop();

            workshop2.Add(new Auto("Жигули", "green", 2000, 12321242, 23));
            workshop1.Add(new Auto("Audi","blue",2010,1232142,2));
            workshop1.Add(new Auto("BMW","blue",2000,1232142,23));
            workshop1.Add(new Auto("Volvo","red",2000,1232142,23));

            workshop2.Add(new Auto("Жигули", "green", 2000, 12321242, 23));
            workshop2.Add(new Auto("Audi", "white", 2010, 1232142, 2));
            workshop2.Add(new Auto("BMW", "blue", 2000, 12321242, 23));
            workshop2.Add(new Auto("Tayota", "blue", 2011, 1232142, 223));

            factory.AddWorkshop(workshop1);
            factory.AddWorkshop(workshop2);

            myCollection.Add(new Auto("Audi", "white", 2010, 1232142, 20));
            myCollection.Add(new Auto("BMW", "green", 2010, 22333, 2));
            myCollection.Add(new Auto("Porshe", "white", 2020, 1323231, 2));
            myCollection.Add(new Auto("Mersedes", "white", 2010, 1232142, 12));
        }

        [TestMethod]
        public void TestQueryWhere_LINQ()
        {
            var (whereResultLinq, _) = Program.QueryWhere(factory);

            // Проверяем, что результат не пустой
            Assert.IsNotNull(whereResultLinq);

            // Проверяем, что все элементы имеют бренд "BMW"
            Assert.IsTrue(whereResultLinq.All(car => car.Brand == "BMW"));
        }
        [TestMethod]
        public void TestQueryAggregation_LINQ()
        {
            var (sumCostLinq, maxCostLinq, minCostLinq, avgCostLinq,
                 _, _, _, _) = Program.QueryAggregation(factory);

            // Проверяем, что значения суммы, максимума, минимума и среднего соответствуют ожидаемым
            Assert.AreEqual(43124436, sumCostLinq); // Замените на ожидаемые значения
            Assert.AreEqual(12321242, maxCostLinq); // Замените на ожидаемые значения
            Assert.AreEqual(1232142, minCostLinq); // Замените на ожидаемые значения
            Assert.AreEqual(5390554.5, avgCostLinq); // Замените на ожидаемые значения
        }

        [TestMethod]
        public void TestQueryAggregation_ExtensionMethods()
        {
            var (_, _, _, _,
                 sumCostExt, maxCostExt, minCostExt, avgCostExt) = Program.QueryAggregation(factory);

            // Проверяем, что значения суммы, максимума, минимума и среднего соответствуют ожидаемым
            Assert.AreEqual(43124436, sumCostExt); // Замените на ожидаемые значения
            Assert.AreEqual(12321242, maxCostExt); // Замените на ожидаемые значения
            Assert.AreEqual(1232142, minCostExt); // Замените на ожидаемые значения
            Assert.AreEqual(5390554.5, avgCostExt); // Замените на ожидаемые значения
        }
        [TestMethod]
        public void TestQueryWhere_ExtensionMethods()
        {
            var (_, whereResultExt) = Program.QueryWhere(factory);

            // Проверяем, что результат не пустой
            Assert.IsNotNull(whereResultExt);

            // Проверяем, что все элементы имеют бренд "BMW"
            Assert.IsTrue(whereResultExt.All(car => car.Brand == "BMW"));
        }

        [TestMethod]
        public void TestQueryCount_LINQ()
        {
            var (countResultLinq, _) = Program.QueryCount(myCollection);

            // Проверяем, что количество элементов соответствует ожидаемому
            Assert.AreEqual(4, countResultLinq);
        }

        [TestMethod]
        public void TestQueryCount_ExtensionMethods()
        {
            var (_, countResultExt) = Program.QueryCount(myCollection);

            // Проверяем, что количество элементов соответствует ожидаемому
            Assert.AreEqual(4, countResultExt);
        }

        [TestMethod]
        public void TestQueryGroupBy_LINQ()
        {
            var (groupedCarsLinq, _) = Program.QueryGroupBy(factory);

            // Проверяем, что результат группировки не пустой
            Assert.IsNotNull(groupedCarsLinq);

            // Пример проверки количества групп
            Assert.IsTrue(groupedCarsLinq.Count() > 0);
        }

        [TestMethod]
        public void TestQueryGroupBy_ExtensionMethods()
        {
            var (_, groupedCarsExt) = Program.QueryGroupBy(factory);

            // Проверяем, что результат группировки не пустой
            Assert.IsNotNull(groupedCarsExt);

            // Пример проверки количества групп
            Assert.IsTrue(groupedCarsExt.Count() > 0);
        }

        [TestMethod]
        public void TestQueryLet_LINQ()
        {
            var (carsByBrandCountLinq, _) = Program.QueryLet(factory);

            // Проверяем, что результат не пустой
            Assert.IsNotNull(carsByBrandCountLinq);

            // Пример проверки содержания
            Assert.IsTrue(carsByBrandCountLinq.Any());
        }

        [TestMethod]
        public void TestQueryLet_ExtensionMethods()
        {
            var (_, carsByBrandCountExt) = Program.QueryLet(factory);

            // Проверяем, что результат не пустой
            Assert.IsNotNull(carsByBrandCountExt);

            // Пример проверки содержания
            Assert.IsTrue(carsByBrandCountExt.Any());
        }

        [TestMethod]
        public void TestQueryJoin_LINQ()
        {
            var (joinedCarsLinq, _) = Program.QueryJoin(factory);

            // Проверяем, что результат не пустой
            Assert.IsNotNull(joinedCarsLinq);

            // Пример проверки количества соединений
            Assert.IsTrue(joinedCarsLinq.Count() > 0);
        }

        [TestMethod]
        public void TestQueryJoin_ExtensionMethods()
        {
            var (_, joinedCarsExt) = Program.QueryJoin(factory);

            // Проверяем, что результат не пустой
            Assert.IsNotNull(joinedCarsExt);

            // Пример проверки количества соединений
            Assert.IsTrue(joinedCarsExt.Count() > 0);
        }
    }
}