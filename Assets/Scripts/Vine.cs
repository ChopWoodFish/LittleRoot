using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    // Creates a line renderer that follows a Sin() function
    // and animates it.

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;

    // Start is called before the first frame update
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = lengthOfLineRenderer;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        var t = Time.time;
        lineRenderer.SetPosition(0, gameObject.transform.position);
        var pos = GameObject.Find("Player").transform.position;
        pos.y = gameObject.transform.position.y;

        for (int i = 1; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i,
                pos);
            //new Vector3(i * 0.5f, Mathf.Sin(i + t), 0.0f));
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Debug.Log(coll.gameObject.tag);
        if (coll.gameObject.tag.Equals("Player"))
        {
            //Destroy(coll.gameObject);//给角色造成伤害

            coll.GetComponent<Fighter>().GetHit();

            Destroy(gameObject);//销毁子弹自己
        }
    }
}
