using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour
{
    [Range(0f, 1f)]
    public float rotationSpeed = .1f;

    private float rotation = 0f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", rotation += rotationSpeed);
    }
}
