using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StemGrowthRules : MonoBehaviour
{
    public MeshFilter MeshFilter = null;

    public float InitialDiameter = 1.0f;

    public float DiameterChangeFactor = 0.9f;

    public float SectionHeight = 1;

    public int SectionCount = 5;

    public float CenterNoiseCoefficient = 0.1f;

    private LineRenderer lineRenderer = null;

    // Use this for initialization
    void Start()
    {
        if (MeshFilter == null)
        {
            throw new UnityException("Must have a mesh filter");
        }
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        var centerPositions = new List<Vector3>();
        var radii = new List<float>();
        centerPositions.Add(gameObject.transform.position);
        var radius = InitialDiameter;
        radii.Add(radius);
        for (var i = 1; i < SectionCount; i++)
        {
            centerPositions.Add(new Vector3
            {
                x = centerPositions[i - 1].x + GetCenterVectorValue(),
                y = centerPositions[i - 1].y + SectionHeight,
                z = centerPositions[i - 1].z + GetCenterVectorValue()
            });
            radius *= DiameterChangeFactor;
            radii.Add(radius);
        }
        MeshDrawUtils.GenerateMultiTube(MeshFilter, 10, centerPositions, radii);
    }

    private float GetCenterVectorValue()
    {
        return Random.Range(-1 * CenterNoiseCoefficient,CenterNoiseCoefficient);
    }

}
