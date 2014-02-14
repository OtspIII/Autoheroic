using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AM;

public class EventController : MonoBehaviour {


//	public IDictionary<string,GameObject> Classes;
	Dictionary<CharClass,ClassController> ClassReference = new Dictionary<CharClass, ClassController>();
	public List<GameObject> Classes;
	Dictionary<TTerrain,TerrainController> TerrainReference = new Dictionary<TTerrain, TerrainController>();
	public List<GameObject> Terrains;
	TerrainController[,] TerrainMap;
	List<ClassController> Characters = new List<ClassController>();
	Dictionary<GEventType,EventParent> Events = new Dictionary<GEventType, EventParent>();
	EventParent CurrentEvent = null;
	List<GameEvent> EventStack = new List<GameEvent>();


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentEvent != null){
			if (CurrentEvent.StillRunning()){
				CurrentEvent.Continue();
			}
			else{
				GotoNextEvent();
			}
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			QueueEvent(new GameEvent(GEventType.Walk,Characters[Random.Range(0,Characters.Count)]));
		}
		if (Input.GetKeyDown(KeyCode.Z))
			QueueEvent(new GameEvent(GEventType.Attack,Characters[Random.Range(0,Characters.Count)]));
	}

	public void Setup(SetupData data){
		//First off, how do we know what to build when we're told that a class/obj/whatever is needed?
		
		//These will be largely asked for as strings, so let's set up some easy ways to convert those strings into objects.
		SetupRefs();
		TerrainMap = new TerrainController[data.TerrainMap.GetLength(0),data.TerrainMap.GetLength(1)];
		for (int i = 0; i < data.TerrainMap.GetLength(0); i++)
			for (int j = 0; j < data.TerrainMap.GetLength(1); j++){
			TerrainController tt = GetTerrain(data.TerrainMap[i,j]);
			Debug.Log(tt + " / " + data.TerrainMap[i,j]);
			GameObject go =	(GameObject)Instantiate(tt.gameObject, new Vector3(i, 0, j), Quaternion.identity);
			TerrainMap[i,j] = (TerrainController)go.GetComponent("TerrainController");
		}
		foreach (CharController c in data.Characters){
			ClassController cl = null;
			if (ClassReference.ContainsKey(c.Class)){
				GameObject go = (GameObject)Instantiate(GetClass(c.Class).gameObject,
				   new Vector3(c.Location.x, 0.5f, c.Location.y), Quaternion.identity);
				Characters.Add((ClassController)go.GetComponent("ClassController"));
			}
		}
	}

	void SetupRefs(){
		foreach (GameObject c in Classes){
			if (c.GetComponent("ClassController") == null)
				continue;
			ClassController cont = (ClassController)c.GetComponent("ClassController");
			ClassReference.Add(cont.Class, cont);
		}
		foreach (GameObject c in Terrains){
			if (c.GetComponent("TerrainController") == null)
				continue;
			TerrainController cont = (TerrainController)c.GetComponent("TerrainController");
			TerrainReference.Add(cont.TerrainType, cont);
		}
		Events.Add(GEventType.Walk,(EventParent)GetComponent("WalkEvent"));
		Events.Add(GEventType.Attack,(EventParent)GetComponent("AttackEvent"));
	}

	ClassController GetClass(CharClass c){
		if (ClassReference.ContainsKey(c))
			return ClassReference[c];
		return null;
	}

	TerrainController GetTerrain(TTerrain name){
		if (TerrainReference.ContainsKey(name))
			return TerrainReference[name];
		Debug.Log("UHOH: " + name);
		return null;
	}

	EventParent GetEvent(GEventType name){
		if (Events.ContainsKey(name))
			return Events[name];
		return null;
	}

	public void QueueEvent(GameEvent e){
		EventStack.Add(e);
		if (CurrentEvent == null)
			StartEvent(EventStack[0]);
	}

	public void StartEvent(GameEvent e){
		EventParent manager = GetEvent(e.Type);
		if (manager == null) 
			return;
		CurrentEvent = manager;
		switch (e.Type)
		{
		case GEventType.Walk:
		{
			((WalkEvent)manager).Begin(e.MainChar);
			break;
		}
		case GEventType.Attack:
		{
			((AttackEvent)manager).Begin(e.MainChar);
			break;
		}
		}
	}

	void GotoNextEvent(){
		CurrentEvent = null;
		EventStack.RemoveAt(0);
		if (EventStack.Count > 0)
			StartEvent(EventStack[0]);
	}
}
