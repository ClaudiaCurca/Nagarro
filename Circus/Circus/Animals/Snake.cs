using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.GrandCircus.Animals
{
    internal class Snake:AnimalBase
    {
       
        public override string SpeciesName => $"Snake";

        public Snake(string name) : base(name) { }

        public override string MakeSound()
        {
            return "SSSSSS";
        }
    }
}
