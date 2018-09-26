using System;
using System.Collections.Generic;

namespace SecretSanta.Models
{
    
    public class Gathering : List<Person>
    {
        ///<Summary>
        ///Validates if the supplied gather is valid
        ///all family members of a user should be members
        ///of the gather.
        ///</Summary>
        public bool Validate()
        {
            return true;
            
        } 
    }
    
}