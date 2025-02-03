using System;
using UnityEngine;

namespace Fsi.Currencies
{
    [Serializable]
    public class Currency<TEnum> : ISerializationCallbackReceiver
        where TEnum : Enum
    {
        public event Action Changed;
        
        [HideInInspector]
        [SerializeField]
        private string name;
        
        public TEnum type;
        public int amount;

        public bool TryCombine(Currency<TEnum> other, out Currency<TEnum> combined)
        {
            if (type.Equals(other.type))
            {
                combined = new Currency<TEnum>
                           {
                               type = type,
                               amount = other.amount,
                           };
                return true;
            }

            combined = null;
            return false;
        }

        public void Add(int amount)
        {
            this.amount += amount;
            Changed?.Invoke();
        }

        public void Add(Currency<TEnum> currency)
        {
            if (type.Equals(currency.type))
            {
                amount += currency.amount;
                Changed?.Invoke();
            }
            else
            {
                Debug.LogError($"Cannot combine currency - {type} with currency - {currency.type}.");
            }
        }

        public void Remove(int amount)
        {
            this.amount -= amount;
            this.amount = Mathf.Clamp(this.amount, 0, int.MaxValue);
            Changed?.Invoke();
        }
        
        public void OnBeforeSerialize()
        {
            name = $"{type} - {amount}";
        }

        public void OnAfterDeserialize() { }

        public override string ToString()
        {
            return name;
        }
    }
}