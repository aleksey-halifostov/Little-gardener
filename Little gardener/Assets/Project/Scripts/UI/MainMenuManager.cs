using LittleGardener.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace LittleGardener.UI 
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenuContainer;
        [SerializeField] private GameObject _settingsContainer;
        [SerializeField] private Slider _slider;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ShowSettings()
        {
            _settingsContainer.SetActive(true);
            _mainMenuContainer.SetActive(false);
        }

        public void HideSettings()
        {
            _settingsContainer.SetActive(false);
            _mainMenuContainer.SetActive(true);
        }

        public void SetGlobalVolume()
        {
            GameSettings.ChangeGlobalVolume(_slider.value);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
