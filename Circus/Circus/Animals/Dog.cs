using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.GrandCircus.Animals
{
    internal class Dog : AnimalBase
    {
        
        public override string SpeciesName => $"Dog";

        public Dog(string name) : base(name) { }

        public override string MakeSound()
        {
            return "Ham";
        }

    }
}
