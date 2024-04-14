using System;
using System.Collections.Generic;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Interfaces;

namespace iQuest.VendingMachine.UseCases
{
    internal class LookUseCase : IUseCase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IShelfView shelfView;
        private readonly IUnitOfWork unitOfWork;
        private readonly LogHelper logger;

        public LookUseCase(IShelfView shelfView, IUnitOfWork unitOfWork, IAuthenticationService authenticationService, LogHelper logger)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.shelfView = shelfView ?? throw new ArgumentNullException(nameof(shelfView));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute()
        {
            logger.Info("Look UseCase");
            List<Product> allProducts = unitOfWork.Products.GetAll();

            List<Product> productsToDisplay = new List<Product>();

            foreach (Product product in allProducts)
            {
                if (product.Quantity > 0)
                {
                    productsToDisplay.Add(product);
                }
            }
            if (authenticationService.IsUserAuthenticated == false)
            {
                shelfView.DisplayProducts(productsToDisplay);
            }
            else
            {
                shelfView.DisplayProducts(allProducts);
            }
        }
    }
}

