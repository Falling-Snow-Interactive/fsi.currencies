using System;
using Fsi.Settings;
using UnityEditor;

namespace Fsi.Currencies.Settings
{
    public static class CurrencySettingsProvider<TID, TData> 
        where TID : Enum
        where TData : CurrencyData<TID>
    {
        // [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SerializedObject settingsProp = CurrencySettings<TID, TData>.GetSerializedSettings();
            return SettingsEditorUtility.CreateSettingsProvider<CurrencySettings<TID, TData>>("Currency", 
                                                                "Falling Snow Interactive/Currency",
                                                                settingsProp);
        }
    }
}