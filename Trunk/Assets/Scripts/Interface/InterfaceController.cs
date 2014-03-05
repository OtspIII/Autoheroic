using UnityEngine;
using System.Collections;
using Cub.View;

public class InterfaceController : MonoBehaviour {

    public bool InterfaceComplete = false;
    public SetupData Data { get; private set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Box(new Rect(100, 100, 500, 100), "YO");
    }

    public void Setup(EventController manager, SetupData data)
    {
        Data = data;
    }
}
