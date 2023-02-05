using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class RootGrow : MonoBehaviour
{
    public List<GameObject> rootPrefabs;
    
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

    public bool isAnim;

    public void Reset()
    {
        rootNum = 0;
        startPoint.Clear();
        endPoint.Clear();
        foreach (var rootItem in roots)
        {
            Destroy(rootItem.gameObject);
        }
        roots.Clear();
    }

    public void ReRoot(Vector3 newStartPos)
    {
        var newRoot = Instantiate(rootPrefabs[0]).GetComponent<Root>();

        newRoot.rootController = this;
        newRoot.index = 0;
        // todo 动态长度
        newRoot.len = 2f;
        rootNum++;

        startPoint.Add(newStartPos);

        // // 角色中心到脚底的偏移量
        // float offsetY = -0.8f;
        // startPoint[0] = rb.transform.position + new Vector3(0f, offsetY, 0f);

        newRoot.InitRootWithStart(startPoint[0]);
        roots.Add(newRoot);

        // 重新设置角色位置
        UpdatePlayerPos();

        isRooting = true;

        // 取消重力影响
        rb.gravityScale = 0;
    }
    
    public void StartRoot()
    {
        // todo 生成根并初始化
        /*
         * 生成根
         * 根据角色位置确定和地面的接触位置
         * 将角色位置置为根的尾部
         */

        // Debug.Log($"rootPrefabs count: {rootPrefabs.Count}");
        var newRoot = Instantiate(rootPrefabs[0]).GetComponent<Root>();
        if (newRoot == null)
        {
            Debug.LogError("null root component!");
        }

        newRoot.rootController = this;
        newRoot.index = 0;
        // todo 动态长度
        newRoot.len = 2f;
        rootNum++;

        startPoint.Add(Vector3.zero);

        // 角色中心到脚底的偏移量
        float offsetY = -1f;
        startPoint[0] = rb.transform.position + new Vector3(0f, offsetY, 0f);

        newRoot.InitRootWithStart(startPoint[0]);
        roots.Add(newRoot);

        // 重新设置角色位置
        // UpdatePlayerPos();
        
        // todo 试试生长动画
        // Transform rootTrans = newRoot.rootSprite.transform;
        // rootTrans.localScale = new Vector3(1, 0, 1);
        newRoot.GrowAnim(transform);
        isAnim = true;
        // 借助dotween延时一下
        float animDur = 0f;
        DOTween.To(() => animDur, value => animDur = value, 0.2f, 0.2f).OnComplete(() =>
        {
            isAnim = false;
        });
        

        isRooting = true;

        // 取消重力影响
        rb.gravityScale = 0;
    }

    public void Swing(float dir)
    {
        roots.First().Swing(dir);
        UpdatePlayerPos();
    }

    public void GrowAnim()
    {
        /*
         * 第一根root逐渐fill
         * 第二根root位置先重置到第一根的start，再移动到end
         * player的位置跟随最后一根root的end
         */
        isAnim = true;
        
        Root firstRoot = roots[0];
        Root secondRoot = roots[1];
        Root lastRoot = roots.Last();
        
        firstRoot.GrowAnim(secondRoot.transform);
        transform.DOMove(lastRoot.endPoint, 0.2f);
        
        // 借助dotween延时一下
        float animDur = 0f;
        DOTween.To(() => animDur, value => animDur = value, 0.2f, 0.2f).OnComplete(() =>
        {
            isAnim = false;
        });
    }

    public void TryGrowRoot()
    {
        if(rootNum >= rootNumMax) return;
        
        var newRoot = Instantiate(rootPrefabs[rootNum]).GetComponent<Root>();

        newRoot.rootController = this;
        newRoot.index = rootNum;
        // todo 动态长度
        newRoot.len = 2f;
        rootNum++;

        startPoint.Insert(0, roots.First().startPoint);
        roots[0].transform.SetParent(newRoot.transform);
        newRoot.InitRootWithStart(startPoint[0]);
        var nextRoot = roots[0];
        roots.Insert(0, newRoot);

        // 插入新根以后，重新计算所有根的点
        for (int i = 1; i < roots.Count; i++)
        {
            Root crtRoot = roots[i];
            Root preRoot = roots[i - 1];
            crtRoot.InitRootWithStart(preRoot.endPoint);
        }

        // 重新设置角色位置
        // UpdatePlayerPos();
        
        // newRoot.GrowAnim(nextRoot.transform);
        GrowAnim();
    }
    
    private void UpdatePlayerPos()
    {
        // Vector3 targetPos = roots.Last().endPoint;
        Vector3 targetPos = roots.Last().endTrans.position;
        Debug.Log($"update player pos: {targetPos}");
        transform.position = targetPos;
    }
}
