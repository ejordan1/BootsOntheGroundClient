using System;
using FixMath.NET;
using UnityEngine;
using Model.Units;
using Model.Abilities;

namespace Data
{
	[Serializable]
	[CreateAssetMenu(fileName = "Ability Variables Registry", menuName = "Registry/Ability Variables Registry")]
	public class AbilityVariablesRegistry : SharedAssetRegistry<AbilityData>
	{
	    public static AbilityVariablesRegistry InternalRef { get; private set; }

	    protected void OnEnable()
	    {
	        InternalRef = this;  
	    }
	}



	[Serializable]
	public class AbilityData : RegistryData
	{
	    public string AbilityType;
		public Fix64Param range;
		public Fix64Param castTime;
		public Fix64Param cooldownTime;
		public string string1;
		public int targetPriority;
		public int int1;
		public int int2;
		public Fix64Param float1;
		public Fix64Param float2;
		public WorldPosition worldPos1;
		public UnitModel unit1;


	    public AbilityData Override(AbilityData overrideData)
	    {
	        return new AbilityData
	        {
                AbilityType = AbilityType,
               
                range = overrideData.range,
				castTime = overrideData.castTime,
				cooldownTime = overrideData.cooldownTime,

           		
                string1 = string1,
                int1 = overrideData.int1,
                int2 = overrideData.int2,
                float1 = overrideData.float1,
				float2 = overrideData.float2,
				worldPos1 = overrideData.worldPos1,

            };
	    }
	}
}

