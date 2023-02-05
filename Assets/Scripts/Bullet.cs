using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject persuer;
    float timeBulletLife = 1.0f;//子弹存在时间
    public int Speed;
    public float angle;

    public GameObject shooter;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeBulletLife -= Time.deltaTime;
        if(timeBulletLife<0)
        {
            Destroy(gameObject);
        }
        // transform.position += new Vector3(facing*0.05f * Speed, 0, 0);
        Move();
    }

    public void Move()
    {
        float arg = 0.05f;
        float fullSpeed = arg * Speed;
        float splitSpeed = fullSpeed * Mathf.Sin((float) (45f * Math.PI / 180f));
        switch (angle)
        {
            case 0f:
                transform.position += new Vector3(fullSpeed, 0f, 0f);
                break;
            case 45f:
                transform.position += new Vector3(splitSpeed, splitSpeed,0f);
                break;
            case 90f:
                transform.position += new Vector3(0f, fullSpeed, 0f);
                break;
            case 135f:
                transform.position += new Vector3(-splitSpeed, splitSpeed,0f);
                break;
            case 180f:
                transform.position += new Vector3(-fullSpeed, 0, 0f);
                break;
            case 225f:
                transform.position += new Vector3(-splitSpeed, -splitSpeed, 0f);
                break;
            case 270f:
                transform.position += new Vector3(0f, -fullSpeed, 0f);
                break;
            case 315f:
                transform.position += new Vector3(splitSpeed, -splitSpeed, 0f);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {

        // Debug.Log(coll.gameObject.tag);
        if (coll.gameObject.tag.Equals("Player"))
        {
            //Destroy(coll.gameObject);//给角色造成伤害
            
            coll.GetComponent<Fighter>().GetHit();
            
            Destroy(gameObject);//销毁子弹自己
        }
    }
}
