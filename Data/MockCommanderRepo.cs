using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
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

        public IEnumerable<Command> GetAllCommand()
        {
            var commands = new List<Command>
            {
                new Command{ Id = 0, HowTo = "Boil an egg", 
                Line = "Boil water", Platform = "Kettle & Pan"},
                new Command{ Id = 1, HowTo = "Cut Bread",
                Line = "Get a Knife", Platform = "Bread & Knife"},
                new Command{ Id = 2, HowTo = "Fried an egg",
                Line = "Get a frying pan", Platform = "Eggs,Pan & hot plate"},
                new Command{ Id = 3, HowTo = "make a cup of coffee",
                Line = "place 2 teaspoon of " +
                "coffee in a cup of hot water", Platform = "Cup & Coffee"}
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id=0, HowTo="Boil an egg", 
                Line ="Boil water", Platform="Kettle & Pan"};
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            // throw new NotImplementedException();
        }
    }
} 
