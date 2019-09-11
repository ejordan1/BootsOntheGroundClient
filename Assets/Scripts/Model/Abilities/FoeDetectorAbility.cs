using Data;
using Model.AI;
using Model.Units;
using Utils;
using Model.Abilities.AbilityStuff;

namespace Model.Abilities
{
    public class FoeDetectorAbility : AbilityCommand, ITargetAbility
    {
        private readonly UnitModel _unit;
        private readonly WorldModel _world;
        private readonly FoeDetectorAbilityParams _data;
		private readonly ProjectileManager _projectileManager;

        public FoeDetectorAbility(UnitModel unit, AbilityData data, WorldModel world)
        {
            _unit = unit;
            _world = world;
            _data = new FoeDetectorAbilityParams(data);
        }

        protected override void DoTheLogic()
        {
            var targets = _world.GetEnemyUnitsTo(_unit.Alliance);
            _target = new UnitTarget(targets.GetClosestUnit1(_unit));
        }

        public override void Init()
        {
        }

        
        private ITarget _target;

        ITarget ITargetAbility.Target
        {
            get { return _target; }
        }

        public int TargetPriority
        {
            get { return _data.TargetPriority; }
        }

		public class FoeDetectorAbilityParams  // just wrapper class that translates from int1, int2, int3, etc to real data that we need
        { 
            public int TargetPriority
            {
                get { return _data.int1; }
            }

            private readonly AbilityData _data;

            public FoeDetectorAbilityParams(AbilityData data)
            {
                _data = data;
            }
        }

		public override string ToString()
		{
			return string.Format("[UnitModel Id={0}, Type= Foe detector ability]", _unit.Id);
		}
    }
}