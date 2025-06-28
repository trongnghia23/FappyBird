using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : NghiaMono
{
    [SerializeField] protected CircleCollider2D _collider;
    [SerializeField] protected Rigidbody2D _rigibody;
    [SerializeField] protected Animator animator;
    protected Vector3 Direction;
    protected float Rotation = 0f;
    public float Gravity = -9.8f;
    public float Strength = 5f;
   
    protected override void Loadcomponents()
    {
        base.Loadcomponents();
        this.LoadTrigger();
        this.LoadRigibody();
    }
    protected virtual void LoadTrigger()
    {
        if (this._collider != null) return;
        this._collider = transform.GetComponent< CircleCollider2D >();
        this._collider.isTrigger = false;
        this._collider.radius = 0.3f;
        Debug.LogWarning(transform.name + " LoadTrigger", gameObject);
    }
    protected virtual void LoadRigibody()
    {
        if (this._rigibody != null) return;
        this._rigibody = GetComponent<Rigidbody2D>();
        this._rigibody.bodyType = RigidbodyType2D.Kinematic;
        this._rigibody.simulated = true;
        Debug.LogWarning(transform.name + " LoadRigibody", gameObject);
    }
    protected void Update()
    {
        this.Move();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        Vector3 pos = transform.position;
        pos.y = 0f;
        transform.position = pos;
        Direction = Vector3.zero;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
    protected virtual void Move()
    {
        if (GameManager.Instance.state != GameState.Playing) return;

        animator.SetFloat("VerticalSpeed", Direction.y);
        if (Input.GetMouseButtonDown(0)|| Input.GetKeyDown(KeyCode.Space)) 
        {
            SoundManager.Instance.PlayMoveNoise();
            animator.Play("Flap", 0, 0f);
            Direction = Vector3.up * Strength;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                SoundManager.Instance.PlayMoveNoise();
                animator.Play("Flap", 0, 0f);
                Direction = Vector3.up * Strength;
            }
        }
        Direction.y += Gravity * Time.deltaTime;
        Rotation = Mathf.Lerp(Rotation, Direction.y * 5f, Time.deltaTime * 5f);
        transform.position += Direction * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, Rotation);
    }
}
