using System;
﻿using SecretSanta.Models;

namespace SecretSanta
{
    class Program
    {
        public const string _familyPath = "/data/family.json";
        public const int _secretAmount = 2;
        
        static void Main(string[] args)
        {
            Gathering gathering = new Gathering(_familyPath);
            bool success = false;
            if(!gathering.Validate())
            {
                Console.WriteLine("Make sure every family member is included in the Secret Santa.");
            }
            else
            {
                success = gathering.Shuffle(_secretAmount);
                if(success)
                {
                    gathering.CreateCsv();
                    Console.WriteLine("Secret Santa list was successfully created");
                }
                else
                {
                    Console.WriteLine("The amount specified are higher or equal to the number of players.");
                }
            }
            Console.WriteLine("Press any button to close.");
            Console.ReadKey();
        }
    }
}
