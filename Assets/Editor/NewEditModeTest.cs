using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using FixMath.NET;
using Editor;
using Model.AI;
using MapSetup.Model;
using Data;
using System.Collections.Generic;
using Model.Abilities;

public class NewEditModeTest
{

	CompositeProperty abe = new CompositeProperty(0);
	CompositeProperty george = new CompositeProperty(5);
	Fix64 added = (Fix64) .0000001f;
	Fix64 multiplied = (Fix64) 0.001f;

	CompositeProperty wash = new CompositeProperty(10);

	//Fix64 multiplierAdded = new Fix64(.00001);
	//Fix64 addValue = new Fix64(20);
	//Fix64 minusAddValue = new Fix64(-10);

	[Test]
	public void NewEditModeTestSimplePasses()
	{
		CompositeProperty lincoln = abe;

		/*
		for (int i = 0; i < 50000; i++) {
			abe.addedValueChange (added);
			abe.multiplierValueChange (multiplied);
		}

		for (int i = 0; i < 50000; i++) {
			abe.addedValueChange (-added);
			abe.multiplierValueChange (-multiplied);
		}
		*/
		Assert.AreEqual(abe, lincoln);


		wash.multiplierValueChange((Fix64) 10);
		Debug.Log(wash.value);
		wash.multiplierValueChange((Fix64) (-10f));
		Debug.Log(wash.value);
		Assert.AreEqual(wash.value, (Fix64) 10f);


	}


	[Test]
	public void MapSerializationTest()
	{
		MapModel map1 = new MapModel(new MapArrangement());
		map1._zoneModels.Add(new ZoneModel(MapModel.Party.P1, AllianceType.Team1, new WorldPosition(-5, 0)));
		map1._zoneModels.Add(new ZoneModel(MapModel.Party.AIOnly, AllianceType.Team2, new WorldPosition(5, 0)));
		map1._zoneModels[0].zonePieceModels
			.Add(new ZzFragModel(ZzFragModel.PieceType.circle, new WorldPosition(0, 0), new WorldPosition(5, 10)));
		map1._zoneModels[0].zonePieceModels.Add(new ZzFragModel(ZzFragModel.PieceType.rectangle, new WorldPosition(-3, 2),
			new WorldPosition(3, 3)));
		map1._zoneModels[1].zonePieceModels
			.Add(new ZzFragModel(ZzFragModel.PieceType.circle, new WorldPosition(0, 0), new WorldPosition(5, 10)));

		map1._creator = "Emerson";
		map1._mapName = "mymap1";
		map1._mapInfo = "this is the map info";
		map1._mapDescription = "hi Im a map description";
		map1._formationResources = 500;
		map1.position = new WorldPosition(30, 30);
		map1.scale = new WorldPosition(1, 1);


		//Unit Toggles
		UnitTogglesAll containerIn = new UnitTogglesAll();
		containerIn._togglesAll = new List<UnitToggleParty>(); //creates list
		UnitToggleParty party1 = new UnitToggleParty(); //creates object for list
		UnitToggleParty party2 = new UnitToggleParty();
		containerIn._togglesAll.Add(party2);
		containerIn._togglesAll.Add(party1);
		party2._togglesParty = new List<UnitToggle>();
		party1._togglesParty = new List<UnitToggle>(); //creates new unit toggles list in it
		UnitToggle unit1 = new UnitToggle();
		UnitToggle unit2 = new UnitToggle();
		unit1.unit = "Barbarian";
		unit1.toggle = false;
		unit2.unit = "Spartan";
		unit2.toggle = true;
		party1._togglesParty.Add(unit1);
		party1._togglesParty.Add(unit2);
		party2._togglesParty.Add(unit2);
		party2._togglesParty.Add(unit1);

		map1.UnitToggles = containerIn;

		map1._preFormationUnits.Add(new Model.Units.UnitPrep("Spartan", AllianceType.Team1, new WorldPosition(5, 5)));
		map1._preFormationUnits.Add(new Model.Units.UnitPrep("Barbarian", AllianceType.Team2, new WorldPosition(-3, 3)));

		string map1JSONtest = JsonUtility.ToJson(map1);
		Debug.Log(map1JSONtest);

	}

	[Test]
	public void WorldPositionSeralization()
	{
		WorldPosition wp = new WorldPosition(30, 20);
		string pos1 = JsonUtility.ToJson(wp);
		Debug.Log(pos1);
		WorldPosition wp2 = JsonUtility.FromJson<WorldPosition>(pos1);
		Assert.AreEqual(wp.X, wp2.X);
		Assert.AreEqual(wp.Y, wp2.Y); //fail test it false
	}



	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator NewEditModeTestWithEnumeratorPasses()
	{
		// Use the Assert class to test conditions.
		// yield to skip a frame



		yield return null;
	}

	[System.Serializable]
	public class stuff
	{
		public List<int> aList = new List<int>();

	}

