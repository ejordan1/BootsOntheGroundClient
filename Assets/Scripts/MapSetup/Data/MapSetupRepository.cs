using System.Collections.Generic;
using MapSetup.Model;
using UnityEngine;

namespace MapSetup.Data
{
	public class MapSetupRepository : MonoBehaviour
	{
		private readonly MapSetupRegistry _mapSetupRegistry;

		public readonly Dictionary<string, MapData> _mapSetupsById = new Dictionary<string, MapData>();

		public MapSetupRepository(MapSetupRegistry mapSetupRegistry) //have to put into main installer
		{
			_mapSetupRegistry = mapSetupRegistry;

		}

		private void ParseUnits()
		{
			foreach (var mapData in _mapSetupRegistry)
			{
				_mapSetupsById[mapData.Id] = mapData;

			}
		}

		public MapData GetById(string id) //this magic happens in the shared asset registry stuff
		{
			return _mapSetupsById[id];
		}

		public MapArrangement createById(string id){
			MapArrangement map = new MapArrangement ();
			MapData mapData = GetById (id);
			//map.zoneObjects = mapData.ZoneObjects;
		//	map.resourceID = mapData.Name;
			return map;
		}
	}
}