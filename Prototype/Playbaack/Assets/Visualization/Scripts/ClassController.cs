using UnityEngine;
using System.Collections;
using AM;

public class ClassController : MonoBehaviour {

	public string Name;
	public CharClass Class;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update(){

	}
	
	public void DoAnimation(Actions act){
		switch (act)
		{
		case Actions.Walk:
		{
			particleSystem.startColor = Color.blue;
			particleSystem.Emit(10);
			Debug.Log("A");
			break;
		}
		case Actions.Attack:
		{
			particleSystem.startColor = Color.red;
			particleSystem.Emit(10);
			break;
		}
		}
	}
}
