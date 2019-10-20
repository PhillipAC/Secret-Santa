using System;
using System.Collections.Generic;

namespace SecretSanta.Models
{
    
    public class Person 
    {
        ///<summary>
        ///The name of the person
        ///</summary>
        public string Name {get; set;}
        ///<summary>
        ///If the person is male or female
        ///</summary>
        public char Gender { get; set; }
        ///<summary>
        ///The collection of people that have been
        ///paired with the person
        ///</summary>
        public List<Person> AssignedList {get; set;}

        public Person()
        {
            AssignedList = new List<Person>();
        }
    }
    
}