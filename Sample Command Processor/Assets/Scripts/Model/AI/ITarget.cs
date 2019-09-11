using Data;
using Model.Units;

namespace Model.AI
{
    public interface ITarget
    {
        WorldPosition GetPosition();
        UnitModel GetUnitModel();
    }
}