using System;
﻿using SecretSanta.Models;

namespace SecretSanta
{
    class Program
    {
        public const string _familyPath = "../../data/family.json";
        public const int _secretAmount = 2;
        
        static void Main(string[] args)
        {
            Gathering gathering = new Gathering(_familyPath);
            gathering.ReadJson();
            bool success = false;
            if(!gathering.Validate())
            {
                Console.WriteLine("Make sure every family member is included in the Secret Santa.");
            }
            else
            {
                while (success == false)
                {
                    success = gathering.Shuffle(_secretAmount);
                }
                Console.WriteLine("Secret Santa list was successfully created");
            }
            gathering.CreateCsv("C:/Users/Public/Documents");
            Console.WriteLine("Press any button to close.");
            Console.ReadKey();
        }
    }
}
