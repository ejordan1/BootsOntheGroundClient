using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doStuffTestScript : MonoBehaviour {
	
	public squareValues square;

	void Start(){
		square = GameObject.Find ("Square").GetComponent<squareValues>();	
	}


	public void changeScore(float value){
		square.scoreValue = value;
	}

}
