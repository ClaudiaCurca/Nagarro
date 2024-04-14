using iQuest.VendingMachine;
using iQuest.VendingMachine.DataAccessLayer;
using iQuest.VendingMachine.Interfaces;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private Mock<IBuyView> buyViewMock = new Mock<IBuyView>();
        private Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();

        [TestMethod]
        public void Update_WhenProductNotNull_ThenDecrementQuantity()
        {
            Product initialProduct = new Product()
            {
                Name = "Chocolate",
                Price = 9,
                Quantity = 19,
                ColumnId = 1
            };
            Product decrementedProduct = new Product()
            {
                Name = "Chocolate",
                Price = 9,
                Quantity = 18,
                ColumnId = 1
            };
            InMemoryRepository productRepository = new InMemoryRepository();
            productRepository.Update(decrementedProduct);

            Product product = productRepository.GetByColumn(1);

            Assert.AreEqual(decrementedProduct.Quantity, product.Quantity);
        }

        [TestMethod]
        public void Update_WhenProductColumnIsInvalid_ReturnsNull()
        {
            const int invalidColumn = 10000;
            Product product = new Product()
            {
                Name = "Chocolate",
                Price = 9,
                Quantity = 19,
                ColumnId = invalidColumn
            };

            InMemoryRepository productRepository = new InMemoryRepository();

            Assert.AreEqual(null,productRepository.Update(product));
        }

        [TestMethod]
        public void GetByColumn_WhenColumnIdIsValid_ReturnsProductName()
        {
            const string productNameExpected = "Chocolate";
            const int selectedColumn = 1;
            InMemoryRepository productRepository = new InMemoryRepository();

            Product product = productRepository.GetByColumn(selectedColumn);

            Assert.AreEqual(product.Name, productNameExpected);
        }

        [TestMethod]
        public void GetByColumn_WhenColumnIdIsNotValid_ReturnsNull()
        {
            const int selectedColumn = 100;
            InMemoryRepository productRepository = new InMemoryRepository();

            Product product = productRepository.GetByColumn(selectedColumn);

            Assert.AreEqual(null, product);
        }

        [TestMethod]
        public void GetAll_Allways_ReturnsListOfProducts()
        {
            List<Product> products = new List<Product>
            {
                new Product
                {
                    Name = "Chocolate",
                    Price = 9,
                    Quantity = 20,
                    ColumnId = 1
                },
                new Product
                {
                    Name = "Chips",
                    Price = 5,
                    Quantity = 7,
                    ColumnId = 2
                },
                new Product
                {
                    Name = "Still Water",
                    Price = 2,
                    Quantity = 10,
                    ColumnId = 3
                },
                new Product
                {
                    Name = "Tea",
                    Price = 2,
                    Quantity = 0,
                    ColumnId = 4
                },
                new Product
                {
                    Name = "Pepsi",
                    Price = 2,
                    Quantity = 1,
                    ColumnId = 5
                }
            };
            
            productRepositoryMock.Setup(x=>x.GetAll()).Returns(products);
            var productRepository = new InMemoryRepository();
            List<Product> listOfProducts = productRepository.GetAll();

            CollectionAssert.AreEqual(products, listOfProducts);
        }

        [TestCleanup]
        public void CleanUp()
        {
            buyViewMock.Reset();
            productRepositoryMock.Reset();
        }
    }
}
