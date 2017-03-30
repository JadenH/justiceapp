using System;

public abstract partial class DragonSubscriber<T>
{
    public void SubscribeTo(Type dragonType)
    {
        Dragon.Prioritize += (newData, oldData) =>
        {
            Data = (T)newData;
        };
        var dragonStore = Dragon.GetPriority<IDragonStore>(dragonType);
        if (dragonStore != null)
        {
            Data = (T)dragonStore;
        }
    }
}