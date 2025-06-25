using UnityEngine;
using LittleGardener.BuildingsData;
using LittleGardener.Wallet;
using LittleGardener.PlayerControls;
using LittleGardener.GameManagement;

namespace LittleGardener.Building
{
    [RequireComponent(typeof(CellMap))]
    public class BuildingManager : MonoBehaviour
    {
        private CellMap _map;
        private GameObject preview;
        private BuildingData _current;
        private WalletManager _walletManager;
        private AudioManager _audioManager;

        [SerializeField] private BuildStoreFiller _buildStoreManager;
        [SerializeField] private AudioClip _placingSound;

        private void Awake()
        {
            _map = GetComponent<CellMap>();
            _walletManager = FindFirstObjectByType<WalletManager>();
            _audioManager = FindFirstObjectByType<AudioManager>();
        }

        private void OnEnable()
        {
            BuildingStorePosition.OnBuidPositionClicked += InstantiatePreview;
        }

        private void OnDisable()
        {
            BuildingStorePosition.OnBuidPositionClicked -= InstantiatePreview;
        }

        public void InstantiatePreview(BuildingData item)
        {
            if (preview == null)
            {
                preview = Instantiate(item.ObjectPrefab, InputManager.GetCameraCenterWorldPosition(), Quaternion.identity);

                if (preview.GetComponent<BuildingBase>() == null && preview.GetComponent<TouchPlacer>() == null)
                    throw new System.ArgumentException(nameof(item));

                _current = item;
            }
        }

        public void InstantiateObject()
        {
            if (preview == null)
                return;

            if (!_walletManager.IsMoneyEnough(_current.Price))
            {
                _walletManager.ShowMessage();
                return;
            }

            TouchPlacer placer = preview.GetComponent<TouchPlacer>();

            if (!_map.IsCellFree(placer.Cell))
                return;

            _walletManager.SpendMoney(_current.Price);
            _audioManager.PlayEffectSound(_placingSound);
            preview.GetComponent<SpriteRenderer>().color = Color.white;
            placer.Init();
            Destroy(placer);
            _map.OccupyCell(placer.Cell, preview.GetComponent<BuildingBase>());
            preview = null;

            CancelPreview();
        }

        public void CancelPreview()
        {
            if (preview != null)
            {
                Destroy(preview);
                preview = null;
            }

            _current = null;
        }

        public void Show()
        {
            _buildStoreManager.gameObject.SetActive(true);
        }

        public void Hide()
        {
            CancelPreview();
            _buildStoreManager.gameObject.SetActive(false);
        }
    }
}
