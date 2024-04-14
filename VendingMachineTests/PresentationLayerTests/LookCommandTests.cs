using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.PresentationLayer.Commands;
using iQuest.VendingMachine.UseCases;
using Moq;

namespace VendingMachineTests
{
    [TestClass]
    public class LookCommandTests
    {
        private Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private Mock<IUseCaseFactory> useCaseFactoryMock = new Mock<IUseCaseFactory>();

        public void Name_WhenInitializingTheUseCase_NameIsCorrect()
        {
            const string expectedName = "look";
            LookCommand lookUseCase = new LookCommand(useCaseFactoryMock.Object);

            Assert.AreEqual(expectedName, lookUseCase.Name);
        }

        [TestMethod]
        public void Description_WhenInitializingTheUseCase_DescriptionIsCorrect()
        {
            const string expectedDescription = "Looking at the products";
            LookCommand lookUseCase = new LookCommand(useCaseFactoryMock.Object);

            Assert.AreEqual(expectedDescription, lookUseCase.Description);
        }

        [TestMethod]
        public void CanExecute_WhenUserIsAuthenticated_ReturnsTrue()
        {
            authenticationServiceMock.Setup(x => x.IsUserAuthenticated).Returns(true);
            LookCommand lookUseCase = new LookCommand(useCaseFactoryMock.Object);

            Assert.AreEqual(true, lookUseCase.CanExecute);
        }

        [TestMethod]
        public void Execute_Always_CallsCreate()
        {
            List<Product> products = new List<Product> 
            {
                new Product
                    {
                        Name = "Chocolate",
                        Price = 9,
                        Quantity = 0,
                        ColumnId = 1
                    },
                new Product
                    {
                        Name = "Tea",
                        Price = 9,
                        Quantity = 10,
                        ColumnId = 2
                    }
            };
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Products.GetAll()).Returns(products);
            LookUseCase lookUseCase = new LookUseCase(Mock.Of<IShelfView>(), unitOfWorkMock.Object, authenticationServiceMock.Object,Mock.Of<LogHelper>());
            useCaseFactoryMock.Setup(x => x.Create<LookUseCase>()).Returns(lookUseCase);
            LookCommand lookCommand = new LookCommand(useCaseFactoryMock.Object);

            lookCommand.Execute();

            useCaseFactoryMock.Verify(x => x.Create<LookUseCase>(), Times.Once);
        }

        [TestCleanup]
        public void CleanUp()
        {
            authenticationServiceMock.Reset();
            useCaseFactoryMock.Reset();
        }
    }
}
