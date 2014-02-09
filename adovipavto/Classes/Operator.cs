using System;
using System.Runtime.Serialization;
using adovipavto.Enums;

namespace adovipavto.Classes
{
    public class Operator
    {
        public Operator(Rights rights, int id, string name, string lastName)
        {
            Rights = rights;
            Id = id;
            Name = name;
            LastName = lastName;
        }

        public Rights Rights { get; private set; }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public int SessionId { get; private set; }
    }

}