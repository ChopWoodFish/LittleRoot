using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EarthSpike : MonoBehaviour
{
    private Collider2D _collider;
    public int damage = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.gameObject;
        if (player.name == ConstValue.Player)
        {
            Debug.Log("player hitted");
            player.GetComponent<Fighter>().GetHit(damage);
        }
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("EarthSpike has no Collider!");
            throw new Exception("Missing Components");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
