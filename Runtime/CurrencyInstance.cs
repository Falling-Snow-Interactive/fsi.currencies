using System;
using UnityEngine;

namespace Fsi.Currencies
{
	[Serializable]
	public class CurrencyInstance<TID, TData> : ISerializationCallbackReceiver
		where TData : CurrencyData<TID>
	{
		public event Action Changed;
		
		[HideInInspector]
		[SerializeField]
		private string name;

		[CurrencyLibrary]
		[SerializeField]
		private TData data;
		public TData Data
		{
			get => data;
			set => data = value;
		}
		
		[SerializeField]
		private int amount;
		public int Amount
		{
			get => amount;
			set => amount = value;
		}

		public CurrencyInstance()
		{
			data = default;
			amount = 0;
		}

		public CurrencyInstance(TData data, int amount)
		{
			this.data = data;
			this.amount = amount;
		}
		
		public bool TryCombine(CurrencyInstance<TID, TData> other, out CurrencyInstance<TID, TData> combined)
		{
			if (data.Equals(other.data))
			{
				combined = new CurrencyInstance<TID, TData>(data, other.amount);
				return true;
			}

			combined = null;
			return false;
		}
		
		#region Add

		public void Add(int amount)
		{
			this.amount += amount;
			Changed?.Invoke();
		}

		public void Add(CurrencyInstance<TID, TData> currencyInstance)
		{
			if (data.Equals(currencyInstance.data))
			{
				amount += currencyInstance.amount;
				Changed?.Invoke();
			}
			else
			{
				Debug.LogError($"Cannot combine currency - {data} with currency - {currencyInstance.data}.");
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
			return $"{data} - {amount}";
		}
		
		#endregion
	}
}