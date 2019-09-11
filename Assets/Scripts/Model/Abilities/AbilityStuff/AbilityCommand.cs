using Services.Commands;

namespace Model.Abilities.AbilityStuff
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

             DoTheLogic();

            return GameCommandStatus.InProgress;
        }

        protected abstract void DoTheLogic();

        public abstract void Init();

        
    }
}



//this is how it works: you have the cast and execute methods also overriden so that you can call them in the tick thing