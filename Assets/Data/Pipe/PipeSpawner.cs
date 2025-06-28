using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : spawner
{
    protected static PipeSpawner instance;
    public static PipeSpawner Instance { get => instance; }
    public static string Pipe1 = "Pipe_1";
    public float spawnrate = 1.0f;
    public float minheight = -1.0f;
    public float maxheight = 1.0f;
    protected override void Awake()
    {
        base.Awake();
        if (PipeSpawner.instance != null) Debug.LogError("only one PipeSpawner allow to exist");
        PipeSpawner.instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        InvokeRepeating(nameof(spawnpipe), spawnrate,spawnrate);
    }

    protected void OnDisable()
    {
        CancelInvoke(nameof(spawnpipe));
    }
    protected virtual void spawnpipe()
    { 
        if (GameManager.Instance.state != GameState.Playing) return;

        Vector3 spawnPos = transform.position + Vector3.up * Random.Range(minheight, maxheight);
        Transform pipe = this.Spawn(Pipe1, spawnPos, Quaternion.identity);
        pipe.gameObject.SetActive(true);

    }
}
