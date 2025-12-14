using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fsi.Currencies
{
	[Serializable]
	public abstract class Wallet<TID, TData, TCurrency>
		where TID : Enum
		where TData : CurrencyData<TID>
		where TCurrency : CurrencyInstance<TID, TData>, new()
	{
		[SerializeField]
		private List<TCurrency> currencies;

		protected Wallet()
		{
			currencies = new List<TCurrency>();
		}

		protected Wallet(TCurrency defaultCurrency)
		{
			currencies = new List<TCurrency>();
			Add(defaultCurrency);
		}

		protected Wallet(TData type, int amount)
		{
			currencies = new List<TCurrency>();
			Add(type, amount);
		}

		protected Wallet(List<TCurrency> currencies)
		{
			this.currencies = new List<TCurrency>();
			foreach (TCurrency currency in currencies) Add(currency);
		}

		protected Wallet(Wallet<TID, TData, TCurrency> wallet)
		{
			currencies = new List<TCurrency>();
			foreach (TCurrency currency in wallet.Currencies) Add(currency);
		}

		public List<TCurrency> Currencies => currencies;
		public event Action Changed;

		public bool TryGetCurrency(TID id, out TCurrency currency)
		{
			foreach (TCurrency c in currencies)
			{
				if (c.Currency.ID.Equals(id))
				{
					currency = c;
					return true;
				}
			}

			currency = null;
			return false;
		}
		
		public bool TryGetCurrency(TData type, out TCurrency currency)
		{
			foreach (TCurrency c in currencies)
				if (c.Currency.Equals(type))
				{
					currency = c;
					return true;
				}

			currency = null;
			return false;
		}

		public void Add(TData data, int amount)
		{
			if (TryGetCurrency(data, out TCurrency c))
			{
				c.Add(amount);
			}
			else
			{
				TCurrency currency = new()
				                     {
					                     Currency = data,
					                     Amount = amount,
				                     };
				currencies.Add(currency);
			}

			Changed?.Invoke();
		}

		public void Add(TCurrency currency)
		{
			Add(currency.Currency, currency.Amount);
			Changed?.Invoke();
		}

		public void Add(Wallet<TID, TData, TCurrency> wallet)
		{
			foreach (TCurrency currency in wallet.Currencies) Add(currency);
		}

		public bool CanAfford(TCurrency currency)
		{
			if (TryGetCurrency(currency.Currency, out TCurrency c)) return c.Amount >= currency.Amount;

			return false;
		}

		public bool Remove(TCurrency currency)
		{
			if (TryGetCurrency(currency.Currency, out TCurrency c))
			{
				if (c.Amount < currency.Amount) return false;

				c.Remove(currency.Amount);
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
				if (currency == currencies[^1]) s += "\n";
			}

			return s;
		}
	}
}