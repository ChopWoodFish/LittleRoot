using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class EnterBossZone : MonoBehaviour
{
    public CinemachineVirtualCamera vCamera;
    public int zoomToSize;

    public void Zoom()
    {
        // vCamera.m_Lens.OrthographicSize = zoomToSize;

        DOTween.To(() => vCamera.m_Lens.OrthographicSize, value => vCamera.m_Lens.OrthographicSize = value, zoomToSize,
            1.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Zoom();
            gameObject.SetActive(false);
        }
    }
}