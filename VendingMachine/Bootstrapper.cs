using Autofac;
using iQuest.VendingMachine.Interfaces;
using iQuest.VendingMachine.PresentationLayer;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace iQuest.VendingMachine
{
    internal class Bootstrapper
    {
        public void Run()
        {
            VendingMachineApplication vendingMachineApplication = BuildApplication();
            vendingMachineApplication.Run();
        }

        private static VendingMachineApplication BuildApplication()
        {
            var container = ContainerConfig.GetContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                return scope.Resolve<VendingMachineApplication>();
            }

        }

    }
}