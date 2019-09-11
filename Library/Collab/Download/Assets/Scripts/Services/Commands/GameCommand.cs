namespace Services.Commands
{
    public abstract class GameCommand : IGameCommand
    {
		

        public GameCommandStatus Status { get; private set; }

        public abstract GameCommandStatus FixedStep(); //

        public virtual void Abort()
        {
            Status = GameCommandStatus.Aborted;
        }

        public virtual void Dispose()
        {
			
        }
    }
}

//to create something: need to take care of when it is being destroyed:
//sub container: can override the implementation of classes: add a new command, requires command processor: 

//each object is responsible to disposing the stuff that it created.
//call the single dispose of the command that created everything: everything will be disposed

//block chain: all clients are aware of what others are happening.

