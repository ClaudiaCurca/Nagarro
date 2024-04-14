using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.GrandCircus.Animals
{
    internal class Leon:AnimalBase
    {
        public override string SpeciesName => $"Leon";

        public Leon(string name) : base(name) { }

        public override string MakeSound()
        {
            return "Roar";
        }
    }
}
