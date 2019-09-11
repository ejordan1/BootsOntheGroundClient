using System;
using UnityEngine;
using Model.Units;
using Utils.Extensions;
using Data;
using MapSetup.Model;
using FixMath.NET;

namespace MapSetup.Data
{
	[Serializable]
	[CreateAssetMenu(fileName = "MapSetup Registry", menuName = "Registry/MapSetup Registry")]
	public class MapSetupRegistry : SharedAssetRegistry<MapData>
	{
	}

	[Serializable]
	public class MapData : RegistryData
	{
		public string resourceId;
		public ZzFragModel[] ZoneObjects; //lower case string
	}
		
}


//create ability registry, create two interfaces. 
//move stuff from data to model, create initial stats stuff
//composite property
//diagram in detail