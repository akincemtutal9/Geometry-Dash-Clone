using GameAssets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace GameAssets.Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Slider volumeSlider;
        public AudioSource audioSource; // AudioSource eklendi
        private GameSharedDataController dataController;

        private void Start()
        {
            dataController = GameSharedDataController.Instance;

            // Başlangıçta Slider'ın değerini DataController'daki sharedVolume değerine ayarla
            volumeSlider.value = dataController.sharedVolume;

            // Başlangıçta AudioSource'un ses seviyesini de ayarla
            audioSource.volume = dataController.sharedVolume;

            // Slider'ın değeri değiştiğinde çağrılacak metodunu atayalım
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }

        private void ChangeVolume(float newVolume)
        {
            // Yeni ses seviyesini DataController'daki sharedVolume'a ve AudioSource'a uygula
            dataController.sharedVolume = newVolume;
            audioSource.volume = newVolume;
        }
    }

    
}
