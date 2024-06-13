using ClassLibrary1;
using laba14;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private Factory factory;
        private MyCollection<Auto> myCollection;

        [TestInitialize]
        public void Initialize()
        {
            factory = Program.InitializeFactory();
            myCollection = Program.InitializeMyCollection();
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
            Assert.AreEqual(10, countResultLinq);
        }

        [TestMethod]
        public void TestQueryCount_ExtensionMethods()
        {
            var (_, countResultExt) = Program.QueryCount(myCollection);

            // ���������, ��� ���������� ��������� ������������� ����������
            Assert.AreEqual(10, countResultExt);
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