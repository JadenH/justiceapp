using System;
using System.Collections.Generic;
using System.Linq;

public partial class Dragon
{
    public Dictionary<Type, HashSet<IDragonStore>> _stores { get; set; }

    public void InitStores()
    {
        _stores = new Dictionary<Type, HashSet<IDragonStore>>();
    }

    public void RegisterStore(IDragonStore store)
    {
        var type = store.GetType();
        if (!_stores.ContainsKey(type))
        {
            _stores[type] = new HashSet<IDragonStore>();
        }
        _stores[type].Add(store);
    }

    public IEnumerable<T> Stores<T>(Type type)
    {
        return !_stores.ContainsKey(type) ? null : _stores[type].Cast<T>();
    }
}