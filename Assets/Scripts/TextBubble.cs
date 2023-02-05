using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
    public Transform targetVillager;
    public GameObject TextBubbles;//测试在转成屏幕的位置生成一个对话气泡
    // Start is called before the first frame update
    void Start()
    {
        targetVillager = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 screenPos = Camera.main.WorldToScreenPoint(targetVillager.position);


        var newBubble = Instantiate(TextBubbles);
        newBubble.transform.position = screenPos;
    }
}
