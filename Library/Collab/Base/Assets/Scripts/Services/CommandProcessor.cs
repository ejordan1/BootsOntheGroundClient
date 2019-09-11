using System.Collections;
using System.Collections.Generic;
using Services.Commands;
using Zenject;

namespace Services
{
    public class CommandProcessor : IFixedTickable
    {
		
		private readonly SortedList<int, IGameCommand> _commands = new SortedList<int, IGameCommand>(); 
        private int _commandIdx;


        public void AddCommand(IGameCommand command)
        {
            _commands.Add(_commandIdx, command);
            _commandIdx++;
        }
			
        public void FixedTick()
        {
            RunActiveCommands();
        }

        private void RunActiveCommands()
        {
            foreach (var gameCommand in _commands)
            {
                RunCommand(gameCommand.Value);
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
		public void RemoveAllCommands(){
			foreach (var gameCommand in _commands) {
				RemoveCommand (gameCommand.Value);
				gameCommand.Value.Dispose (); //not sure what this does
			}
		}
    }
}