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
        private int _numberOfPlayers;
        private const char _male = 'M';
        private const char _female = 'F';
        
        public Gathering(string infoPath, int secretAmount){
            _infoPath = infoPath;
            _numberOfPlayers = secretAmount;
        }
        
        ///<summary>
        ///Assignes everyone people for secret santa. 
        ///<param ref="numberOfPlayers">The number of people to assign
        ///should be less than the number of people in the group
        ///minus the largest number of family members</param>
        ///</summary>
        public bool Shuffle()
        {
            if(_numberOfPlayers >= this.Count){
                return false;
            }
            IEnumerable<Person> males = this.Where(x => x.Gender.Equals(_male));
            IEnumerable<Person> females = this.Where(x => x.Gender.Equals(_female));

            males.Shuffle();
            females.Shuffle();

            foreach(Person person in this)
            {
                List<Person> currentList = person.Gender.Equals(_male) ? males.ToList() : females.ToList();
                int index = currentList.FindIndex(x => x.Equals(person));
                for(var i = 0; i < _numberOfPlayers; i++)
                {
                    int assignedIndex = index + i + 1;
                    if(assignedIndex >= currentList.Count())
                    {
                        assignedIndex -= currentList.Count();
                    }

                    person.AssignedList.Add(currentList[assignedIndex]);
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
            string header = "Person";
            for (var i = 0; i < _numberOfPlayers; i++)
            {
                header += $", Giftee{i + 1}";
            }
            csv.AppendLine(header);
            foreach(Person person in this)
            {
                string line = $"{person.Name}";
                foreach (Person assigned in person.AssignedList)
                {
                    line += $", {assigned.Name}";
                }
                csv.AppendLine(line);
            }
            try
            {
                File.WriteAllText(filePath, csv.ToString());
            }
            catch
            {
                Console.WriteLine("There was an issue writing tot he csv file.");
            }
            finally
            {
                Console.Write(csv.ToString());
            }
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