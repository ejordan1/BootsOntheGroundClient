using System;
using System.Collections.Generic;
using UnityEngine;
using Data;
using MapSetup.Model;

namespace MapSetup.Model
{

	[Serializable]
	public class MapArrangement //
	{
		public List<ZoneModel> _zoneModels;
		public WorldPosition position;
		public WorldPosition scale;

		public MapArrangement(){
			_zoneModels = new List<ZoneModel>();
			position = new WorldPosition (0, 0);
			scale = new WorldPosition (1, 1);
		}

		
		//public string resourceID;

	
	}

}