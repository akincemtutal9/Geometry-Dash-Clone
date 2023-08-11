using GameAssets.Scripts.Utils;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GameAssets.Scripts.Obstacles
{
    public class KillPlayer : MonoBehaviour
    {
        private const string Player = nameof(Player);
    
        private void Start()
        {
            HandleTriggerWithPlayer();  
        }
    
        private void HandleTriggerWithPlayer()
        {
            this.OnTriggerEnter2DAsObservable().Where(other => other.gameObject.CompareTag(Player)).Subscribe(_ =>
            {
                SceneLoader.ReloadScene();
            }).AddTo(gameObject);
        }
    }
}
