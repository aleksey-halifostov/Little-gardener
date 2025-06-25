using UnityEngine;

namespace LittleGardener.SpriteSorting
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DefaultYSorter : MonoBehaviour, ISortable
    {
        public void Sort()
        {
            GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 10);
        }
    }
}