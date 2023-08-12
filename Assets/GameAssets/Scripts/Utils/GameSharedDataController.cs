using UnityEngine;

namespace GameAssets.Scripts.Utils
{
    public class GameSharedDataController : MonoBehaviour
    {
        public static GameSharedDataController Instance { get; private set; }
        
        [Header("Audio")]
        public float sharedVolume;
 
        // [Header("SomethingElse")] if needed
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
