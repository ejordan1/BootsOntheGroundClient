using System;

namespace Services.Commands
{
    public interface IGameCommand : IDisposable
    {
        GameCommandStatus Status { get; }
        GameCommandStatus FixedStep();
        void Abort();
    }

    public enum GameCommandStatus
    {
        InProgress,
        Complete,
        Aborted
    }
}