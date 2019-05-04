using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static partial class MeshDrawUtils
{
    public static Vector3[] MakeRing(Vector3 centerLocation, float radius, int resolution,bool includeCenter = true)
    {
        if (resolution < 3)
        {
            Debug.LogWarning("Resolution must be 3 or greater");
            resolution = 3;
        }
        List<Vector3> verts = new List<Vector3>();
        if (includeCenter)
        {
            verts.Add(centerLocation);
        }

        int stepSize = (int)Mathf.Floor(360 / resolution);
        for (var i = resolution; i > 0; i--)
        {
            var angle = stepSize * i;
            var v = new Vector3
            {
                x = (radius * Mathf.Cos(angle * Mathf.Deg2Rad)) + centerLocation.x,
                y = centerLocation.y,
                z = (radius * Mathf.Sin(angle * Mathf.Deg2Rad)) + centerLocation.z
            };
            verts.Add(v);

        }
        return verts.ToArray();
    }

    public static Vector3[] MakeReverseRing(Vector3 centerLocation, float radius, int resolution, bool includeCenter = true)
    {
        if (resolution < 3)
        {
            Debug.LogWarning("Resolution must be 3 or greater");
            resolution = 3;
        }
        List<Vector3> verts = new List<Vector3>();
        if (includeCenter)
        {
            verts.Add(centerLocation);
        }

        int stepSize = (int)Mathf.Floor(360 / resolution);
        for (var i = 0; i < resolution; i++)
        {
            var angle = stepSize * i;
            var v = new Vector3
            {
                x = (radius * Mathf.Cos(angle * Mathf.Deg2Rad)) + centerLocation.x,
                y = centerLocation.y,
                z = (radius * Mathf.Sin(angle * Mathf.Deg2Rad)) + centerLocation.z
            };
            verts.Add(v);

        }
        return verts.ToArray();
    }

    public static List<int> MakeTrisForRing(Vector3[] ring)
    {
        var center = ring[0];
        var tris = new List<int>();
        for (var i = 1; i < ring.Length - 1; i++)
        {
            tris.Add(0);
            tris.Add(i);
            tris.Add(i + 1);
        }
        //close
        tris.Add(0);
        tris.Add(ring.Length - 1);
        tris.Add(1);
        return tris;
    }

    public static List<Vector2> MakeUVs(Vector3[] verts)
    {
        var uvs = new List<Vector2>();
        foreach (var v in verts)
        {
            uvs.Add(new Vector2(0f, 0f));
        }
        return uvs;
    }

    public static void RenderDebugLines(Vector3[] verts,LineRenderer lineRenderer)
    {
        lineRenderer.SetVertexCount(verts.Length);
        lineRenderer.SetPositions(verts);
    }

    public static void GenerateCircleMesh(MeshFilter filter)
    {
        var width = 10;
        List<Vector3[]> verts = new List<Vector3[]>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        verts.Add(MakeRing(filter.gameObject.transform.position, 1, 36));
        tris = MakeTrisForRing(verts[0]);
        uvs = MakeUVs(verts[0]);
        // Unfold the 2d array of verticies into a 1d array.
        //Vector3[] unfolded_verts = new Vector3[width * width];
        Vector3[] unfolded_verts = verts[0];
        /*int i = 0;
        foreach (Vector3[] v in verts)
        {
            v.CopyTo(unfolded_verts, i * width);
            i++;
        }*/

        // Generate the mesh object.
        Mesh ret = new Mesh();
        ret.vertices = unfolded_verts;
        ret.triangles = tris.ToArray();
        ret.uv = uvs.ToArray();

        // Assign the mesh object and update it.
        ret.RecalculateBounds();
        ret.RecalculateNormals();
        filter.mesh = ret;
    }

}
