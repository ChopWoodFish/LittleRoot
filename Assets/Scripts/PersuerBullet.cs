using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersuerBullet : MonoBehaviour
{
    GameObject persuer;
    float timer = 2f;
    public int Speed, facing;
    // Start is called before the first frame update
    void Start()
    {
        persuer = GameObject.Find("Persuer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
