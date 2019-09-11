using System;
using UnityEngine;
using Model.Units;

namespace Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "UnitModel Registry", menuName = "Registry/UnitModel Registry")]
    public class UnitRegistry : SharedAssetRegistry<UnitData>
    {
    }

    [Serializable]
    public class UnitData : RegistryData
    {

	//	public ArrayList eventList = new ArrayList();
	//	public ArrayList resultList = new ArrayList();


		//Later change this from separate variables to one, encapulsates all three values.
		//The final value is stored in the property itself. 
		public int initialHP;
		public int initialArmor;
		public int initialRange;
		public int initialMoveSpeed;
		public int initialSize;
		public int initialWeight;
		public int initialCost;

		public UnitAbility[] abilityIDs; //lower case string

		public int unitType;



		// where and how to do this? public UnitModel[] movementTargets = new object[abilityTargets[1]];

        [Serializable]
        public class UnitAbility
        {
            public bool AbilityOverride;
            public AbilityData AbilityOverrideData;
            public string AbilityId;
        }
    }

}


//create ability registry, create two interfaces. 
//move stuff from data to model, create initial stats stuff
//composite property
//diagram in detail