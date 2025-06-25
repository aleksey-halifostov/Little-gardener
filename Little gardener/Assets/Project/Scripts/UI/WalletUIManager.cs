using UnityEngine;
using System.Collections;
using TMPro;

namespace LittleGardener.UI
{
    public class WalletUIManager : MonoBehaviour
    {
        private Coroutine _littleMoneyCoroutine;

        [SerializeField] private GameObject _littleMoneyText;
        [SerializeField] private TextMeshProUGUI _text;

        private IEnumerator LittleMoney()
        {
            _littleMoneyText.SetActive(true);
            yield return new WaitForSeconds(3);
            _littleMoneyText.SetActive(false);
            _littleMoneyCoroutine = null;
        }

        public void ShowLittleMoneyMessage()
        {
            if (_littleMoneyCoroutine != null)
            {
                StopCoroutine(_littleMoneyCoroutine);
            }

            _littleMoneyCoroutine = StartCoroutine(LittleMoney());
        }

        public void UpdateWalletText(int money)
        {
            _text.text = money.ToString();
        }
    }
}
