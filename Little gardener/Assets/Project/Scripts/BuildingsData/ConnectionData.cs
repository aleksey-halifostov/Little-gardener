using UnityEngine;
using System.Collections.Generic;

namespace LittleGardener.BuildingsData
{
    [CreateAssetMenu(menuName="Building/new Connection data")]
    public class ConnectionData : ScriptableObject
    {
        [SerializeField] private Sprite _n;
        [SerializeField] private Sprite _r;
        [SerializeField] private Sprite _l;
        [SerializeField] private Sprite _lr;
        [SerializeField] private Sprite _b;
        [SerializeField] private Sprite _br;
        [SerializeField] private Sprite _bl;
        [SerializeField] private Sprite _blr;
        [SerializeField] private Sprite _t;
        [SerializeField] private Sprite _tr;
        [SerializeField] private Sprite _tl;
        [SerializeField] private Sprite _tlr;
        [SerializeField] private Sprite _tb;
        [SerializeField] private Sprite _tbr;
        [SerializeField] private Sprite _tbl;
        [SerializeField] private Sprite _a;

        public List<Sprite> Sprites => new() { _n, _r, _l, _lr, _b, _br, _bl, _blr, _t, _tr, _tl, _tlr, _tb, _tbr, _tbl, _a };
    }
}
