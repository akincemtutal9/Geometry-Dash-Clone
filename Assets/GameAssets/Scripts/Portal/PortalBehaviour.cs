using GameAssets.Scripts.Player;
using GameAssets.Scripts.Utils;
using UnityEngine;

namespace GameAssets.Scripts.Portal
{
    public class PortalBehaviour : MonoBehaviour
    {
        [SerializeField] private GameModes gameMode;
        [SerializeField] private MoveSpeed speed;
        [SerializeField] private Gravity gravity;
        [SerializeField] private int state;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                playerController.ChangeThroughPortal(speed,gameMode,gravity,state);
            }
        }
    }
}
