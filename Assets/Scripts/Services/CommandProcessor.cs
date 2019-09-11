using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Services.Commands;
using Zenject;

namespace Services
{
    public class CommandProcessor : IFixedTickable
    {
		
		private readonly SortedList<int, IGameCommand> _commands = new SortedList<int, IGameCommand>(); 
        private int _commandIdx;

        private readonly List<IGameCommand> _lateTickCommands = new List<IGameCommand>();

        public void AddCommand(IGameCommand command)
        {
            _commands.Add(_commandIdx, command);
            _commandIdx++;
        }

        public void AddLateCommand(IGameCommand command)
        {
            _lateTickCommands.Add(command);
        }
			
        public void FixedTick()
        {
            RunActiveCommands();
            RunLateTickCommands();
        }

        private void RunLateTickCommands()
        {
            var activeCommands = _lateTickCommands.ToArray();

            foreach (var gameCommand in activeCommands)
            {
                RunCommand(gameCommand);
            }
        }

        private void RunActiveCommands()
        {
            var activeCommands = _commands.Values.ToArray();

            foreach (var gameCommand in activeCommands)
            {
                RunCommand(gameCommand);
            }
        }

        private void RunCommand(IGameCommand gameCommand)
        {
            var status = gameCommand.FixedStep();
            if (status == GameCommandStatus.InProgress) return; //if it is no longer in progress remove it
            RemoveCommand(gameCommand);
        }

        private void RemoveCommand(IGameCommand gameCommand)
        {
            var index = _commands.IndexOfValue(gameCommand);
            _commands.RemoveAt(index);
            gameCommand.Dispose();
        }

        private void RemoveLateCommand(IGameCommand gameCommand)
        {
            var index = _lateTickCommands.IndexOf(gameCommand);
            _commands.RemoveAt(index);
            gameCommand.Dispose();
        }

        public void RemoveAllCommands(){

            foreach (var gameCommand in _commands.Values)
            {
                gameCommand.Dispose();
            }
            _commands.Clear();
		}
    }
}