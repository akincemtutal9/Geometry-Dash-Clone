using UnityEngine;

namespace GameAssets.Scripts.Obstacles
{
    public class WinBox : MonoBehaviour
    {
        private const string Player = nameof(Player);
        [SerializeField] private GameObject winMenu;
        [SerializeField] private GameObject pauseButton;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Player))
            {
                winMenu.SetActive(true);
                pauseButton.SetActive(false);
            }
        }
    }
}
