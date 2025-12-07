using System;
using Fsi.Ui.Headers;
using UnityEditor;
using UnityEngine;

namespace Fsi.Currencies.Settings
{
    public class CurrencySettings<TID, TData> : ScriptableObject
		where TID : Enum
		where TData : CurrencyData<TID>
	{
		private const string Name = "Currency";
		private const string Path = "Settings/" + Name + "Settings";
		private const string FullPath = "Assets/Resources/" + Path + ".asset";

		private static CurrencySettings<TID, TData> settings;
		private static CurrencySettings<TID, TData> Settings => settings ??= GetOrCreateSettings();
		
		// Libraries
		[FsiHeader("Libraries")]

		[SerializeField]
		private CurrencyLibrary<TID, TData> library = new();
		public static CurrencyLibrary<TID, TData> Library => Settings.library;

		#region Settings

		private static CurrencySettings<TID, TData> GetOrCreateSettings()
		{
			CurrencySettings<TID, TData> s = Resources.Load<CurrencySettings<TID, TData>>(Path);

			#if UNITY_EDITOR
			if (!s)
			{
				if (!AssetDatabase.IsValidFolder("Assets/Resources"))
				{
					AssetDatabase.CreateFolder("Assets", "Resources");
				}

				if (!AssetDatabase.IsValidFolder("Assets/Resources/Settings"))
				{
					AssetDatabase.CreateFolder("Assets/Resources", "Settings");
				}

				s = CreateInstance<CurrencySettings<TID, TData>>();
				AssetDatabase.CreateAsset(s, FullPath);
				AssetDatabase.SaveAssets();
			}
			#endif

			return s;
		}

		#if UNITY_EDITOR
		public static SerializedObject GetSerializedSettings()
		{
			return new SerializedObject(GetOrCreateSettings());
		}
		#endif

		#endregion
	}
}