using System;
using FixMath.NET;
using UnityEngine;
using Model.Units;

namespace Data
{
	[Serializable]
	[CreateAssetMenu(fileName = "Projectile Registry", menuName = "Registry/Projectile Registry")]
	public class ProjectileRegistry : SharedAssetRegistry<ProjectileData>
	{
	}

	[Serializable]
	public class ProjectileData : RegistryData
	{

		public string projectileType;
		public int projectileHP;
		public Fix64Param speed;
		public Fix64Param range;
		public string resourceID;

		public int int1;
		public int int2;
		public int int3;

		public Fix64Param float1;
		public Fix64Param float2;
		public Fix64Param float3;

	}

}


//create ability registry, create two interfaces. 
//move stuff from data to model, create initial stats stuff
//composite property
//diagram in detail