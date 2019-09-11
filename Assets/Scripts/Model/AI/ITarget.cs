using System.Runtime.CompilerServices;
using Data;
using FixMath.NET;
using Model.Units;

namespace Model.AI
{
    public interface ITarget
    {
        WorldPosition GetPosition();
        bool IsTargetReached(UnitModel me);
		bool notNull ();
    }

    public class WorldPositionTarget : ITarget
    {
        private readonly WorldPosition _position;

        public WorldPositionTarget(WorldPosition position)
        {
            _position = position;
        }

        public WorldPosition GetPosition()
        {
            return _position;
        }

        public bool IsTargetReached(UnitModel me)
        {
            return _position.IsInRange(me.Position, (Fix64) 1);
        }

		public bool notNull()
		{
			return true; //this is a bad way of doing this: unecessary to extend to all the types here
		}

        public override string ToString()
        {
            return _position.ToString();
        }
    }

    public class UnitTarget : ITarget
    {
        private readonly UnitModel _unitModel;

        public UnitTarget(UnitModel unitModel)
        {
            _unitModel = unitModel;
        }


        public UnitModel UnitModel
        {
            get { return _unitModel; }
        }

        public WorldPosition GetPosition()
        {
				return _unitModel.Position;	
        }

		public bool notNull()
		{
			if (_unitModel != null) {
				return true;
			}
			return false;
		}

        public bool IsTargetReached(UnitModel me)
        {
            return _unitModel.Position.IsInRange(me.Position, 2);
        }
    }

    public class FormationTarget : ITarget
    {
        private readonly WorldPosition _position;

        public FormationTarget(WorldPosition position)
        {
            _position = position;
        }

        public WorldPosition GetPosition()
        {
            return _position;
        }

        public bool IsTargetReached(UnitModel me)
        {
            return _position.IsInRange(me.Position, (Fix64) 0.1);
        }
		public bool notNull()
		{
			return true; //this is a bad way of doing this: unecessary to extend to all the types here
		}
    }
}