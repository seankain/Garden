using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(LineRenderer))]
public class LRLSystemDrawer : MonoBehaviour
{

    public Dictionary<char, Action> Actions = new Dictionary<char, Action>();
    public float SegmentLength = 0.1f;
    public float Angle = 45.0f;
    private LineRenderer lineRenderer;
    private List<LineRenderer> lineRenderers;
    private Tree<List<Vector3>> positions;
    //private List<Vector3> positions = new List<Vector3>();
    private Stack<Vector3> restorePositions = new Stack<Vector3>();
    private Stack<float> restoreAngles = new Stack<float>();
    private float currentTurnAngle = 0;
    private Vector3 currentPosition;

    public void AddAction(char key, Action drawAction) {
        Actions.Add(key, drawAction);
    }

    public void GoForward() {
        var last = positions[positions.Count - 1];
        var next = new Vector3(Mathf.Sin(Mathf.Deg2Rad * currentTurnAngle),last.y + SegmentLength,last.z);
        positions.Add(next);
    }

    public void RestoreLatest() {
        currentPosition = restorePositions.Pop();
        currentTurnAngle = restoreAngles.Pop();
        var walkBackPositions = positions.ToArray();
        for (var i = walkBackPositions.Length - 1; i>= 0; i--) {
            positions.Add(walkBackPositions[i]);
            if (walkBackPositions[i].Equals(currentPosition)) {
                break;
            }
        }
        //positions.Add(currentPosition);
    }

    public void PushPosition() {
        restorePositions.Push(positions[positions.Count - 1]);
        restoreAngles.Push(currentTurnAngle);
    }

    public void TurnLeft() {
        PushPosition();
        currentTurnAngle = -Angle;
    }

    public void TurnRight() {
        RestoreLatest();
        currentTurnAngle = Angle;
    }

    public void Parse(string text) {
        foreach (var c in text) {
            Actions[c]();
        }
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }

     void Awake()
    {
        positions = new Tree<List<Vector3>>(new List<Vector3> { gameObject.transform.position});
        //positions.Add(gameObject.transform.position);
        //lineRenderer = GetComponent<LineRenderer>();
        lineRenderers = new List<LineRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        //positions.Add(gameObject.transform.position);
        //lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

