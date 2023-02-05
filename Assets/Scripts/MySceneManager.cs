using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
    {
        


        public void GoToScene(int index)
        {
            SceneManager.LoadScene(index);
        }
        
    }