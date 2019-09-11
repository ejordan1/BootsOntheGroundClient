using Services;
using Services.Commands;

namespace Model.Abilities.AbilityStuff
{
    public interface ITickAbility : IAbility
    {
        void Activate();
        GameCommandStatus FixedTick();
        void Deactivate();
    }
}