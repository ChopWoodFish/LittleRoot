using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public float speed = 1f;
    public float jumpForce = 50f;

    // 用于转向的参数
    private float scaleX;
    private float scaleY;

    private void Awake()
    {
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
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
}
