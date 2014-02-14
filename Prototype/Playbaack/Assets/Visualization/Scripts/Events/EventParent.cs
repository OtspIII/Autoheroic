using UnityEngine;
using System.Collections;

public class EventParent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	virtual public void Continue(){
	}

	virtual public bool StillRunning(){
		return false;
	}

//	virtual void Begin(){
//
//	}
}
