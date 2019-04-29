using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeItem : MonoBehaviour
{
    Transform rotator;


    private void Awake()
    {
        rotator = transform.GetChild(0);
    }

    public void Position(Pipe pipe, float curveRotation, float ringRotator)
    {
        transform.SetParent(pipe.transform,false);
        transform.localRotation = Quaternion.Euler(0f,0f,-curveRotation);
        rotator.localPosition = new Vector3(0f,pipe.CurveAngle);
        rotator.localRotation = Quaternion.Euler(ringRotator,0f,0f);
    }
}
