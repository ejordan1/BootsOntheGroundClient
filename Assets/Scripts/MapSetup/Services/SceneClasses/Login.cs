using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using MapSetup.Model;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//force players to use letters numbers to not mess up url
using MyMvcProject.Data;
using Scripts.MapSetup.Services;


public class Login : MonoBehaviour {
	public Text username;
	public Text password;
	public Text title;
	public GameObject loginButton;
	public string ServerPath = "http://localhost:5000/api/Login";
	public Home home;

	// Use this for initialization
	void Start () {
		username = GameObject.Find ("Username").GetComponent<Text> ();
		password = GameObject.Find ("Password").GetComponent<Text> ();
		title = GameObject.Find ("TitleText").GetComponent<Text> ();
		loginButton = GameObject.Find ("LoginButton");

		home = GetComponent<Home>();

	}

	[ContextMenu("open connectoin")]
	public void open()
	{
		SocketHandler.Open();
	}


	public void login(){
		Debug.Log ("attempting login");
		StartCoroutine(LoginAsync());
	}


	IEnumerator LoginAsync()
	{
		Debug.Log("LoginAsync...");
		yield return 1;
		//adding the headers
		RestClient.Headers["Username"] =  username.text;

		RestClient.Headers["DeviceID"] = SystemInfo.deviceUniqueIdentifier;

		RestClient.Headers ["Password"] = password.text;

		var task = RestClient.Get(ServerPath); //rest client get

		yield return task.SendWebRequest(); 

		if (task.isNetworkError)
		{
			Debug.LogError(task.error);
		}
		else
		{
			var login = task.Deserialize<LoginResponse>();
			//Debug.Log (login.ErrorCode);
			if (login == null) {
				Debug.Log ("Login null");
			} else
			{
				if (login.ErrorCode != 0) {
					Debug.Log ("login error");
				} else {
					if (login.ErrorText == "new account") {
						newAccountLoggedIn ();
					} else if (login.ErrorText == "existing account") {
					
						existingAccountLoggedIn ();
					} else if (login.ErrorText == "incorrect password") {
						incorrectPassword ();
					} else {
						Debug.Log ("didn't recognize login text");
					}
				}

				//RestClient.Headers.Remove ("Username");
				RestClient.Headers.Remove ("Password");
				RestClient.Headers ["Token"] = login.Token; //ADDS TOKEN TO HEADERS: IS THIS CORRECT?

			}
		}

		task.Dispose();
	}

	public void newAccountLoggedIn(){
	
		Debug.Log ("new account : " + username.text + " logged in!");
		loginSuccessful ();
	}

	public void existingAccountLoggedIn(){
		Debug.Log ("existing account : " + username.text + " logged in!");
		loginSuccessful ();
	}

	public void incorrectPassword(){

		Debug.Log (username.text + "password incorrect. try again!");

	}


	public void loginSuccessful(){
		Debug.Log("Login successful");
		ClientStaticData.UserID = RestClient.Headers["Username"];
		//home.HomeStart();
//		GameObject.Find("Login").SetActive(false);
	}


}
