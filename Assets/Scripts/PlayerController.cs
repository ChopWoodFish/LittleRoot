using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;
    public Animator anim;
    public float speed = 1f;
    public float jumpForce = 50f;

    // 用于转向的参数
    private float scaleX;
    private float scaleY;

    public PlayerState state;
    public State stateFlag;
    public Collider2D colliderUnder;

    public List<Collider2D> allColliders = new List<Collider2D>();
    public RootRangeChecker rangeChecker;

    private void Awake()
    {
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }

    private void Start()
    {
        rangeChecker.gameObject.SetActive(false);
        
        
        stateFlag = State.OnGround;
        state = new OnGroundState(this);
    }

    private void Update()
    {
        // Move();
        var newState = state.Update();
        if (newState != null)
        {
            state.Exit();
            state = newState;
            state.Enter();
        }
    }

    public void Move()
    {
        // -1, 0, 1
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        
        // 动画控制
        anim.SetTrigger(horizontalMove == 0 ? "Idle" : "Walk");
    
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        // 转身
        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove * scaleX, scaleY, 1);
        }
        
        // 其他按键
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("btn jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var otherCollider = other.collider;
        allColliders.Add(otherCollider);
        Debug.Log($"add collider object {otherCollider.gameObject.name}");

        // 若是plateform，判断是否在脚底
        if (otherCollider.CompareTag("Platform"))
        {
            var hitPoint = other.GetContact(0);
            var hitPointY = hitPoint.point.y;
            var underBoundY = transform.position.y - boxCollider.bounds.size.y / 2;
            Debug.Log($"hitPointY: {hitPointY} underBoundY: {underBoundY}");
            if (hitPointY <= underBoundY)
            {
                Debug.Log("collide with box collider under bound!");
                colliderUnder = otherCollider;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        var otherCollider = other.collider;
        allColliders.Remove(otherCollider);
        Debug.Log($"remove collider object {otherCollider.gameObject.name}");

        if (otherCollider == colliderUnder)
            colliderUnder = null;
    }
}

public enum State
{
    // 在地面上，在生根，在落下，被攻击的无敌帧（大概）
    OnGround, OnRoot, OnAir, OnHit
}
