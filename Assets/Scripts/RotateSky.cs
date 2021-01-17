using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour
{
    [Range(0f, 10f)]
    public float rotationSpeed = 2f;

    private float rotation = 0f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", rotation += rotationSpeed * Time.deltaTime);
    }
}
