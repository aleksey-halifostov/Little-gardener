using UnityEngine;
using System.Collections;
using TMPro;

namespace LittleGardener.UI
{
    public class InventoryUI : MonoBehaviour
    {
        private Coroutine _spaceErrorCoroutine;
        private Coroutine _itemNameCorouine;
        private bool _isMainInventoryActive;

        [SerializeField] private GameObject _noSpaceText;
        [SerializeField] private GameObject _mainInventory;
        [SerializeField] private GameObject _generalInventory;
        [SerializeField] private TextMeshProUGUI _itemNameText;

        public bool IsMainInventoryActive => _isMainInventoryActive;

        private IEnumerator InventorySpaceError()
        {
            _noSpaceText.SetActive(true);

            yield return new WaitForSeconds(3);

            _noSpaceText.SetActive(false);
            _spaceErrorCoroutine = null;
        }

        private IEnumerator ShowItemNameText(string name)
        {
            _itemNameText.text = name;
            _itemNameText.gameObject.SetActive(true);

            yield return new WaitForSeconds(3);

            _itemNameText.gameObject.SetActive(false);
            _spaceErrorCoroutine = null;
        }

        public void ShowLittleSpaceMessage()
        {
            if (_spaceErrorCoroutine != null)
            {
                StopCoroutine(_spaceErrorCoroutine);
            }

            _spaceErrorCoroutine = StartCoroutine(InventorySpaceError());
        }

        public void ShowItemName(string name)
        {
            if (_itemNameCorouine != null)
            {
                StopCoroutine(_itemNameCorouine);
            }

            _itemNameCorouine = StartCoroutine(ShowItemNameText(name));
        }

        public void ShowMainInventory()
        {
            _mainInventory.SetActive(true);
            _isMainInventoryActive = true;
        }

        public void HideMainInventory()
        {
            _mainInventory.SetActive(false);
            _isMainInventoryActive = false;
        }

        public void ShowGeneralInventory()
        {
            _generalInventory.SetActive(true);
        }

        public void HideGeneralInventory()
        {
            _generalInventory.SetActive(false);
        }
    }
}