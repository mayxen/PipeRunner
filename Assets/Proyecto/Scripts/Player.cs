using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PipeSystem PlayerPipeSystem;
    public float Velocity;
    public float RotationVelocity;

    float distanceTravelled;
    float deltaToRotation;
    float systemRotation;
    Transform world;
    Transform rotator;
    Pipe currentPipe;
    float worldRotation;
    float avatarRotation;
    int change;
    

    private void Start()
    {
        world = PlayerPipeSystem.transform.parent;
        rotator = transform.GetChild(0);
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
        if(change > 2)
        {
            delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;

            currentPipe = PlayerPipeSystem.SetUpNextPipe();
            SetUpCurrentPipe();
            systemRotation = delta * deltaToRotation;
            change = 0;
        }
        if(systemRotation >= currentPipe.CurveAngle)
        {
            change++;
        }
        PlayerPipeSystem.transform.rotation = Quaternion.Euler(0f,0f,systemRotation);
        UpdateAvatarRotation();
    }

    void UpdateAvatarRotation()
    {
        avatarRotation += RotationVelocity * Time.deltaTime * Input.GetAxis("Horizontal");
        if (avatarRotation < 0f)
            avatarRotation += 360f;
        else if (avatarRotation >= 360f)
            avatarRotation -= 360f;

        rotator.localRotation = Quaternion.Euler(avatarRotation,0f,0f);
    }
}
