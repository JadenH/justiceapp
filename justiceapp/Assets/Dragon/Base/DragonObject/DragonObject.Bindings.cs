using System.Collections.Generic;
using Newtonsoft.Json;

public partial class DragonStore<T>
{

    [JsonIgnore]
    public List<IDragonBehaviour> Bindings { get; set; }

    public void BindWith(IDragonBehaviour behaviour)
    {
        if (Bindings == null) Bindings = new List<IDragonBehaviour>();
        Bindings.Add(behaviour);
    }

    public void UnbindWith(IDragonBehaviour behaviour)
    {
        if (Bindings != null && Bindings.Contains(behaviour))
        {
            Bindings.Remove(behaviour);
        }
    }
}