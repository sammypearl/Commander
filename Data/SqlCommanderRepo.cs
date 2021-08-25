using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;
        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Commands.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));

            }
            _context.Commands.Remove(cmd);
           // _context.SaveChanges();
        }

        public IEnumerable<Command> GetAllCommand()
        {
           return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            var command = _context.Commands
                .FirstOrDefault(x => x.Id == id);

            return command;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {
           // throw new NotImplementedException();
        }
    }
}
