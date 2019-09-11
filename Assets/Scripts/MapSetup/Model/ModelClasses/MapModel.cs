using System;
using MapSetup.Model;
using Data;
using System.Collections;
using Model.Units;
using UnityEngine;
using FixMath.NET;
using System.Collections.Generic;

namespace MapSetup.Model
{
	[Serializable]
	public class MapModel : MapArrangement
	{
		public enum Party {P1, P2, P3, P4, AIOnly}


		public string _creator = "hi"; 
		public string _mapName;
		public string _mapInfo;
		public string _mapDescription;
		public int _formationResources;
		public int _timeAllowed;
		public UnitTogglesAll UnitToggles;
		//replace dictionary with an array of objects

		public List<UnitPrep> _preFormationUnits;
		public List<UnitPrep> _formationUnits;

		public MapModel (MapArrangement mapArrangement)
		{
			_preFormationUnits = new List<UnitPrep> ();
			_formationUnits = new List<UnitPrep> ();
			UnitToggles = new UnitTogglesAll ();
			_zoneModels = mapArrangement._zoneModels;
			position = mapArrangement.position;
			scale = mapArrangement.scale;

		}
	}



	[Serializable]
	public class ZoneModel 
	{
		public MapModel.Party _party;
		public AllianceType _alliance;
		public WorldPosition position;
		public List<ZzFragModel> zonePieceModels;

		public ZoneModel(MapModel.Party party, AllianceType alliance, WorldPosition wp){
			zonePieceModels = new List<ZzFragModel> ();
			_alliance = alliance;
			position = wp;
			_party = party;
		}
	}

	[Serializable]
	public class ZzFragModel 
	{
		public enum PieceType{rectangle, circle};
		public PieceType pieceType;
		public WorldPosition position;
		public WorldPosition _scale;

		public ZzFragModel(PieceType pt, WorldPosition wp, WorldPosition scale){
			pieceType = pt;
			_scale = scale;
			position = wp;


		}

	}

	[Serializable]
	public class UnitToggle{
		public string unit;
		public bool toggle;

	}

	[Serializable]
	public class UnitToggleParty{
		public List<UnitToggle> _togglesParty;
	}

	[Serializable]
	public class UnitTogglesAll{
		public List<UnitToggleParty> _togglesAll;
	}




}
	