using UnityEngine;
using System.Collections;

public class AttackEvent : EventParent {

	float TimerMax = 0.5f;
	float Timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Begin(ClassController who){
		who.DoAnimation(AM.Actions.Attack);
		Timer = TimerMax;
	}

	public override void Continue ()
	{
		Timer -= Time.deltaTime;
	}

	public override bool StillRunning (){
		if (Timer > 0) return true;
		return false;
	}

}
