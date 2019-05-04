using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrawer : MonoBehaviour {

    public int Generations = 1;

	// Use this for initialization
	void Start () {
        var lSystem = gameObject.GetComponent<LSystem>();
        var text = lSystem.Generate(Generations);
        var drawer = gameObject.GetComponent<LRLSystemDrawer>();

        drawer.AddAction('0', drawer.GoForward);
        drawer.AddAction('1', drawer.GoForward);
        drawer.AddAction('[', drawer.TurnLeft);
        drawer.AddAction(']', drawer.TurnRight);

        drawer.Parse(text);
        Debug.Log(text);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
