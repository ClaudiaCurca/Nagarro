using iQuest.GrandCircus.Animals;
using iQuest.GrandCircus.Interface;
using iQuest.GrandCircus.Presentation;
using System;
using System.Collections.Generic;

namespace iQuest.GrandCircus.CircusModel
{
    internal class Circus
    {
       Arena arena;
       const string CircusName= "zombozo";

       List<IAnimal> animals = new List<IAnimal>();
        

        public Circus(Arena arena)
        {
            this.arena = arena;
            AddAnimals();
        }
        public void Perform()
        {
            arena.PresentCircus(CircusName);
            for (int i = 0; i < animals.Count; i++)
            {
                arena.AnnounceAnimal(animals[i].Name, animals[i].SpeciesName);
                arena.DisplayAnimalPerformance(animals[i].MakeSound());
            }

        }
        public void AddAnimals()
        {
            animals.Add(new Cat("Tom"));
            animals.Add(new Leon("Alex"));
            animals.Add(new Dog("Spike"));
            animals.Add(new Snake("Kaa"));
            
        }
    }
}