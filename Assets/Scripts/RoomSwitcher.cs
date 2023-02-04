using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RoomSwitcher : MonoBehaviour
{
    public GameObject virtualCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
            virtualCamera.gameObject.SetActive(true);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
            virtualCamera.gameObject.SetActive(false);
    }
}
