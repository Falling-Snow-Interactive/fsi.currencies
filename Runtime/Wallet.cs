using System;
using System.Collections.Generic;

namespace Fsi.Currencies
{
    public abstract class Wallet<TEnum, TCurrency> 
        where TEnum : Enum
        where TCurrency : Currency<TEnum>, new()
    {
        public event Action Changed;
        
        private readonly Dictionary<TEnum, TCurrency> currencies;
        public List<TCurrency> Currencies => new (currencies.Values);

        protected Wallet()
        {
            currencies = new Dictionary<TEnum, TCurrency>();
        }

        public Wallet(TCurrency defaultCurrency)
        {
            Add(defaultCurrency);
        }

        public Wallet(List<TCurrency> currencies)
        {
            foreach (TCurrency currency in currencies)
            {
                Add(currency);
            }
        }

        public void Add(TCurrency currency)
        {
            var type = currency.type;
            if (currencies.TryGetValue(type, out var c))
            {
                c.Add(currency.amount);
            }
            else
            {
                currencies.Add(type, currency);
            }
            
            Changed?.Invoke();
        }
        
        public void Add(TEnum type, int amount)
        {
            if (currencies.TryGetValue(type, out var c))
            {
                c.Add(amount);
            }
            else
            {
                TCurrency currency = new TCurrency()
                                     {
                                         amount = amount,
                                         type = type,
                                     };
                currencies.Add(currency.type, currency);
            }
            
            Changed?.Invoke();
        }

        public bool CanAfford(TCurrency currency)
        {
            var type = currency.type;
            if (currencies.TryGetValue(type, out var c))
            {
                return c.amount >= currency.amount;
            }

            return false;
        }

        public bool Remove(TCurrency currency)
        {
            var type = currency.type;
            if (currencies.TryGetValue(type, out var c))
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

        public bool TryGetValue(TEnum type, out TCurrency currency)
        {
            return currencies.TryGetValue(type, out currency);
        }
    }
}