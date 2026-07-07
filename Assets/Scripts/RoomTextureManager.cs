using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTextureManager : MonoBehaviour
{
    [Header("Renderers")]
    public MeshRenderer floorRendererFront;
    public MeshRenderer floorRendererLeft;
    public MeshRenderer floorRendererRight;
    public MeshRenderer floorRendererBack;
    public MeshRenderer ceilingRenderer;
    public MeshRenderer[] wallRenderers;

    [Header("Textures")]
    public Texture2D floorTexture;
    public Texture2D ceilingTexture;
    public Texture2D wallTexture; // same texture for all walls

    void Start()
    {
        // Floors
        ApplyTexture(floorRendererFront, floorTexture);
        ApplyTexture(floorRendererLeft, floorTexture);
        ApplyTexture(floorRendererRight, floorTexture);
        ApplyTexture(floorRendererBack, floorTexture);

        // Ceiling
        ApplyTexture(ceilingRenderer, ceilingTexture);

        // Walls
        if (wallRenderers != null && wallTexture != null)
        {
            foreach (var r in wallRenderers)
            {
                ApplyTexture(r, wallTexture);
            }
        }
    }

    void ApplyTexture(MeshRenderer renderer, Texture2D texture)
    {
        if (renderer == null || texture == null) return;

        // Create a new material instance so we don't edit shared material
        Material mat = new Material(renderer.material);
        mat.mainTexture = texture;
        renderer.material = mat;
    }
}