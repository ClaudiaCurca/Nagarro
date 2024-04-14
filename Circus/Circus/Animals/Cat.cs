using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.GrandCircus.Animals
{

    internal class Cat:AnimalBase
    {
        public override string SpeciesName => $"Cat";

        public Cat(string name) : base(name) { }

        public override string MakeSound()
        {
            return "Miau";
        }
    }
}
