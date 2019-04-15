using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float CurveRadius;
    public float PipeRadius;

    public int CurveSegmentCount;
    public int PipeSegmentCount;

    private Vector3 GetPointsOnTorus(float u, float v)
    {
        Vector3 p;
        float r = (CurveRadius + PipeRadius * Mathf.Cos(v));
        p.x = r * Mathf.Sin(u);
        p.y = r * Mathf.Cos(u);
        p.z = PipeRadius * Mathf.Sin(v);
        return p;
    }

    private void OnDrawGizmos()
    {
        float uStep = (2f * Mathf.PI) / CurveSegmentCount;
        float vStep = (2f * Mathf.PI) / PipeSegmentCount;

        for (int u = 0; u < CurveSegmentCount; u++)
        {
            for (int v = 0; v < PipeSegmentCount; v++)
            {
                Vector3 point = GetPointsOnTorus(u * uStep, v * vStep);
                Gizmos.color = new Color(1f, (float)v / PipeSegmentCount, (float)u / CurveSegmentCount);
                Gizmos.DrawSphere(point,0.1f);
            }
        }
    }
}
