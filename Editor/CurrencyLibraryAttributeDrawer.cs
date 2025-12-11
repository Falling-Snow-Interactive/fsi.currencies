using System;
using Fsi.DataSystem.Libraries;

namespace Fsi.Currencies
{
    public abstract class CurrencyLibraryAttributeDrawer<TID, TData> : LibraryAttributeDrawer<TID, TData>
        where TID : Enum
        where TData : CurrencyData<TID>
    {
    }
}