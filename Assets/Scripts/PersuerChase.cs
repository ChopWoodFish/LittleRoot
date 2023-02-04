using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersuerChase : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    private Vector2 movDir = new Vector2(); //物理引擎方式采用的参数
    public Transform target; //要追捕的对象
    private Rigidbody2D rb2D;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Chase();
    }

    private void Chase(){
        movDir.x = Input.GetAxisRaw("Horizontal");
        movDir.y = Input.GetAxisRaw("Vertical");
 
        //防止角色斜向运动过快，我们让斜方向距离也为1，原来是根号2
        //归一化处理
        movDir.Normalize();

        //角色向某个位置移动（不是受控制的移动）可以实现跟随或小怪追主角
        transform.position = Vector2.MoveTowards(transform.position,target.position,Time.deltaTime);

        if (transform.position.x > target.position.x){
            transform.GetComponent<SpriteRenderer>().flipX = true;
        }
        else{
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
