using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeDespawn : despawnbydistance
{
    public override void DespawnObject()
    {
        PipeSpawner.Instance.Despawn(transform.parent);
    }
    protected override void ResetValue()
    {
        base.ResetValue();
        this.dislimit = 20f;
    }
}
