using UnityEngine;


public class despawnbydistance : Despawn
{
    [SerializeField] protected float dislimit = 70f;
    [SerializeField] protected float distance = 0f;
    [SerializeField] protected Transform maincam;

    protected override void Loadcomponents()
    {
        base.Loadcomponents();
        this.loadcamera();
    }
    protected virtual void loadcamera()
    {
        if (this.maincam != null) return;
        this.maincam = Camera.main.transform;
        Debug.Log(transform.parent.name + ": Loadcamera", gameObject);
    }
    public override bool CanDespawn()
    {
        this.distance = Vector3.Distance(transform.position, this.maincam.position);
        if (this.distance > this.dislimit) return true;
        return false;
    }


}