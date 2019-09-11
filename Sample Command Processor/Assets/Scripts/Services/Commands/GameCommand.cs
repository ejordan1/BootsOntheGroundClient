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