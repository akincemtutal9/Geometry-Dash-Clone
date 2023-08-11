using GameAssets.Scripts.Utils;
using UnityEngine;

namespace GameAssets.Scripts.Obstacles
{
    public class KillPlayer : MonoBehaviour
    {
        private const string Player = nameof(Player);
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Player))
            {
                SceneLoader.ReloadScene();
            }
        }
    }
}
