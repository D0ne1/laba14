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

            workshop2.Add(new Auto("������", "green", 2000, 12321242, 23));
            workshop1.Add(new Auto("Audi","blue",2010,1232142,2));
            workshop1.Add(new Auto("BMW","blue",2000,1232142,23));
            workshop1.Add(new Auto("Volvo","red",2000,1232142,23));

            workshop2.Add(new Auto("������", "green", 2000, 12321242, 23));
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

            // ���������, ��� ��������� �� ������
            Assert.IsNotNull(whereResultLinq);

            // ���������, ��� ��� �������� ����� ����� "BMW"
            Assert.IsTrue(whereResultLinq.All(car => car.Brand == "BMW"));
        }
        [TestMethod]
        public void TestQueryAggregation_LINQ()
        {
            var (sumCostLinq, maxCostLinq, minCostLinq, avgCostLinq,
                 _, _, _, _) = Program.QueryAggregation(factory);

            // ���������, ��� �������� �����, ���������, �������� � �������� ������������� ���������
            Assert.AreEqual(43124436, sumCostLinq); // �������� �� ��������� ��������
            Assert.AreEqual(12321242, maxCostLinq); // �������� �� ��������� ��������
            Assert.AreEqual(1232142, minCostLinq); // �������� �� ��������� ��������
            Assert.AreEqual(5390554.5, avgCostLinq); // �������� �� ��������� ��������
        }

        [TestMethod]
        public void TestQueryAggregation_ExtensionMethods()
        {
            var (_, _, _, _,
                 sumCostExt, maxCostExt, minCostExt, avgCostExt) = Program.QueryAggregation(factory);

            // ���������, ��� �������� �����, ���������, �������� � �������� ������������� ���������
            Assert.AreEqual(43124436, sumCostExt); // �������� �� ��������� ��������
            Assert.AreEqual(12321242, maxCostExt); // �������� �� ��������� ��������
            Assert.AreEqual(1232142, minCostExt); // �������� �� ��������� ��������
            Assert.AreEqual(5390554.5, avgCostExt); // �������� �� ��������� ��������
        }
        [TestMethod]
        public void TestQueryWhere_ExtensionMethods()
        {
            var (_, whereResultExt) = Program.QueryWhere(factory);

            // ���������, ��� ��������� �� ������
            Assert.IsNotNull(whereResultExt);

            // ���������, ��� ��� �������� ����� ����� "BMW"
            Assert.IsTrue(whereResultExt.All(car => car.Brand == "BMW"));
        }

        [TestMethod]
        public void TestQueryCount_LINQ()
        {
            var (countResultLinq, _) = Program.QueryCount(myCollection);

            // ���������, ��� ���������� ��������� ������������� ����������
            Assert.AreEqual(4, countResultLinq);
        }

        [TestMethod]
        public void TestQueryCount_ExtensionMethods()
        {
            var (_, countResultExt) = Program.QueryCount(myCollection);

            // ���������, ��� ���������� ��������� ������������� ����������
            Assert.AreEqual(4, countResultExt);
        }

        [TestMethod]
        public void TestQueryGroupBy_LINQ()
        {
            var (groupedCarsLinq, _) = Program.QueryGroupBy(factory);

            // ���������, ��� ��������� ����������� �� ������
            Assert.IsNotNull(groupedCarsLinq);

            // ������ �������� ���������� �����
            Assert.IsTrue(groupedCarsLinq.Count() > 0);
        }

        [TestMethod]
        public void TestQueryGroupBy_ExtensionMethods()
        {
            var (_, groupedCarsExt) = Program.QueryGroupBy(factory);

            // ���������, ��� ��������� ����������� �� ������
            Assert.IsNotNull(groupedCarsExt);

            // ������ �������� ���������� �����
            Assert.IsTrue(groupedCarsExt.Count() > 0);
        }

        [TestMethod]
        public void TestQueryLet_LINQ()
        {
            var (carsByBrandCountLinq, _) = Program.QueryLet(factory);

            // ���������, ��� ��������� �� ������
            Assert.IsNotNull(carsByBrandCountLinq);

            // ������ �������� ����������
            Assert.IsTrue(carsByBrandCountLinq.Any());
        }

        [TestMethod]
        public void TestQueryLet_ExtensionMethods()
        {
            var (_, carsByBrandCountExt) = Program.QueryLet(factory);

            // ���������, ��� ��������� �� ������
            Assert.IsNotNull(carsByBrandCountExt);

            // ������ �������� ����������
            Assert.IsTrue(carsByBrandCountExt.Any());
        }

        [TestMethod]
        public void TestQueryJoin_LINQ()
        {
            var (joinedCarsLinq, _) = Program.QueryJoin(factory);

            // ���������, ��� ��������� �� ������
            Assert.IsNotNull(joinedCarsLinq);

            // ������ �������� ���������� ����������
            Assert.IsTrue(joinedCarsLinq.Count() > 0);
        }

        [TestMethod]
        public void TestQueryJoin_ExtensionMethods()
        {
            var (_, joinedCarsExt) = Program.QueryJoin(factory);

            // ���������, ��� ��������� �� ������
            Assert.IsNotNull(joinedCarsExt);

            // ������ �������� ���������� ����������
            Assert.IsTrue(joinedCarsExt.Count() > 0);
        }
    }
}