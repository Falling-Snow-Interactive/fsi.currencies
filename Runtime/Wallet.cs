using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fsi.Currencies
{
    [Serializable]
    public abstract class Wallet<TEnum, TCurrency> 
        where TEnum : Enum
        where TCurrency : Currency<TEnum>, new()
    {
        public event Action Changed;
        
        [SerializeField]
        private List<TCurrency> currencies;
        public List<TCurrency> Currencies => currencies;

        protected Wallet()
        {
            currencies = new List<TCurrency>();
        }

        public Wallet(TCurrency defaultCurrency)
        {
            currencies = new List<TCurrency>();
            Add(defaultCurrency);
        }

        public Wallet(TEnum type, int amount)
        {
            currencies = new List<TCurrency>();
            Add(type, amount);
        }

        public Wallet(List<TCurrency> currencies)
        {
            this.currencies = new List<TCurrency>();
            foreach (TCurrency currency in currencies)
            {
                Add(currency);
            }
        }

        public Wallet(Wallet<TEnum, TCurrency> wallet)
        {
            currencies = new List<TCurrency>();
            foreach (TCurrency currency in wallet.Currencies)
            {
                Add(currency);
            }
        }

        public bool TryGetCurrency(TEnum type, out TCurrency currency)
        {
            foreach (TCurrency c in currencies)
            {
                if (c.type.Equals(type))
                {
                    currency = c;
                    return true;
                }
            }
            
            currency = null;
            return false;
        }
        
        public void Add(TEnum type, int amount)
        {
            if (TryGetCurrency(type, out TCurrency c))
            {
                c.Add(amount);
            }
            else
            {
                TCurrency currency = new()
                                     {
                                         amount = amount,
                                         type = type,
                                     };
                currencies.Add(currency);
            }
            
            Changed?.Invoke();
        }

        public void Add(TCurrency currency)
        {
            Add(currency.type, currency.amount);
            Changed?.Invoke();
        }

        public void Add(Wallet<TEnum, TCurrency> wallet)
        {
            foreach (TCurrency currency in wallet.Currencies)
            {
                Add(currency);
            }
        }

        public bool CanAfford(TCurrency currency)
        {
            if (TryGetCurrency(currency.type, out TCurrency c))
            {
                return c.amount >= currency.amount;
            }

            return false;
        }

        public bool Remove(TCurrency currency)
        {
            if (TryGetCurrency(currency.type, out TCurrency c))
            {
                if (c.amount < currency.amount)
                {
                    return false;
                }
                
                c.Remove(currency.amount);
                Changed?.Invoke();
                
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            string s = "";
            foreach (TCurrency currency in currencies)
            {
                s += currency.ToString();
                if (currency == currencies[^1])
                {
                    s += "\n";
                }
            }
            return s;
        }
    }
}