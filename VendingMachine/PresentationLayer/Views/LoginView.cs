using System;
using iQuest.VendingMachine.Interfaces;

namespace iQuest.VendingMachine.PresentationLayer.Views
{
    internal class LoginView : DisplayBase, ILoginView
    {
        public string AskForPassword()
        {
            Console.WriteLine();
            Display("Type the admin password: ", ConsoleColor.Cyan);
            return Console.ReadLine();
        }
    }
}