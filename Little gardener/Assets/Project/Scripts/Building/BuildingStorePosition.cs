using UnityEngine;
using TMPro;
using UnityEngine.UI;
using LittleGardener.BuildingsData;
using System;

public class BuildingStorePosition : MonoBehaviour
{
    private BuildingData _buildingData;

    [SerializeField] TextMeshProUGUI _priceText;
    [SerializeField] Image _image;

    public static Action<BuildingData> OnBuidPositionClicked;

    public void Init(BuildingData data)
    {
        _buildingData = data;
        _image.overrideSprite = _buildingData.UISprite;
        _priceText.text = _buildingData.Price.ToString();
    }

    public void ShowPreview()
    {
        OnBuidPositionClicked?.Invoke(_buildingData);
    }
}
