using System;
using UnityEngine;



namespace MyMvcProject.Data
{
	[Serializable]
	public class LoginResponse : Response
	{
		[SerializeField]
		public string Token;
	}
}