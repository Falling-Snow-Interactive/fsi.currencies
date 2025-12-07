using System;
using Fsi.DataSystem.Selectors;

namespace Fsi.Currencies
{
    public abstract class CurrencyLibraryAttributeDrawer<TID, TData> : SelectorAttributeDrawer<TID, TData>
        where TID : Enum
        where TData : CurrencyData<TID>
    {
    }
}