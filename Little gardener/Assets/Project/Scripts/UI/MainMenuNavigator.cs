using UnityEngine;

namespace LittleGardener.UI 
{
    public class MainMenuNavigator : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenuContainer;
        [SerializeField] private GameObject _settingsContainer;

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

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
