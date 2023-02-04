using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1f;

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
        // Debug.Log($"horizontalMove: {horizontalMove}");

        // 不要惯性
        // if (horizontalMove != 0)
        // {
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
            // 转身
            if (horizontalMove != 0)
            {
                transform.localScale = new Vector3(horizontalMove * scaleX, scaleY, 1);   
            }
            // }

    }
}
