using UnityEngine;
using System.Collections;

public class WalkEvent : EventParent {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Begin(ClassController who){
		who.DoAnimation(AM.Actions.Walk);
	}

	public override bool StillRunning (){
		return false;
	}
}
