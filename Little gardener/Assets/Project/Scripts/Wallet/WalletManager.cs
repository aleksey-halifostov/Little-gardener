using UnityEngine;
using LittleGardener.UI;

namespace LittleGardener.Wallet
{
    public class WalletManager : MonoBehaviour
    {
        private WalletUIManager _uiManager;
        private int _moneyAmount;

        private void Awake()
        {
            _uiManager = FindFirstObjectByType<WalletUIManager>();
            AddMoney(200);
        }
        
        public bool IsMoneyEnough(int needMoney)
        {
            return _moneyAmount >= needMoney;
        }

        public void ShowMessage()
        {
            _uiManager.ShowLittleMoneyMessage();
        }

        public void AddMoney(int money)
        {
            _moneyAmount += money;
            _uiManager.UpdateWalletText(_moneyAmount);
        }

        public void SpendMoney(int needMoney)
        {
            if (needMoney > _moneyAmount)
                throw new System.ArgumentOutOfRangeException(nameof(needMoney));

            _moneyAmount -= needMoney;
            _uiManager.UpdateWalletText(_moneyAmount);
        }
    }
}
