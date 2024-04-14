using Autofac;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.DataAccessLayer;
using iQuest.VendingMachine.FileFormat;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.Payment;
using iQuest.VendingMachine.PaymentMethods;
using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.ReportRepository;
using iQuest.VendingMachine.UseCases;
using log4net;
using System.Reflection;

namespace iQuest.VendingMachine
{
    internal static class ContainerConfig
    {
        private static IContainer container;
        public static IContainer GetContainer()
        {
            container ??= Configure();
            return container;
        }
        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterType<LogHelper>();
            builder.RegisterType<VendingDbContext>().AsSelf()
                    .InstancePerLifetimeScope();
            builder.RegisterType<EntityFrameworkRepository>().As<IProductRepository>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();

            builder.RegisterType<CashPaymentTerminal>().As<ICashPaymentTerminal>();
            builder.RegisterType<CardPaymentTerminal>().As<ICardPaymentTerminal>();
            builder.RegisterType<CardPayment>()
                   .As<IPaymentAlgorithm>()
                   .WithParameter("cardPaymentTerminal", new CardPaymentTerminal())
                   .WithParameter("PaymentMethod", 2);
            builder.RegisterType<CashPayment>()
                   .As<IPaymentAlgorithm>()
                   .WithParameter("cashPaymentTerminal", new CashPaymentTerminal())
                   .WithParameter("PaymentMethod", 1);

            builder.RegisterAssemblyTypes(dataAccess)
                   .Where(t => t.Name.EndsWith("View"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(dataAccess)
                   .Where(t => t.Name.EndsWith("Command"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(dataAccess)
                   .Where(t => t.Name.EndsWith("UseCase"))
                   .InstancePerLifetimeScope();



            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().SingleInstance();

            builder.RegisterType<PaymentUseCase>().As<IPaymentUseCase>();

            builder.RegisterType<UseCaseFactory>().As<IUseCaseFactory>();


            builder.RegisterType<SalesRepository>().As<ISalesRepository>().SingleInstance();
            builder.RegisterType<StockReportRepository>();
            builder.RegisterType<SalesReportRepository>();
            builder.RegisterType<VolumeReportRepository>();

            builder.RegisterType<XMLFile<Stock>>().As<IFileCreator<Stock>>();
            builder.RegisterType<JsonFile<Sales>>().As<IFileCreator<Sales>>();
            builder.RegisterType<JsonFile<SalesVolume>>().As<IFileCreator<SalesVolume>>();


            builder.RegisterType<PaymentMethod>()
                   .WithParameter("name", "cash")
                   .WithParameter("id", "1");
            builder.RegisterType<PaymentMethod>()
                   .WithParameter("name", "card")
                   .WithParameter("id", "2");

            //builder.RegisterType<LiteDbRepository>().As<IProductRepository>();
            //builder.RegisterType<MySQLRepository>().As<IProductRepository>();

            builder.RegisterType<VendingMachineApplication>();


            return builder.Build();
        }
    }
}
