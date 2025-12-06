using System;
using Fsi.DataSystem.Libraries;

namespace Fsi.Currencies
{
    [Serializable]
    public class CurrencyLibrary<TID, TData> : Library<TID, TData>
        where TID : Enum
        where TData : CurrencyData<TID>
    {
        
    }
}