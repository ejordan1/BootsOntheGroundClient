using System;
using MapSetup.Model;
using System.Collections.Generic;

namespace Scripts.MapSetup.Services
{
	public static class StaticMapCreateData  //static are not the zenject way: need to convert to zenjct: need to create binding,
	//inject into constructors. 
	{
		

		public static Dictionary<int, MapArrangement> mapList;

		public static MapModel currentMap;

		public static ZoneModel selectedZone;

		public static string mapSerialized;


	}
}


//flow of data: goes through map arrangements, creates new map when you select arrangement