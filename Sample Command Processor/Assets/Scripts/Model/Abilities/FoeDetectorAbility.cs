using Data;
using Model.AI;
using Model.Units;
using Utils;

namespace Model.Abilities
{
    public class FoeDetectorAbility : AbilityCommand, ITargetAbility
    {
        private readonly UnitModel _unit;
        private readonly WorldModel _world;
        private readonly FoeDetectorAbilityParams _data;

        public FoeDetectorAbility(UnitModel unit, AbilityData data, WorldModel world)
        {
            _unit = unit;
            _world = world;
            _data = new FoeDetectorAbilityParams(data);
        }

        protected override void DoTheLogic()
        {
            var targets = _world.GetEnemyUnitsTo(_unit.Alliance);
            Target = targets.GetClosestUnit(_unit);
        }

        public override void Init()
        {
        }

        public override bool Check()
        {
            return true;
        }

        public ITarget Target { get; private set; }

        public int TargetPriority
        {
            get { return _data.TargetPriority; }
        }

        public class FoeDetectorAbilityParams
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
    }
}