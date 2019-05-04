using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoreMeshGenerator : MonoBehaviour
{

    public MeshFilter MeshFilter = null;

    public int sectionCount = 5;

    private LineRenderer lineRenderer = null;
    // Use this for initialization
    void Start()
    {
        if (MeshFilter == null)
        {
            throw new UnityException("Must have a mesh filter");
        }
        lineRenderer = gameObject.GetComponent<LineRenderer>() as LineRenderer;
        var centerPositions = new List<Vector3>();
        var radii = new List<float>();
        centerPositions.Add(gameObject.transform.position);
        radii.Add(1);
        for (var i = 1; i < sectionCount; i++){
            centerPositions.Add(new Vector3 {
                x = centerPositions[i - 1].x,
                y = centerPositions[i - 1].y + 0.3f,
                z = centerPositions[i - 1].z
            });
            radii.Add(1);
        }
        MeshDrawUtils.GenerateMultiTube(MeshFilter, 10,centerPositions, radii);
        MeshDrawUtils.RenderDebugLines(MeshFilter.mesh.vertices, lineRenderer);
        GetComponent<MeshDebugging>().Show();
        //GenerateMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
