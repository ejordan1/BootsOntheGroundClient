using System;
using UnityEngine;

namespace MyMvcProject.Model
{
	[Serializable]
	public class LoginModel
	{

		[SerializeField]
		public string DeviceID;

		[SerializeField]
		public string Username;

		[SerializeField]
		public string Password;

		[SerializeField]
		public string Token;

		[SerializeField]
		public DateTime LoginDate;
	}


}
