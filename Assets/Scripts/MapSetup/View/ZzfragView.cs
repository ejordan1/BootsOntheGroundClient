using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapSetup.Model;

public class ZzfragView : MonoBehaviour {
	public ZzFragModel _ZzFragModel;
	public Sprite circleSprite;
	public Sprite squareSprite;


	// Update is called once per frame
	void Update () {
		if (_ZzFragModel != null) {
			if (_ZzFragModel.pieceType == ZzFragModel.PieceType.rectangle) {
				GetComponent<SpriteRenderer> ().sprite = squareSprite;
			} else {
				GetComponent<SpriteRenderer> ().sprite = circleSprite;
			}
			transform.localPosition = new Vector3((float)_ZzFragModel.position.X, (float)_ZzFragModel.position.Y, 0);
			transform.localScale = new Vector3 ((float)_ZzFragModel._scale.X, (float)_ZzFragModel._scale.Y, 1);
		} 
	}
}
 
//maybe better to make these all in the same script? list of lists............