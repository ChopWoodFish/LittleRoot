using System;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    private Text textHp;
    
    private void Awake()
    {
        textHp = transform.Find("TextHp").GetComponent<Text>();
    }

    public void UpdateUI(Fighter fighter)
    {
        textHp.text = fighter.hp.ToString();
    }
}