using System;
using System.Collections.Generic;
using System.Text;
using iQuest.GrandCircus.Interface;

namespace iQuest.GrandCircus
{
    internal abstract class AnimalBase:IAnimal
    {
        public string Name
        {
            get;

        }

        public abstract string SpeciesName
        {
            get;
        }

        protected AnimalBase(string name)
        {
            Name = name;
        }


        public abstract string MakeSound();
       
    }
}
