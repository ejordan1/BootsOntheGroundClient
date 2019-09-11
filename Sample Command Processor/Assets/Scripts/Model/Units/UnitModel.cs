using System.Collections.Generic;
using System.Linq;
using Data;
using Model.Abilities;
using View;
using Model.AI;

namespace Model.Units
{
    public class UnitModel : IResource, ITarget
    {
		public int unitCost; 
		public int unitType;

		public CompositeProperty hP; //add composite property instead of multiplier addition etc. 
		public CompositeProperty armor; 
		public CompositeProperty size; 
		public CompositeProperty damage; 
		public CompositeProperty moveRange; 
		public CompositeProperty shield;
		public CompositeProperty shieldReducing;
		public CompositeProperty moveSpeed;
		public CompositeProperty weight;
		public CompositeProperty range;

		public int hpInt;
		public int armorInt;

		public readonly List<IAbility> Abilities = new List<IAbility>();

		public UnitModel(UnitData unitData, AllianceType alliance) //constructor for the unit, takes unit data, sets it equal in the model.
        {
            Alliance = alliance;
            UnitData = unitData;

			hP = new CompositeProperty (unitData.initialHP);
			armor = new CompositeProperty (unitData.initialArmor);
			size = new CompositeProperty (unitData.initialSize);
			moveSpeed  = new CompositeProperty (unitData.initialMoveSpeed);
			weight = new CompositeProperty (unitData.initialWeight);
			range = new CompositeProperty (unitData.initialRange);
			unitType = unitData.unitType;


			//hP.setBaseValue (unitData.initialHP);
//			armor.setBaseValue (unitData.initialArmor);



        }
		//move unit data to here


        public UnitData UnitData { get; private set; }

        public WorldPosition Position { get; private set; }
       
        public AllianceType Alliance { get; private set; }

		public bool isAlive { get  {return hP.value > 0;} }

        public string ResourceId
        {
            get { return UnitData.Id; }
        }

        public void SetPosition(WorldPosition position)
        {
            Position = position;
        }

        public ITarget GetAbilityTarget()
        {
            return GetAbilities<ITargetAbility>()
                .Where(ability => ability.Target != null)
                .OrderBy(ability => ability.TargetPriority)
                .Select(ability => ability.Target)
                .FirstOrDefault();
        }

        public IEnumerable<TAbility> GetAbilities<TAbility>()
            where TAbility : class, IAbility
        {
            return Abilities
                .Select(ability => ability as TAbility)
                .Where(ability => ability != null);
        }

        public WorldPosition GetPosition()
        {
            return Position;
        }

        public UnitModel GetUnitModel()
        {
            return this;
        }
    }
}
