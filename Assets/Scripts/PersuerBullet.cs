using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersuerBullet : MonoBehaviour
{
    GameObject persuer;
    float timer = 2f;
    public int Speed, facing;
    // Start is called before the first frame update
    void Start()
    {
        persuer = GameObject.Find("Persuer");
        if(persuer.GetComponent<SpriteRenderer>().flipX == false)
            facing = 1;
        else
            facing = -1;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<0)
        {
            Destroy(gameObject);
        }
        transform.position += new Vector3(facing*0.05f * Speed, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {

        Debug.Log(coll.gameObject.tag);
        if (coll.gameObject.tag.Equals("Player"))
        {
            //Destroy(coll.gameObject);//给角色造成伤害
            Destroy(gameObject);//销毁子弹自己
        }
    }
}