using UnityEngine;
using System.Collections;

public class GeneRepresenter : MonoBehaviour {

    public IGeneExpresser[] Expressers { get; set; }

    void Awake() {
        Expressers = new IGeneExpresser[5] {
            new SizeExpresser(1),
            new ColorExpressor(ColorComponent.Blue),
            new ColorExpressor(ColorComponent.Green),
            new ColorExpressor(ColorComponent.Red),
            new ColorExpressor(ColorComponent.Alpha),
        };
    }

	// Use this for initialization
	void Start () {
        
        var genes = this.GetComponent<Genes>();
        for (var i=0;i<this.Expressers.Length;i++) {
            if (i < genes.GeneCount) {
                this.Expressers[i].Express(this.gameObject,genes.GeneValues[i]);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
