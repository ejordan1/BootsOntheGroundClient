using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapSetup.Model;
using Data;
using FixMath.NET;
using UnityEngine.SceneManagement;
using Scripts.MapSetup.Services;

public static class MapLoaderService {


	public static void populateUnitToggles(MapModel mapModel){
		mapModel.UnitToggles._togglesAll = new List<UnitToggleParty> ();
		for (int i = 0; i < 5; i++) { //p1p2p3p4AI
			mapModel.UnitToggles._togglesAll.Add (new UnitToggleParty());
			mapModel.UnitToggles._togglesAll [i]._togglesParty = new List<UnitToggle> ();
			foreach( var UnitID in BridgeData.unitRepo._unitsById.Keys){
				mapModel.UnitToggles._togglesAll[i]._togglesParty.Add(new UnitToggle(){toggle = true, unit = UnitID});
			}
		}
	}


	public static void populateMaps(){
		MapArrangement map1 = new MapArrangement ();
		map1.zoneModels.Add (new ZoneModel (MapModel.Party.P1, AllianceType.Team1, new WorldPosition (-5, 0)));
		map1.zoneModels.Add (new ZoneModel (MapModel.Party.AIOnly, AllianceType.Team2,  new WorldPosition (5, 0)));
		map1.zoneModels [0].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (0, 0), new WorldPosition (5, 10)));
		map1.zoneModels [0].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.rectangle, new WorldPosition (-3, 2), new WorldPosition (3, 3)));
		map1.zoneModels [1].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (0, 0), new WorldPosition (5, 10)));


		MapArrangement map2 = new MapArrangement ();
		map2.zoneModels.Add (new ZoneModel (MapModel.Party.P1, AllianceType.Team1, new WorldPosition (-5, 0)));
		map2.zoneModels.Add (new ZoneModel (MapModel.Party.AIOnly, AllianceType.Team2,  new WorldPosition (5, 0)));
		map2.zoneModels [0].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.rectangle, new WorldPosition (0, 0), new WorldPosition (5, 5)));
		map2.zoneModels [0].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (-3, 2), new WorldPosition (5, 5)));
		map2.zoneModels [1].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.rectangle, new WorldPosition (0, 0), new WorldPosition (5, 5)));


		MapArrangement map3 = new MapArrangement ();
		map3.zoneModels.Add (new ZoneModel (MapModel.Party.P1, AllianceType.Team1, new WorldPosition (-5, 0)));
		map3.zoneModels.Add (new ZoneModel (MapModel.Party.AIOnly, AllianceType.Team2,  new WorldPosition (5, 0)));
		map3.zoneModels [0].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (0, 0), new WorldPosition (5, 10)));
		map3.zoneModels [0].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.rectangle, new WorldPosition (-3, 2), new WorldPosition (3, 3)));
		map3.zoneModels [1].zonePieceModels.Add (new ZzFragModel (ZzFragModel.PieceType.circle, new WorldPosition (0, 0), new WorldPosition (3, 7)));


		StaticMapCreateData.mapList = new Dictionary<int, MapArrangement> ();

		StaticMapCreateData.mapList [0] = map1;
		StaticMapCreateData.mapList [1] = map2;
		StaticMapCreateData.mapList [2] = map3;

	}
}
