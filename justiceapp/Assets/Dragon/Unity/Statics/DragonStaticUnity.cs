using UnityEngine.Events;

public static class DragonStaticUnity
{
    public static void Prioritize(this UnityEvent e, IDragonSubscriber data)
    {
        e.AddListener(() => data.DragonStore.Prioritize());
    }

    public static void DestroyData(this UnityEvent e, IDragonSubscriber data)
    {
        e.AddListener(() => DragonStatic.Destroy(data.DragonStore));
    }
}