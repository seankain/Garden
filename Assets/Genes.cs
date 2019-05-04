using UnityEngine;
using System.Collections;

public class Genes : MonoBehaviour {
    public int GeneCount = 10;
    public int[] GeneValues { get; private set; }

    public Genes(int[] values) {
        if (values != null) {
            GeneValues = values;
        }
    }

    public Genes() { }

    void Awake() {
        BuildGeneValues();
    }

	// Use this for initialization
	void Start () {
        
        
	}

    void BuildGeneValues()
    {
        this.GeneValues = new int[this.GeneCount];
        for (var i = 0; i < GeneCount; i++) {
            this.GeneValues[i] =(int)Mathf.Floor(Random.Range(0, 256));
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
