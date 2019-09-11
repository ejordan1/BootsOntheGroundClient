using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is not in use yet
public class EventObj
{

	public float timeSinceGameStarted;
	public int eventType;
	public int subType;
	public GameObject otherUnit;
	public float value;
	public float value2;
	public float value3;



	//For killobj/revive object: subtype for kill obj is 1, revive obj is 2.

	public EventObj(float timeSinceGameStarted, int eventType, int subtype, GameObject other)
	{
		this.timeSinceGameStarted = timeSinceGameStarted;
		this.eventType = eventType;
		this.subType = subtype;
		this.otherUnit = other;
	}

	public EventObj(float timeSinceGameStarted, int eventType, int subtype, GameObject other, float value)
	{
		this.timeSinceGameStarted = timeSinceGameStarted;
		this.eventType = eventType;
		this.subType = subtype;
		this.otherUnit = other;
		this.value = value;
	}

	public EventObj(float timeSinceGameStarted, int eventType, int subtype, GameObject other, float value, float value2)
	{
		this.timeSinceGameStarted = timeSinceGameStarted;
		this.eventType = eventType;
		this.subType = subtype;
		this.otherUnit = other;
		this.value = value;
		this.value2 = value2;
	}

    public EventObj(float timeSinceGameStarted, int eventType, int subtype, GameObject other, float value, float value2, float value3)
	{
		this.timeSinceGameStarted = timeSinceGameStarted;
		this.eventType = eventType;
		this.subType = subtype;
		this.otherUnit = other;
		this.value = value;
		this.value2 = value2;
		this.value3 = value3;
	}
}