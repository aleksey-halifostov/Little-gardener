using System;
using LittleGardener.ItemsBehaviour;
using LittleGardener.ItemsData;

namespace LittleGardener.Store
{
    public static class PriceCalculator
    {
        private static float _oldItemCoeficient = .9f;

        public static int GetBuyPrice(GameItem item)
        {
            return item.Data.DefaultPrice;
        }

        public static int GetSellPrice(GameItem item)
        {
            if (!(item is IUsable)) 
                return item.Data.DefaultPrice;

            if (item is ItemWithDurability durable && item.Data is DurabilityItemData data)
                return (int)MathF.Max(1, (durable.Data.DefaultPrice * _oldItemCoeficient * durable.CurrentDurability / data.MaxDurability));
            
            return (int)(item.Data.DefaultPrice * _oldItemCoeficient);
        }
    }
}