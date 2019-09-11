using Services.Commands;

namespace Model.Abilities.AbilityStuff
{
    public interface IAbility : IGameCommand
    {
        void Init();
      
    }
}