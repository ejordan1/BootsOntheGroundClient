using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapSetup.Model;

public class ZoneView : MonoBehaviour {
	public ZoneModel _zoneModel;


	// Update is called once per frame
	void Update () {
		if (_zoneModel != null) {
			transform.localPosition = new Vector3 ((float)_zoneModel.position.X, (float)_zoneModel.position.Y, 0);
		} 
	}
}

//maybe better to make these all in the same script? list of lists............