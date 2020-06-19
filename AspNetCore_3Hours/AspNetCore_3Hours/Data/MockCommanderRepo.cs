
using AspNetCore_3Hours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore_3Hours.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command { Id = 0, HowTo = "Boil An Egg", Line = "Boil Water", Platform = "Kettle and Pan" },
                new Command { Id = 1, HowTo = "Cut Bread", Line = "Get a knife", Platform = "Knife and Chopping Board" },
                new Command { Id = 2, HowTo = "Make Cup Of Tea", Line = "Place teabag in cup", Platform = "Kettle and Cup" }
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo="Boil An Egg", Line="Boil Water", Platform="Kettle and Pan"};
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
