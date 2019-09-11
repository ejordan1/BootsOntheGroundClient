using Services.Commands;

namespace Model.Abilities
{
    public abstract class AbilityCommand : GameCommand, IAbility
    {
        private bool _isInitialized;

        public override GameCommandStatus FixedStep()
        {
            if (!_isInitialized)
            {
                Init();
                _isInitialized = true;
            }

            if (Check()) DoTheLogic();

            return GameCommandStatus.InProgress;
        }

        protected abstract void DoTheLogic();

        public abstract void Init();

        public abstract bool Check();
        
    }
}