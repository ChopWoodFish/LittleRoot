using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RootGrow : MonoBehaviour
{
    public GameObject rootPrefab;
    
    public Rigidbody2D rb;

    public bool isRooting;  // 是否正在扎根
    public float rootLen;   // 根的当前长度
    public float rootLenMax;    // 根的最大长度
    // public float verticalSpeed; // 垂直速度
    public float horizontalSpeed;   // 水平速度

    public int rootNum;
    public int rootNumMax = 3;
    public List<float> oneRootLen = new List<float>(capacity: 3);
    public List<Vector3> startPoint = new List<Vector3>(3);
    public List<Vector3> endPoint = new List<Vector3>(capacity: 3);
    public List<Root> roots = new List<Root>(capacity: 3);

    private void Update()
    {
        TryGrowRoot();
    }

    private void TryGrowRoot()
    {
        var verticalMove = Input.GetAxisRaw("Vertical");
        var HorizontalMove = Input.GetAxisRaw("Horizontal");

        if (HorizontalMove != 0)
        {
            roots.Last().Swing(HorizontalMove);
            UpdatePlayerPos();
        }
        

        if (verticalMove == 0)
        {
            // 不动
            rb.velocity = Vector2.zero;
            return;
        }

        if (verticalMove > 0)
        {
            // 尝试进入扎根状态
            if (!isRooting)
            {
                TryEnterRoot();
            }
            
            
            // 不可超出最大长度
            // if (rootLen >= rootLenMax)
            // {
            //     Debug.Log("Hit max root len");
            //     return;
            // }
            
            // 增加长度，计算根的新形态
            // todo..

            // rb.velocity = new Vector2(0f, horizontalSpeed);
            // rootLen += 1;
            // Debug.Log($"add root len: {rootLen}");

        }
        else if (verticalMove < 0 && isRooting)
        {
            // 减少长度，计算根的新形态
        //     
        //     rb.velocity = new Vector2(0f, -horizontalSpeed);
        //     rootLen -= 1;
        //     Debug.Log($"sub root len: {rootLen}");
        //     
        //     
        //     // 根长度至0时退出扎根状态
        //     if (rootLen <= 0)
        //     {
        //         Debug.Log("Exit root state");
        //         isRooting = false;
        //
        //     }
        }
    }

    private void TryEnterRoot()
    {
        Debug.Log("Try Enter root state");
        
        // todo...
        // 判断角色自身状态是否可以扎根
        // 判断脚下平台是否可以扎根
        // 判断是否达到最大根数
        if (rootNum >= rootNumMax)
        {
            Debug.Log("Hit max root num!");
            return;
        }
        
        // todo 生成根并初始化
        /*
         * 生成根
         * 根据角色位置确定和地面的接触位置
         * 将角色位置置为根的尾部
         */

        var newRoot = Instantiate(rootPrefab).GetComponent<Root>();
        if (newRoot == null)
        {
            Debug.LogError("null root component!");
        }
        
        newRoot.rootController = this;
        newRoot.index = rootNum;
        // todo 动态长度
        newRoot.len = 1f;
        rootNum++;

        // 第一节根，赋角色位置给根的起始位置
        if (rootNum == 1)
        {
            startPoint.Add(Vector3.zero);
            startPoint[0] = rb.transform.position;
            newRoot.InitRootWithStart(startPoint[0]);
            roots.Add(newRoot);
        }
        
        // 重新设置角色位置
        UpdatePlayerPos();
        



        isRooting = true;
        
        // 取消重力影响
        rb.gravityScale = 0;
    }

    private void UpdatePlayerPos()
    {
        Vector3 targetPos = roots.Last().endPoint;
        Debug.Log($"update player pos: {targetPos}");
        transform.position = targetPos;
    }
}
