using System;
using UnityEngine;

using MyMvcProject.Data;
using MapSetup.Model;namespace MyMvcProject.Map

{
	[Serializable]
	public class MapResponse : Response
	{
		[SerializeField]
		public MapModelJson  map;  //the seralized model fields CANNOT HAVE GETTER AND SETTER
		//IF YOU COPY THE CLASS, DELETE THE GETTER AND SETTER
	}
}