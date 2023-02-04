using System;
using Unity.Mathematics;
using UnityEngine;

public class Root : MonoBehaviour
{
    public int index;
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float len;
    public float angle = 90f;
    public float leftAngleBound = 0f;
    public float rightAngleBound = 180f;

    public RootGrow rootController;

    /// <summary>
    /// 旋转根
    /// </summary>
    /// <param name="dir">左右方向，-1为左，1为右</param>
    public void Swing(float dir)
    {
        // 角度增大是向左转
        float newAngle = angle + (-1) * dir;
        ;
        if (CanSwingToAngle(newAngle))
        {
            angle = newAngle;
            UpdateRoot();
        }
        else
        {
            Debug.Log("Hit angle bound!");
            return;
        }

    }

    public void InitRootWithStart(Vector3 pos)
    {
        startPoint = pos;
        transform.position = startPoint;
        UpdateRoot();
    }

    public void UpdateRoot()
    {
        double radian = (angle * Math.PI) / 180f;

        endPoint.x = (float) (startPoint.x + len * Math.Cos(radian));
        endPoint.y = (float) (startPoint.y + len * Math.Sin(radian));
        
        // 旋转图片
        float rotateZ = transform.rotation.z;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        PrintInfo();
    }

    /// <summary>
    /// 检查角度是否处于合法区间
    /// </summary>
    /// <returns></returns>
    public bool CanSwingToAngle(float an)
    {
        Debug.Log($"angle check: left {leftAngleBound} want {an} right {rightAngleBound}");
        return an > leftAngleBound && an < rightAngleBound;
    }

    private void PrintInfo()
    {
        Debug.Log($"[root {index}] angle:{angle} startPoint: {startPoint} endPoint: {endPoint}");
    }
}