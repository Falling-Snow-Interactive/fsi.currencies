using System;
using UnityEngine;

namespace Fsi.Currencies
{
	[Serializable]
	public class CurrencyInstance<TID, TCurrency> : ISerializationCallbackReceiver
		where TCurrency : CurrencyData<TID>
	{
		public event Action Changed;
		
		[HideInInspector]
		[SerializeField]
		private string name;

		[CurrencyLibrary]
		[SerializeField]
		private TCurrency currency;
		public TCurrency Currency
		{
			get => currency;
			set => currency = value;
		}
		
		[Min(1)]
		[SerializeField]
		private int amount;
		public int Amount
		{
			get => amount;
			set => amount = value;
		}

		public CurrencyInstance()
		{
			currency = default;
			amount = 1;
		}

		public CurrencyInstance(TCurrency currency, int amount)
		{
			this.currency = currency;
			this.amount = amount;
		}
		
		#region Arithmetic
		
		#region Combine
		
		public bool TryCombine(CurrencyInstance<TID, TCurrency> other, out CurrencyInstance<TID, TCurrency> combined)
		{
			if (currency.Equals(other.currency))
			{
				combined = new CurrencyInstance<TID, TCurrency>(currency, other.amount);
				return true;
			}

			combined = null;
			return false;
		}
		
		#endregion
		
		#region Add

		public void Add(int amount)
		{
			this.amount += amount;
			Changed?.Invoke();
		}

		public void Add(CurrencyInstance<TID, TCurrency> currencyInstance)
		{
			if (currency.Equals(currencyInstance.currency))
			{
				amount += currencyInstance.amount;
				Changed?.Invoke();
			}
			else
			{
				Debug.LogError($"Cannot combine currency - {currency} with currency - {currencyInstance.currency}.");
			}
		}
		
		#endregion
		
		#region Remove

		public void Remove(int amount)
		{
			this.amount -= amount;
			this.amount = Mathf.Clamp(this.amount, 0, int.MaxValue);
			Changed?.Invoke();
		}
		
		#endregion
		
		#endregion
		
		#region Serialization Callbacks

		public void OnBeforeSerialize()
		{
			name = ToString();
		}

		public void OnAfterDeserialize()
		{
		}
		
		#endregion
		
		#region Object Overrides

		public override string ToString()
		{
			return $"{currency} - {amount}";
		}
		
		#endregion
	}
}