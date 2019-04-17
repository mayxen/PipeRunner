using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PipeSystem PlayerPipeSystem;
    public float Velocity;

    float distanceTravelled;
    float deltaToRotation;
    float systemRotation;
    Transform world;
    Pipe currentPipe;
    float worldRotation;

    private void Start()
    {
        world = PlayerPipeSystem.transform.parent;
        currentPipe = PlayerPipeSystem.SetUpFirstPipe();
        SetUpCurrentPipe();
        //deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
    }

    private void SetUpCurrentPipe()
    {
        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        worldRotation += currentPipe.RelativeRotation;
        if (worldRotation < 0f)
            worldRotation += 360f;
        else if (worldRotation >= 360f)
            worldRotation = 360f;

        world.localRotation = Quaternion.Euler(worldRotation,0f,0f);
    }

    private void Update()
    {
        float delta = Velocity * Time.deltaTime;
        distanceTravelled += delta;
        systemRotation += delta * deltaToRotation;

        if(systemRotation >= currentPipe.CurveAngle)
        {
            delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
            currentPipe = PlayerPipeSystem.SetUpNextPipe();
            SetUpCurrentPipe();
            systemRotation = delta * deltaToRotation;
        }
        PlayerPipeSystem.transform.rotation = Quaternion.Euler(0f,0f,systemRotation);
    }
}
