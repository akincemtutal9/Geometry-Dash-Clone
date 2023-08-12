using UnityEngine;

namespace GameAssets.Scripts.Utils
{
    public class QuitGame : MonoBehaviour
    {
        public void OnApplicationQuit()
        {
            Application.Quit();
        }
    }
}
