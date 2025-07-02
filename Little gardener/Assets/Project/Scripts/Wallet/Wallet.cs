using UnityEngine;

namespace LittleGardener.GameWallet
{
    [RequireComponent(typeof(WalletView))]
    public class Wallet : MonoBehaviour
    {
        private int _moneyAmount;
        private WalletView _view;

        private void Awake()
        {
            _view = GetComponent<WalletView>();

            AddMoney(2000);
        }
        
        public bool IsMoneyEnough(int needMoney)
        {
            return _moneyAmount >= needMoney;
        }

        public void ShowMessage()
        {
            _view.ShowLittleMoneyMessage();
        }

        public void AddMoney(int money)
        {
            if (money < 0)
                throw new System.ArgumentOutOfRangeException(nameof(money));

            _moneyAmount += money;
            _view.UpdateWalletText(_moneyAmount);
        }

        public void SpendMoney(int needMoney)
        {
            if (needMoney > _moneyAmount || needMoney < 0)
                throw new System.ArgumentOutOfRangeException(nameof(needMoney));

            _moneyAmount -= needMoney;
            _view.UpdateWalletText(_moneyAmount);
        }
    }
}
