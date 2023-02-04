using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersuerShoot : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FPoint;

    public float bulletRate = 0;//发射子弹的速度

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        bulletRate -= Time.deltaTime;
  
        if (bulletRate <= 0)
        {
            bulletRate = 0.1f; //每隔0.1秒发射一次子弹
            //if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            //{
                //Instantiate函数动态的创建子弹游戏体 发射子弹
             //   Instantiate(m_rocket, m_transform.position, m_transform.rotation);
            //}
        }

        Instantiate(Bullet, FPoint.position, FPoint.rotation);
    }
}
