using UnityEngine;
using System.Collections;

public class MeshDebugging : MonoBehaviour {
    public GameObject VertexVisualPrefab;
    private MeshFilter meshFilter;

	// Use this for initialization
	void Start () {
        meshFilter = this.gameObject.GetComponent<MeshFilter>();
        
    }

    public void Show() {
        this.meshFilter = this.gameObject.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            for (var i = 0; i < meshFilter.mesh.vertices.Length; i++)
            {
                var vertexVisualSphere = GameObject.Instantiate(VertexVisualPrefab);
                vertexVisualSphere.transform.position = meshFilter.mesh.vertices[i];
                var textChild = vertexVisualSphere.GetComponentInChildren<TextMesh>();
                textChild.text = i.ToString();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
