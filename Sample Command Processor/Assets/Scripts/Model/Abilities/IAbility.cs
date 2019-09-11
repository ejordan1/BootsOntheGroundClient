using Services.Commands;

namespace Model.Abilities
{
    public interface IAbility : IGameCommand
    {
        void Init();
        bool Check();
    }
}