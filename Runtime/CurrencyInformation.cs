using System;
using UnityEngine;

namespace Fsi.Currencies
{
    [Serializable]
    public class CurrencyInformation<TEnum>
        where TEnum : Enum
    {
        [SerializeField]
        private TEnum type;
        public TEnum Type => type;
        
        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;
    }
}