using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FPoint;
    public SpriteRenderer facingDecider;

    public float bulletRate;//发射子弹的速度
    public float lastFireTime;//上一次发射子弹的时间
    
    public List<float> listShootAngle = new List<float>(); // 发射方向
    public List<String> canDamageTag = new List<String>();  // 子弹可以伤害的tag

    public bool isAutoShoot;
    public bool decideShootDirByFacing;
    public KeyCode shootKey;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isAutoShoot)
        {
            if (lastFireTime + bulletRate > Time.time)
                return;
            lastFireTime = Time.time;
            GenBullets();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GenBullets();
            }
        }
    }

    private void GenBullets()
    {
        // 根据朝向动态生成一颗水平子弹，angle不起效
        if (decideShootDirByFacing)
        {
            var newBullet = Instantiate(Bullet, FPoint.position, FPoint.rotation).GetComponent<Bullet>();
            newBullet.canDamageTag = canDamageTag;
            if(facingDecider.flipX == false)
                newBullet.angle = 0;
            else
                newBullet.angle = 180;
            return;
        }
        
        // 根据angle list生成子弹
        foreach (var angle in listShootAngle)
        {
            var newBullet = Instantiate(Bullet, FPoint.position, FPoint.rotation).GetComponent<Bullet>();
            newBullet.angle = angle;
            newBullet.canDamageTag = canDamageTag;
        }
    }
}
