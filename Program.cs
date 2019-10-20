using System;
﻿using SecretSanta.Models;

namespace SecretSanta
{
    class Program
    {
        public const string _familyPath = "../../data/family.json";
        public const int _secretAmount = 3;
        
        static void Main(string[] args)
        {
            Gathering gathering = new Gathering(_familyPath, _secretAmount);
            gathering.ReadJson();
            bool success = gathering.Shuffle();
            if (success)
            {

                Console.WriteLine("Secret Santa list was successfully created");
                gathering.CreateCsv("C:/Users/Public/Documents/SecretSantaExport.csv");
            }
            else
            {
                Console.WriteLine("There was an issue creating the list.");
            }
            
            Console.WriteLine("Press any button to close.");
            Console.ReadKey();
        }
    }
}
