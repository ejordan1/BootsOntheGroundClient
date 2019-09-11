using Data;
using Model.Units;

namespace Model.AI
{
	public interface ITarget
	{
		WorldPosition GetPosition();
		bool IsTargetReached(UnitModel me);
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
			return _position.IsInRange(me.Position, 1f);
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

		public bool IsTargetReached(UnitModel me)
		{
			return _unitModel.Position.IsInRange(me.Position, 2f);
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
			return _position.IsInRange(me.Position, 0.1f);
		}
	}
}