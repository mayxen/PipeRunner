using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSystem : MonoBehaviour
{
    public Pipe PipePrefab;
    public int PipeCount;
    

    Pipe[] pipes;
    

    private void Awake()
    {
        pipes = new Pipe[PipeCount];

        for (int i = 0; i < pipes.Length; i++)
        {
            Pipe pipe = pipes[i] = Instantiate(PipePrefab);
            pipe.transform.SetParent(transform,false);
            pipe.Generate();

            if(i > 0)
            {
                pipe.AlingWith(pipes[i - 1]);
            }
        }
        AlignNextPipeWithrigin();
    }

    public Pipe SetUpFirstPipe()
    {
        transform.localPosition = new Vector3(0f,-pipes[1].CurveRadius);
        return pipes[1];
    }

    public Pipe SetUpNextPipe()
    {
        ShiftPipes();
        AlignNextPipeWithrigin();
        pipes[pipes.Length - 1].Generate();
        pipes[pipes.Length - 1].AlingWith(pipes[pipes.Length - 2]);
        transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
        return pipes[1];
    }

    private void AlignNextPipeWithrigin()
    {
        Transform transformToAlign = pipes[1].transform;
        
        pipes[1].transform.SetParent(transformToAlign);
        transformToAlign.localPosition = Vector3.zero;
        transformToAlign.localRotation = Quaternion.identity;
        pipes[1].transform.SetParent(transform);
       
    }

    private void ShiftPipes()
    {
        Pipe temp = pipes[0];
        for (int i = 1; i < pipes.Length; i++)
        {
            pipes[i - 1] = pipes[i];
        }
        pipes[pipes.Length - 1] = temp;
    }
}
