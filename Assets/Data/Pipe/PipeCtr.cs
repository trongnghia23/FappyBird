using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCtr : NghiaMono
{
    public float Speed = 5f;
    public float LeftEdge;

    protected override void Start()
    {
        base.Start();
        LeftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }
    protected virtual void Update()
    {
        transform.position += Vector3.left * Speed * Time.deltaTime;
       
    }
}
