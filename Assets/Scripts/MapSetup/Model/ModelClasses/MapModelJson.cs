using System;
using UnityEngine;
using MapSetup.Model;

namespace MyMvcProject.Data
{	
	[Serializable]
	public class MapModelJson : MapArrangement
	{

		[SerializeField]
		public int MapID;

		[SerializeField]
		public string UserID;

		[SerializeField]
		public string MapTitle;

		[SerializeField]
		public string MapJSON;
	}

	[Serializable]
	public class MapModelContainer{

		public MapModelJson[] mapList; //need to figure out which needs to be json and which doesn't
	}
}
