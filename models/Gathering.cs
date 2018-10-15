using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SecretSanta.Extensions;

namespace SecretSanta.Models
{
    
    public class Gathering : List<Person>
    {
        private string _infoPath;
        
        public Gathering(string infoPath){
            _infoPath = infoPath;
        }
        ///<summary>
        ///Validates if the supplied gather is valid
        ///all family members of a user should be members
        ///of the gather.
        ///</summary>
        public bool Validate()
        {
            return true;  
        } 
        
        ///<summary>
        ///Assignes everyone people for secret santa. 
        ///<param ref="numberOfPlayers">The number of people to assign
        ///should be less than the number of people in the group
        ///minus the largest number of family members</param>
        ///</summary>
        public bool Shuffle(int numberOfPlayers = 1)
        {
            if(numberOfPlayers >= this.Count){
                return false;
            }
            for(var i = 0; i < numberOfPlayers; i++){
                List<Person> notAssigned = new List<Person>(this);
                foreach(Person person in this){
                    //Person should not be included with their own list
                    //Person should not be part of that family
                    //Person should not already be assigned to that person
                    //Person should not already be assigned to someone that round
                    List<Person> possibleAssignments = this
                        .Where(p => p != person
                            && !person.Family.Contains(p.Name)
                            && !person.AssignedList.Contains(p)
                            && notAssigned.Contains(p)
                        ).ToList();
                    if (!possibleAssignments.Any())
                    {
                        Console.WriteLine("Not enough possible assignments: ");
                        Console.WriteLine(person.Name);
                        return false;
                    }
                    Person assigned = possibleAssignments.PickRandom();
                    possibleAssignments.Remove(assigned);
                    notAssigned.Remove(assigned);
                    person.AssignedList.Add(assigned);
                }
            }
            return true;
        }
        
        ///<summary>
        ///Creates CSV file to display information about the group
        ///includes who should be assigned to whom
        ///</summary>
        public void CreateCsv(string filePath)
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("Person, Giftee1, Giftee2");
            foreach(Person person in this)
            {
                string line = $"{person.Name}";
                foreach (Person assigned in person.AssignedList)
                {
                    line += $", {assigned.Name}";
                }
                csv.AppendLine(line);
            }
            File.WriteAllText(filePath, csv.ToString());
        }

        /// <summary>
        /// Reads CSV file to determine the group
        /// </summary>
        public void ReadJson()
        {
            List<Person> people = new List<Person>();
            using (StreamReader r = new StreamReader(_infoPath))
            {
                string jsonString = r.ReadToEnd();
                people = JsonConvert.DeserializeObject<List<Person>>(jsonString);
            }
            this.AddRange(people);
        }
    }
    
}