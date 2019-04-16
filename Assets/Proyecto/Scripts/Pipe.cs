using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float CurveRadius;
    public float PipeRadius;

    public int CurveSegmentCount;
    public int PipeSegmentCount;

    public float RingDistance;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = mesh =  new Mesh();
        mesh.name = "PipeMesh";
        SetVertices();
        SetTriangles();
        mesh.RecalculateNormals();

    }

    private void SetTriangles()
    {
        triangles = new int[PipeSegmentCount * CurveSegmentCount * 6];
        for (int t = 0,i=0; t < triangles.Length; t+=6,i+=4)
        {
            triangles[t] = i;
            triangles[t + 1] = triangles[t + 4] = i + 1;
            triangles[t + 2] = triangles[t + 3] = i + 2;
            triangles[t + 5] = i + 3;
        }
        mesh.triangles = triangles;
    }

    private void SetVertices()
    {
        vertices = new Vector3[PipeSegmentCount * CurveSegmentCount * 4];
        float uStep = RingDistance / CurveRadius;
        CreateFirstQuadring(uStep);
        int iDelta = PipeSegmentCount * 4;

        for (int i = iDelta, u = 2; u <= CurveSegmentCount; u++, i += iDelta)
        {
            CreateQuadRing(u * uStep, i);
        }

        mesh.vertices = vertices;
    }

    private void CreateQuadRing(float u, int i)
    {
        float vStep = (2f * Mathf.PI) / PipeSegmentCount;
        int ringOffSet = PipeSegmentCount * 4;

        Vector3 vertex = GetPointOnTorus(u,0f);

        for (int v = 1; v <= PipeSegmentCount; v++,i+=4)
        {
            vertices[i] = vertices[i - ringOffSet + 2];
            vertices[i + 1] = vertices[i - ringOffSet + 3];
            vertices[i + 2] = vertex;
            vertices[i + 3] = vertex = GetPointOnTorus(u, v * vStep);
        }
    }

    private void CreateFirstQuadring(float uStep)
    {
        float vStep = (2f * Mathf.PI) / PipeSegmentCount;

        Vector3 vertexA = GetPointOnTorus(0f, 0f);
        Vector3 vertexB = GetPointOnTorus(uStep,0f);

        for (int v = 1, i = 0; v <= PipeSegmentCount; v++,i += 4)
        {
            vertices[i] = vertexA;
            vertices[i + 1] = vertexA =GetPointOnTorus(0f, v * vStep);
            vertices[i + 2] = vertexB;
            vertices[i + 3] = vertexB= GetPointOnTorus(uStep, v * vStep);
        }
    }

    private Vector3 GetPointOnTorus(float u, float v)
    {
        Vector3 p;
        float r = (CurveRadius + PipeRadius * Mathf.Cos(v));
        p.x = r * Mathf.Sin(u);
        p.y = r * Mathf.Cos(u);
        p.z = PipeRadius * Mathf.Sin(v);
        return p;
    }

    //private void OnDrawGizmos()
    //{
    //    float uStep = (2f * Mathf.PI) / CurveSegmentCount;
    //    float vStep = (2f * Mathf.PI) / PipeSegmentCount;

    //    for (int u = 0; u < CurveSegmentCount; u++)
    //    {
    //        for (int v = 0; v < PipeSegmentCount; v++)
    //        {
    //            Vector3 point = GetPointOnTorus(u * uStep, v * vStep);
    //            Gizmos.color = new Color(1f, (float)v / PipeSegmentCount, (float)u / CurveSegmentCount);
    //            Gizmos.DrawSphere(point,0.1f);
    //        }
    //    }
    //}
}
