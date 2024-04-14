using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.GrandCircus.Interface
{
    internal interface IAnimal
    {
        string Name
        {
            get;
            
        }
        string SpeciesName
        {
            get;

        }
        public string MakeSound();

    }
}
