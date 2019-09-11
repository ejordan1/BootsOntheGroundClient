using System;
using MapSetup.Model;
using System.Collections.Generic;

namespace Scripts.MapSetup.Services
{
	public static class StaticMapCreateData  //static are not the zenject way: need to convert to zenjct: need to create binding,
	//inject into constructors. 
	{
		
		public static Dictionary<int, MapArrangement> _mapList;

		public static MapArrangement _currentArrange;

		public static MapModel _currentMap;

		public static ZoneModel _selectedZone;

		public static string _mapSerialized;


	}
}


//flow of data: goes through map arrangements, creates new map when you select arrangement