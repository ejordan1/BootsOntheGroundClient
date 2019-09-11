using System;
using UnityEngine;
using MapSetup.Model;
using UnityEngine.SceneManagement;
using Scripts.MapSetup.Services;
using Zenject;
using Data;


namespace Scripts.MapSetup.Services
{
	public class Arrange : ClientPage
	{
		public GameObject mapPrefab;
		public GameObject mapView;


		[Inject]
		public void Construct()
		{

		}

		void Start(){

			StaticMapCreateData._selectedZone = StaticMapCreateData._currentMap._zoneModels [0];
			mapView = Instantiate (mapPrefab, Vector3.zero, Quaternion.identity);
			mapView.GetComponent<MapView> ()._mapModel = StaticMapCreateData._currentMap;
		}

		public int count;

		public void ZoneIncrease(){
			int thisCount = count % StaticMapCreateData._currentMap._zoneModels.Count;
			StaticMapCreateData._selectedZone = StaticMapCreateData._currentMap._zoneModels [thisCount];
			Debug.Log (thisCount + " " + StaticMapCreateData._selectedZone);
			count++;
		}
		
		public void ChangeTeam(){



		
			if (StaticMapCreateData._selectedZone != null) {
				switch (StaticMapCreateData._selectedZone._party) {
				case MapModel.Party.P1:
					StaticMapCreateData._selectedZone._party = MapModel.Party.P2;
					break;
				case MapModel.Party.P2:
					StaticMapCreateData._selectedZone._party = MapModel.Party.P3;
					break;
				case MapModel.Party.P3:
					StaticMapCreateData._selectedZone._party = MapModel.Party.P4;
					break;
				case MapModel.Party.P4:
					StaticMapCreateData._selectedZone._party = MapModel.Party.AIOnly;
					break;
				case MapModel.Party.AIOnly:
					StaticMapCreateData._selectedZone._party = MapModel.Party.P1;
					break;
				}

			}
			Debug.Log ("party changed to : " + StaticMapCreateData._selectedZone._party);
		}


		public void ChangeAlliance(){
			

			switch (StaticMapCreateData._selectedZone._alliance) {
			case AllianceType.Team1:
				StaticMapCreateData._selectedZone._alliance = AllianceType.Team2;
				break;
			case AllianceType.Team2:
				StaticMapCreateData._selectedZone._alliance = AllianceType.Team3;
				break;
			case AllianceType.Team3:
				StaticMapCreateData._selectedZone._alliance = AllianceType.Team4;
				break;
			case AllianceType.Team4:
				StaticMapCreateData._selectedZone._alliance = AllianceType.Team5;
				break;
			case AllianceType.Team5:
				StaticMapCreateData._selectedZone._alliance = AllianceType.Team6;
				break;
			case AllianceType.Team6:
				StaticMapCreateData._selectedZone._alliance = AllianceType.Team7;
				break;
			case AllianceType.Team7:
				StaticMapCreateData._selectedZone._alliance = AllianceType.Team8;
				break;
			case AllianceType.Team8:
				StaticMapCreateData._selectedZone._alliance = AllianceType.Team1;
				break;
			}
			Debug.Log (StaticMapCreateData._selectedZone._alliance);
			}



		public void forward(){
			SceneManager.LoadScene (4);

		}
		public void back(){
			
			SceneManager.LoadScene (2);

		}
	}
}

