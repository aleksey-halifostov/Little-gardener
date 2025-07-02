using UnityEngine;
using LittleGardener.ItemsData;

namespace LittleGardener.ItemsBehaviour
{
    public class PlantItem : GameItem
    {
        private int _growStage = 0;
        private PlantData _data;

        public int GrowTime {  get => _data.GrowTime; }
        public bool IsGrown { get; private set; }

        public PlantItem(PlantData data) : base(data) 
        { 
            _data = data;
        }

        public Sprite GetFirstProgressSprite()
        {
            return _data.ProgressSprites[0];
        }

        public Sprite Grow()
        {
            if (!IsGrown)
            {
                _growStage++;

                if (_growStage == _data.ProgressSprites.Length - 1)
                {
                    IsGrown = true;
                }
            }

            return _data.ProgressSprites[_growStage];
        }
    }
}