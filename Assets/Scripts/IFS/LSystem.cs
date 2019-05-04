using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class LSystem : MonoBehaviour {
    public List<char> Variables = new List<char>();
    public List<char> Constants = new List<char>();
    public string Axiom;
    //Was originally going to be a monobehavior but might not be necessary
    public List<ProductionRule> Rules = new List<ProductionRule>();
    private Dictionary<char, string> rules = new Dictionary<char, string>();
    //public List<GameObject> ProducerPrefabs = new List<GameObject>(); 
    //public List<IProducer> Producers = new List<IProducer>();
    public Dictionary<char, IProducer<Tree<List<Vector3>>>> Producers = new Dictionary<char, IProducer<Tree<List<Vector3>>>>();
    public GameObject Drawer;
    public float Angle;

    private Stack<Vector3> restorePositions = new Stack<Vector3>();
    private Stack<float> restoreAngles = new Stack<float>();



    public string Generate(int generations) {
        var text = Generate(generations, 0,Axiom);
        Debug.Log(text);
        return text;
    }
    
    private string Generate(int generations,int sentinel,string systemText) {
        if (sentinel < generations) {
            sentinel++;
            var current = systemText;
            var next = new StringBuilder();
            foreach(var c in current) {
                if (Variables.Contains(c))
                {
                    next.Append(rules[c]);
                }
                if (Constants.Contains(c)) {
                    next.Append(c);
                }
            }
            current = next.ToString();
            return Generate(generations, sentinel,current);
        }
        return systemText;
    }

    private void Evaluate(Tree<List<Vector3>> tree,string systemText) {

    }

     void Awake()
    {
        foreach (var pr in Rules) {
            rules[pr.Key] = pr.Value;
        }
    }


}
