using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : NghiaMono
{
    protected MeshRenderer mRenderer;
    public float animationSpeed = 1.0f;

    protected override void Awake()
    {
        base.Awake();
        mRenderer = GetComponent<MeshRenderer>();
    }
    protected void Update()
    {
        mRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
