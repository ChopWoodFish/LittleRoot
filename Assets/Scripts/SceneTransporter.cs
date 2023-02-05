using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SceneTransporter : MonoBehaviour
    {
        public int nextScene;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(nextScene);
            }
        }

        // ontrigger
        // {
        //     if (other.collider.CompareTag("Player"))
        //     {
        //         SceneManager.LoadScene(nextScene);
        //     }
        // }
    }
}