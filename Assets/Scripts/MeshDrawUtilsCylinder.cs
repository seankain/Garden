using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static partial class MeshDrawUtils
{
    public static List<Vector3> MakeCylinderSectionVerts(Vector3 bottomCenter,float bottomRadius, Vector3 topCenter, float topRadius,int resolution = 36) {
        var verts = new List<Vector3>();
        var bottomRing = MakeReverseRing(bottomCenter, bottomRadius, resolution,false);
        var topRing = MakeReverseRing(topCenter, topRadius, resolution,false);
        verts.AddRange(bottomRing);
        verts.AddRange(topRing);
        return verts;
    }

    public static List<Vector3> MakeMultiSectionVerts(List<Vector3> centerPositions, List<float> radii, int resolution) {
        var verts = new List<Vector3>();
        for (var i = 0; i < centerPositions.Count; i++) {
            verts.AddRange(MakeReverseRing(centerPositions[i], radii[i], resolution, false));
        }
        return verts;
    }

    public static List<int> MakeMultiSectionTris(int ringLength,int sectionCount) {
        var tris = new List<int>();
        for (var i = 0; i + 1 < sectionCount; i+=1) {
            AppendTris(tris, i * ringLength,ringLength);
        }
        
        return tris;
    }

    public static void AppendTris(List<int> tris,int startingIndex, int ringLength) {
        var endIndex = (ringLength + startingIndex) - 1;        
        var j = startingIndex;
        for (var i = startingIndex; i < endIndex; i++)
        {
            tris.Add(i);
            tris.Add(i + ringLength);
            tris.Add(i + 1);

            tris.Add(i + ringLength);
            tris.Add((i + ringLength) + 1);
            tris.Add(i + 1);
        }

        tris.Add((startingIndex + (ringLength * 2)) - 1);
        tris.Add(endIndex + 1);
        tris.Add(endIndex);

        tris.Add(endIndex);
        tris.Add(startingIndex + ringLength);
        tris.Add(startingIndex);
    }

    public static List<int> MakeTrisForCylinderSection(List<Vector3> rings,int ringLength) {
        var tris = new List<int>();
        for (var i = 0; i < ringLength -1; i++)
        {
            tris.Add(i);
            tris.Add(i + ringLength);
            tris.Add(i + 1);

            tris.Add(i + ringLength);
            tris.Add((i + ringLength) + 1);
            tris.Add(i + 1);
        }

        tris.Add(0);
        tris.Add(ringLength - 1);
        tris.Add((ringLength * 2) - 1);

        tris.Add(0);
        tris.Add((ringLength * 2) - 1);
        tris.Add(ringLength);

        return tris;
    }

    public static void GenerateTube(MeshFilter filter,int resolution, int length, float bottomRadius, float topRadius) {
        var pos = filter.gameObject.transform.position;
        var upperPos = new Vector3(pos.x, pos.y + length, pos.z);
        var verts = MakeCylinderSectionVerts(pos, bottomRadius, upperPos, topRadius);
        var tris = MakeTrisForCylinderSection(verts, resolution);
        var uvs = MakeUVs(verts.ToArray());
        GenerateMesh(filter, verts, tris, uvs);
    }

    public static void GenerateMultiTube(MeshFilter filter,int resolution, List<Vector3> centerPositions,List<float> radii) {
        var verts = MakeMultiSectionVerts(centerPositions, radii, resolution);
        var tris = MakeMultiSectionTris(resolution, centerPositions.Count);
        var uvs = MakeUVs(verts.ToArray());
        GenerateMesh(filter, verts, tris, uvs);
    }

    public static void GenerateMesh(MeshFilter filter, List<Vector3>verts,List<int> tris, List<Vector2> uvs) {
        Mesh ret = new Mesh();
        ret.vertices = verts.ToArray();
        ret.triangles = tris.ToArray();
        ret.uv = uvs.ToArray();

        // Assign the mesh object and update it.
        ret.RecalculateBounds();
        ret.RecalculateNormals();
        filter.mesh = ret;
    }

}
