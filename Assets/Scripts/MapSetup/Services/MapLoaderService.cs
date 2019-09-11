using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapSetup.Model;
using Data;
using FixMath.NET;
using UnityEngine.SceneManagement;
using Scripts.MapSetup.Services;
using Zenject;
using Model;

public class MapLoaderService
{
	private readonly MapManager _mapManager;


	public MapLoaderService(MapManager mapManager)
	{
		_mapManager = mapManager;

	}




	public void populateUnitToggles(MapModel mapModel){
		mapModel.UnitToggles._togglesAll = new List<UnitToggleParty> ();
		for (int i = 0; i < 5; i++) { //p1p2p3p4AI
			mapModel.UnitToggles._togglesAll.Add (new UnitToggleParty());
			mapModel.UnitToggles._togglesAll [i]._togglesParty = new List<UnitToggle> ();
			foreach( var UnitID in BridgeData.unitRepo._unitsById.Keys){
				mapModel.UnitToggles._togglesAll[i]._togglesParty.Add(new UnitToggle(){toggle = true, unit = UnitID});
			}
		}
	}


	public void populateMaps(){
		MapArrangement map1 = _mapManager.CreateMapArrange ();

		map1._zoneModels.Add (new ZoneModel (MapModel.Party.P1, AllianceType.Team1, new WorldPosition (-5, 0)));
		map1._zoneModels.Add (new ZoneModel (MapModel.Party.P2, AllianceType.Team1, new WorldPosition (0, 5)));
		map1._zoneModels.Add (new ZoneModel (MapModel.Party.P3, AllianceType.Team1, new WorldPosition (5, 0)));
		map1._zoneModels.Add (new ZoneModel (MapModel.Party.P4, AllianceType.Team1, new WorldPosition (0, -5)));
		map1._zoneModels.Add (new ZoneModel (MapModel.Party.AIOnly, AllianceType.Team2,  new WorldPosition (0, 0)));


		map1._zoneModels [0].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (0, 0), new WorldPosition (5, 5)));
		map1._zoneModels [1].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (0, 0), new WorldPosition (5, 5)));
		map1._zoneModels [2].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (0, 0), new WorldPosition (5, 5)));
		map1._zoneModels [3].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (0, 0), new WorldPosition (5, 5)));
		map1._zoneModels [4].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (0, 0), new WorldPosition (5, 5)));



		MapArrangement map2 =  _mapManager.CreateMapArrange ();
		map2._zoneModels.Add (new ZoneModel (MapModel.Party.P1, AllianceType.Team1, new WorldPosition (-5, 0)));
		map2._zoneModels.Add (new ZoneModel (MapModel.Party.AIOnly, AllianceType.Team2,  new WorldPosition (5, 0)));
		map2._zoneModels [0].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.rectangle, new WorldPosition (0, 0), new WorldPosition (5, 5)));
		map2._zoneModels [0].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (-3, 2), new WorldPosition (5, 5)));
		map2._zoneModels [1].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.rectangle, new WorldPosition (0, 0), new WorldPosition (5, 5)));


		MapArrangement map3 =  _mapManager.CreateMapArrange ();
		map3._zoneModels.Add (new ZoneModel (MapModel.Party.P1, AllianceType.Team1, new WorldPosition (-5, 0)));
		map3._zoneModels.Add (new ZoneModel (MapModel.Party.AIOnly, AllianceType.Team2,  new WorldPosition (5, 0)));
		map3._zoneModels [0].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (0, 0), new WorldPosition (10, 10)));
		map3._zoneModels [0].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.rectangle, new WorldPosition (-3, 2), new WorldPosition (3, 3)));
		map3._zoneModels [1].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (0, 0), new WorldPosition (3, 3)));


		StaticMapCreateData._mapList = new Dictionary<int, MapArrangement> ();

		StaticMapCreateData._mapList [0] = map1;
		StaticMapCreateData._mapList [1] = map2;
		StaticMapCreateData._mapList [2] = map3;
	
	} 


		
}
