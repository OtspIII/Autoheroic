using UnityEngine;
using System.Collections;

public class WalkEvent : EventParent {

	ClassController ActiveChar;
	Vector2 EndLocation;
	Vector2 StartLocation;

	// Use this for initialization
	void Start () {
		Initialize();
	}

	//This wants {string Character.UniqueName, float Destination X Coord, float Destination Y Coord}
	public override void Begin (System.Collections.Generic.List<string> data)
	{
		base.Begin(data);
		ClassController who = Manager.CharacterReference[data[0]];;
		Vector2 whereE = new Vector2(float.Parse(data[1]),float.Parse(data[2]));
		ActiveChar = who;
		EndLocation = whereE;
		StartLocation = new Vector2(ActiveChar.transform.position.x,ActiveChar.transform.position.z);
		ActiveChar.DoAnimation(AM.Actions.Walk);
	}

	public override void Continue (){
		base.Continue ();
		float perc = 1 -(Timer / TimerMax);
		Vector3 where = new Vector3(StartLocation.x + (EndLocation.x - StartLocation.x) * perc
		   ,0.5f,StartLocation.y + (EndLocation.y - StartLocation.y) * perc);
		//Debug.Log(EndLocation + "/" + StartLocation + "/" + where);
		ActiveChar.gameObject.transform.position = where;
	}

	public override void End (){
		ActiveChar.gameObject.transform.position = new Vector3(EndLocation.x,0.5f,EndLocation.y);
	}
}
