using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameAssets.Scripts.Utils
{
    public class SceneLoader : MonoBehaviour
    {
        public static void ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
