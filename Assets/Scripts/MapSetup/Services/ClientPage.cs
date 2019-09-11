using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Scripts.MapSetup.Services
{
	public abstract class ClientPage : MonoBehaviour
	{

		public void returnToHome(){

			SceneManager.LoadScene (1);
		}
		
	}
}

