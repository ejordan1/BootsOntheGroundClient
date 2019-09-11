using Services;
using Services.Commands;

namespace Model.Abilities
{
    public interface ITickAbility : IAbility
    {
        void Activate();
        GameCommandStatus FixedTick();
        void Deactivate();
    }
}