	//magic: list is not marked as serializable: this is used by unity to see what can be serialized. if an object is not 
	//marked as that it wont be. json utility is just used for convience, and you need to use serializalbe every so doing that is arbitrraty.
	//sleep. change all the stuff to wrapper classes and primitive types.
	[Test]
	public void ListTJOSN()
	{
		stuff hello = new stuff();
		hello.aList.Add(20);
		hello.aList.Add(2);
		string why = JsonUtility.ToJson(hello);
		stuff hea = JsonUtility.FromJson<stuff>(why);

		Assert.AreEqual(hello.aList.Count, hea.aList.Count);
		Assert.AreEqual(hello.aList[0], hea.aList[0]);
		Assert.AreEqual(hello.aList[1], hea.aList[1]);


	}

	[Test]
	public void ZoneToJSON()
	{
		ZoneModel zone = new ZoneModel(MapModel.Party.P1, AllianceType.Team2, new WorldPosition(30, 30));
		ZzFragModel zz1 =
			new ZzFragModel(ZzFragModel.PieceType.rectangle, new WorldPosition(50, 50), new WorldPosition(30, 30));
		ZzFragModel zz2 = new ZzFragModel(ZzFragModel.PieceType.circle, new WorldPosition(50, 50), new WorldPosition(30, 30));
		ZzFragModel zz3 =
			new ZzFragModel(ZzFragModel.PieceType.rectangle, new WorldPosition(50, 50), new WorldPosition(30, 30));
		ZzFragModel zz4 = new ZzFragModel(ZzFragModel.PieceType.circle, new WorldPosition(50, 50), new WorldPosition(30, 30));
		zone.zonePieceModels.Add(zz1);
		zone.zonePieceModels.Add(zz2);
		zone.zonePieceModels.Add(zz3);
		zone.zonePieceModels.Add(zz4);
		string zoneJson = JsonUtility.ToJson(zone);
		Debug.Log(zoneJson);
		ZoneModel zoneOut = JsonUtility.FromJson<ZoneModel>(zoneJson);
		Assert.AreEqual(zone._party, zoneOut._party);
		Assert.AreEqual(zone._alliance, zoneOut._alliance);
		Assert.AreEqual(zone.zonePieceModels[0].pieceType, zoneOut.zonePieceModels[0].pieceType);
		Assert.AreEqual(zone.zonePieceModels[1].pieceType, zoneOut.zonePieceModels[1].pieceType);
		Assert.AreEqual(zone.zonePieceModels[2].pieceType, zoneOut.zonePieceModels[2].pieceType);
		//Assert.AreEqual (zone.zonePieceModels, zoneOut.zonePieceModels);

	}


	[Test]
	public void zzToJSON()
	{
		ZzFragModel zz =
			new ZzFragModel(ZzFragModel.PieceType.rectangle, new WorldPosition(50, 50), new WorldPosition(30, 30));
		string why = JsonUtility.ToJson(zz);
		ZzFragModel yy = JsonUtility.FromJson<ZzFragModel>(why);
		Debug.Log(why);
		Assert.AreEqual(zz.pieceType, yy.pieceType);

	}

	[Test]
	public void togglesContainerJSON()
	{
		UnitTogglesAll containerIn = new UnitTogglesAll();
		containerIn._togglesAll = new List<UnitToggleParty>(); //creates list
		UnitToggleParty party1 = new UnitToggleParty(); //creates object for list
		UnitToggleParty party2 = new UnitToggleParty();

		containerIn._togglesAll.Add(party2);
		containerIn._togglesAll.Add(party1);
		party2._togglesParty = new List<UnitToggle>();
		party1._togglesParty = new List<UnitToggle>(); //creates new unit toggles list in it
		UnitToggle unit1 = new UnitToggle();
		UnitToggle unit2 = new UnitToggle();
		unit1.unit = "Barbarian";
		unit1.toggle = false;
		unit2.unit = "Spartan";
		unit2.toggle = true;
		party1._togglesParty.Add(unit1);
		party1._togglesParty.Add(unit2);
		party2._togglesParty.Add(unit2);
		party2._togglesParty.Add(unit1);
		//
		//
		string convert = JsonUtility.ToJson(containerIn);
		UnitTogglesAll containerOut = JsonUtility.FromJson<UnitTogglesAll>(convert);
		Debug.Log(convert);
		Assert.AreEqual(containerIn._togglesAll[0]._togglesParty[0].unit, containerOut._togglesAll[0]._togglesParty[0].unit);
		Assert.AreEqual(containerIn._togglesAll[0]._togglesParty[1].unit, containerOut._togglesAll[0]._togglesParty[1].unit);
		Assert.AreEqual(containerIn._togglesAll[0]._togglesParty[0].toggle,
			containerOut._togglesAll[0]._togglesParty[0].toggle);
		Assert.AreEqual(containerIn._togglesAll[0]._togglesParty[1].toggle,
			containerOut._togglesAll[0]._togglesParty[1].toggle);
	}

	[Test]
	public void listStrinTest()
	{
		PartyServerModel party = new PartyServerModel();
		
		party.inviteRequests = new List<string>();
		party.inviteRequests.Add("asdf");
		party.inviteRequests.Add("ffff");

		string s = JsonUtility.ToJson(party);

		PartyServerModel party2 = JsonUtility.FromJson<PartyServerModel>(s);
		Assert.AreEqual(party.inviteRequests[0], party2.inviteRequests[0]);
		Assert.AreEqual(party.inviteRequests[1], party2.inviteRequests[1]);


	}

	
}
