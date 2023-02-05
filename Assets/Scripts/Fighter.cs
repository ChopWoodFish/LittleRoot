using System;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hp;
    public HpUI ui;
    public Animator anim;
    public Action getHitAction;

    private void Start()
    {
        ui?.UpdateUI(this);
    }

    public void GetHit(int damage = -1)
    {
        // 默认伤害1
        if (damage == -1)
            damage = 1;
        hp -= damage;
        
        ui?.UpdateUI(this);
        getHitAction?.Invoke();
    }

}