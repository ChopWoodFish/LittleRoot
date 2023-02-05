using System;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hp;
    public HpUI ui;
    public Animator anim;
    public Action getHitAction;
    public Action deadAction;

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
        Debug.Log($"{gameObject.name} get damage {damage}, crt hp: {hp}");
        
        if (hp <= 0)
        {
            Debug.Log("try death");
            if (deadAction == null)
            {
                DefaultDeath();
            }
            else deadAction.Invoke();
        }
        
        ui?.UpdateUI(this);
        getHitAction?.Invoke();
    }
    
    void DefaultDeath()
    {
        Destroy(gameObject);
    }

}