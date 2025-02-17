using UnityEngine;
using UnityEngine.SceneManagement;

public class LVLSelect : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //Hvillken scene spilleren er i nå
        int numberOfScenes = SceneManager.sceneCountInBuildSettings - 1; //Antall scener det er totalt
        //string LVLString = $"LVL{}";


        if (collision.gameObject.CompareTag("LVL0"))
        {
            int nextScene = currentSceneIndex + 1;
            SceneManager.LoadScene(nextScene);
            Debug.Log(currentSceneIndex);
        }
        else if (collision.gameObject.CompareTag("LVL1"))
        {
            int nextScene = currentSceneIndex + 2;
            SceneManager.LoadScene(nextScene);
        }
        else if (collision.gameObject.CompareTag("LVL2"))
        {
            int nextScene = currentSceneIndex + 3;
            SceneManager.LoadScene(nextScene);
        } else if (collision.gameObject.CompareTag("Finish"))
        {
            int nextScene = currentSceneIndex;
            SceneManager.LoadScene(0);
        }
    }
}

    

