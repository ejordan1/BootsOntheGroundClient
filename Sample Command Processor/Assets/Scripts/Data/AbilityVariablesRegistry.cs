using System;
using UnityEngine;
using Model.Units;

namespace Data
{
	[Serializable]
	[CreateAssetMenu(fileName = "Ability Variables Registry", menuName = "Registry/Ability Variables Registry")]
	public class AbilityVariablesRegistry : SharedAssetRegistry<AbilityData>
	{
	}



	[Serializable]
	public class AbilityData : RegistryData
	{
	    public string AbilityType;

		public float coolDown;
		public float range;
		public int castTime;
		public int coolDownTime;
        
		public string ProjectileID;

		public int int1;
		public int int2;
		public int int3;
		public int int4;
		public int int5;

		public float float1;
		public float float2;
		public float float3;
		public float float4;
		public float float5;

	    public AbilityData Override(AbilityData overrideData)
	    {
	        return new AbilityData
	        {
                AbilityType = AbilityType,
                coolDown =  overrideData.coolDown,
                range = overrideData.range,
                castTime =  overrideData.castTime,
                coolDownTime = overrideData.castTime,
                ProjectileID = ProjectileID,
                int1 = overrideData.int1,
                int2 = overrideData.int2,
                int3 = overrideData.int3,
                int4 = overrideData.int4,
                int5 = overrideData.int5,
                float1 = overrideData.float1
                // TODO: ask Emerson to do the rest
            };
	    }
	}
}


//create ability registry, create two interfaces. 
//move stuff from data to model, create initial stats stuff
//composite property
//diagram in detail


