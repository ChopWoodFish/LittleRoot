using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EarthSpike : MonoBehaviour
{
    public float damage = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == ConstValue.Player)
        {
            Debug.Log("player hitted");
            // hurt the player
            // player.health-=damage;
        }
    }

    private void Awake()
    {
        if (GetComponent<Rigidbody>() == null && GetComponent<Collider>() == null)
        {
            Debug.LogError("EarthSpike has no Collider!");
            throw new Exception("Missing Components");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
