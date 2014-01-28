using adovipavto.Enums;

namespace adovipavto.Classes
{
    internal class Operator
    {
        public Operator(Rights rights, int id, string name, string lastName, int sessionId)
        {
            Rights = rights;
            Id = id;
            Name = name;
            LastName = lastName;
            SessionId = sessionId;
        }

        public Rights Rights { get; private set; }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public int SessionId { get; private set; }

    }

}