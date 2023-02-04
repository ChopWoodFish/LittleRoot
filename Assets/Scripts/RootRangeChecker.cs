using System;
using UnityEngine;

public class RootRangeChecker : MonoBehaviour
{
    public CircleCollider2D collider;
    public Collider2D otherCollider;
    public Vector3 hitPos;

    public SpriteRenderer pointSprite;

    public bool isInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Platform") || !(other is EdgeCollider2D)) return;

        // Debug.Log("trigger enter");
        otherCollider = other;
        pointSprite.gameObject.SetActive(true);
        hitPos = other.bounds.ClosestPoint(transform.position);
        isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("trigger Exit");
        otherCollider = null;
        pointSprite.gameObject.SetActive(false);
        isInRange = false;
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     Debug.Log("trigger stay");
    //     hitPos = other.bounds.ClosestPoint(transform.position);
    // }

    private void Update()
    {
        if (isInRange)
        {
            hitPos = otherCollider.bounds.ClosestPoint(transform.position);
            pointSprite.transform.position = hitPos;   
        }
    }
}