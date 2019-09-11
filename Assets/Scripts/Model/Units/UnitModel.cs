using System.Collections.Generic;
using System.Linq;
using Data;
using Model.Abilities;
using View;
using Model.AI;
using UnityEngine;
using Model.Abilities.AbilityStuff;
using UniRx;
using MapSetup.Model;

namespace Model.Units
{
    public class UnitModel : IResource
    {
        public static int IdTracker;

        public readonly int Id;

		public int unitCost; 
		public int unitType;

		public CompositeProperty hP; //add composite property instead of multiplier addition etc. 
		public CompositeProperty maxHp;
		public CompositeProperty armor; 
		public CompositeProperty heal; 
		public CompositeProperty size; 
		public CompositeProperty damage; 
		public CompositeProperty moveRange; 
		public CompositeProperty shield;
		public CompositeProperty shieldReducing;
		public CompositeProperty moveSpeed;
		public CompositeProperty weight;
		public CompositeProperty range;
		public CompositeProperty healModifier;
		public CompositeProperty damageModifier;
		public int reviveTime;
		public int dieTime;

		private int petrified = 0;

        public enum AliveStateFlag {  Alive, Dying, Dead, Reviving }

        public ReactiveProperty<AliveStateFlag> AliveState = new ReactiveProperty<AliveStateFlag>(AliveStateFlag.Alive);

        //STATUS
        /*
        public bool alive = true;
		public bool dying = false;
		public bool reviving = false;
        */

		public int hpInt;
		public int armorInt;

		public readonly List<IAbility> Abilities = new List<IAbility>();
		 
		public UnitModel(UnitData unitData, AllianceType alliance) //constructor for the unit, takes unit data, sets it equal in the model.
		{
		    Id = IdTracker++;
            Alliance = alliance;
            UnitData = unitData;


			hP =  new CompositeProperty (unitData.initialHP); //hp starts at 0; this is done throught he added value
			maxHp = new CompositeProperty (unitData.initialHP);
			armor = new CompositeProperty (unitData.initialArmor);
			heal = new CompositeProperty (0);
			damage = new CompositeProperty (0);
			moveRange = new CompositeProperty (unitData.initialMoveRange);
			size = new CompositeProperty (unitData.initialSize);
			moveSpeed  = new CompositeProperty (unitData.initialMoveSpeed);
			weight = new CompositeProperty (unitData.initialWeight);
			range = new CompositeProperty (unitData.initialRange);
			shield = new CompositeProperty (0);
			shieldReducing = new CompositeProperty (5); //figure out what this is
			healModifier = new CompositeProperty (0);
			damageModifier = new CompositeProperty (0);
			unitType = unitData.unitType;
			reviveTime = 30; //maybe change this elsewhere
			dieTime = 30;



			//hP.setBaseValue (unitData.initialHP);
//			armor.setBaseValue (unitData.initialArmor);



        }
		//move unit data to here


        public UnitData UnitData { get; private set; }

        public WorldPosition Position { get; private set; }
       
        public AllianceType Alliance { get; private set; }

        public bool IsAlive
        {
			get { return AliveState.Value == AliveStateFlag.Alive; }
        }

        public bool IsDying
        {
            get { return AliveState.Value == AliveStateFlag.Dying; }
        }

        public bool IsDead
        {
			get { return AliveState.Value == AliveStateFlag.Dead; }
        }

        public bool IsReviving
        {
            get { return AliveState.Value == AliveStateFlag.Reviving; }
        }

        public string ResourceId
        {
			get { return UnitData.resourceID; }
        }

        public void SetPosition(WorldPosition position)
        {
            Position = position;
        }

        public ITarget GetAbilityTarget()
        {
            return GetAbilities<ITargetAbility>()
                .Where(ability => ability.Target != null)
                .OrderBy(ability => ability.TargetPriority)   //this is siiiiiick
                .Select(ability => ability.Target)
                .FirstOrDefault();
        }
		public List<ICastingAbility> GetAbilitiesCasting() 			//returns bool array of abilities casting
		{
			List<ICastingAbility> abilitiesCasting = new List<ICastingAbility>();
			foreach (ICastingAbility ability in GetAbilities<ICastingAbility>()){
				if (ability.Casting) {
					abilitiesCasting.Add (ability);
				}
			}
			return abilitiesCasting;
		}

		public bool AnyAbilitiesCasting() 			//returns bool array of abilities casting
		{
			foreach (ICastingAbility ability in GetAbilities<ICastingAbility>()) {
				if (ability.Casting) {
					return true;
				}
			}
			return false;
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

		public bool IsPetrified(){
			if (petrified > 0) {
				return true;
			} return false;
		}

        public UnitModel GetUnitModel()
        {
            return this;
        }

        public override string ToString()
        {
            return string.Format("[UnitModel Id={0}, Type={1}]", Id, UnitData.Id);
        }
    }


	[System.Serializable]
	public class UnitPrep{

		public string _unitID;
		public AllianceType _alliance; 
		public WorldPosition _position;

		public UnitPrep(string unitID, AllianceType team, WorldPosition position){
			_unitID = unitID;
			_alliance = team;
			_position = position;
		}
	}
}
	

//how to make the composite properties private but still be able to change stuff in them without a rediculous number of methods in here?