using System;
using System.Collections.Generic;

public partial class Dragon
{
    private Dictionary<Type, IDragonStore> _priority = new Dictionary<Type, IDragonStore>();
    public Action<IDragonStore, IDragonStore> Prioritize;

    public T GetPriority<T>(Type type) where T : class
    {
        IDragonStore oldStore;
        _priority.TryGetValue(type, out oldStore);

        if (oldStore != null)
        {
            return oldStore as T;
        }
        return null;
    }

    public void SetPriority(IDragonStore dragonObject)
    {
        IDragonStore oldStore;
        _priority.TryGetValue(dragonObject.GetType(), out oldStore);

        if (oldStore != dragonObject)
        {
            if (Prioritize != null) Prioritize.Invoke(dragonObject, oldStore);
            _priority[dragonObject.GetType()] = dragonObject;
        }
    }

    public void ClearPriority(Type type)
    {
        IDragonStore oldStore;
        _priority.TryGetValue(type, out oldStore);
        if (Prioritize != null) Prioritize.Invoke(null, oldStore);

        _priority[type] = null;
        _priority.Remove(type);
    }

    public void Destroy(IDragonStore store)
    {
        IDragonStore oldStore;
        _priority.TryGetValue(store.GetType(), out oldStore);

        if (oldStore == store) ClearPriority(store.GetType());
        store.OnDestroyed.Invoke();

        foreach (var bind in store.Bindings)
        {
            bind.DoDestroy();
        }
    }
}