using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EarthSpike : MonoBehaviour
{
    int damage = 5;
    public Rigidbody body;

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
        if(GetComponent<Rigidbody>() == null && GetComponent<Collider>() == null)
        {
            Debug.LogError("EarthSpike has no Collider!");
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
