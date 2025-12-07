using System;
using Fsi.Settings;
using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.Currencies.Settings
{
    public static class CurrencySettingsProvider<TID, TData> 
        where TID : Enum
        where TData : CurrencyData<TID>
    {
        // [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return SettingsEditorUtility.CreateSettingsProvider("Currency", 
                                                                "Falling Snow Interactive/Currency",
                                                                OnActivate);
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            SerializedObject settingsProp = CurrencySettings<TID, TData>.GetSerializedSettings();
            root.Add(SettingsEditorUtility.CreateSettingsPage(settingsProp, "Currency"));
        }
    }
}