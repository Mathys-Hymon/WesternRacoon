using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShader : MonoBehaviour
{
    public Shader shader;

    private Material material;

    void Start()
    {
        material = new Material(shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
