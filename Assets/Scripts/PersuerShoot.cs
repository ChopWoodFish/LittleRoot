using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersuerShoot : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FPoint;

    public float bulletRate;//发射子弹的速度
    public float lastFireTime;//上一次发射子弹的时间

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (lastFireTime + bulletRate > Time.time)
            return;
        lastFireTime = Time.time;

        Instantiate(Bullet, FPoint.position, FPoint.rotation);
    }
}
