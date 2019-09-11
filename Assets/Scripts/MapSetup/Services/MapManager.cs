using Data;
using System.Collections.Generic;
using Model.Units;
using UnityEngine;
using View;
using Zenject;
using Model.AI;
using Services;
using MapSetup.Model;


namespace Model
{
	public class MapManager
	{
		//beind it it to this zenject intaller, not the main one
		//need to make that fist
		private readonly IFactory<MapArrangement> _mapArrangeFactory;
		private readonly IFactory <MapArrangement, MapModel> _mapFactory; //bind this. this how this works
		UnitRepository _unitRepository;

		//unit prep factory
		//map factory

		public MapManager (UnitRepository unitRepository, 
			IFactory<MapArrangement> mapArrangeFactory,
			IFactory <MapArrangement, MapModel> mapFactory
			)
		{
			_mapArrangeFactory = mapArrangeFactory;
			_unitRepository = unitRepository;
			_mapFactory = mapFactory;


		}

		public MapArrangement CreateMapArrange()
		{
			var mapArrange =  _mapArrangeFactory.Create();
			return mapArrange;

		}

		public MapModel CreateMapModel(MapArrangement mapArrange)
		{
			var map =  _mapFactory.Create(mapArrange);
			return map;
			
		}
	}
}

//okay so the manager just gets the info from the registry; then it puts it into the createbilityfromid thing which then goes from there. 
//that is all the abilites array in the unit registry does. it does not play a role in the actual ability creation,
//therefore, making abilities in the game requires only giving the specific parameters to the createabilityfromid thing.