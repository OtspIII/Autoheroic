using UnityEngine;
using System.Collections;
using AM;

public class ClassController : MonoBehaviour {

	public string Name;
	public string UniqueName;
	public CharClass Class;
	GameObject deathSpray;
	public GameObject DeathSpray {get{return deathSpray;}}
	public GameObject DeathSprayClass;

	// Use this for initialization
	void Start () {
		deathSpray = (GameObject)Instantiate(DeathSprayClass,new Vector3(9999,9999,9199),Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update(){

	}

	public void ImprintFrom(CharController who){
		Name = who.Name;
		UniqueName = who.UniqueName;
	}
	
	public void DoAnimation(Actions act){
		switch (act)
		{
		case Actions.Walk:
		{
//			particleSystem.startColor = Color.blue;
//			particleSystem.Emit(10);
			break;
		}
		case Actions.Attack:
		{
			particleSystem.startColor = Color.white;
			particleSystem.Emit(10);
			break;
		}
		}
	}
}
