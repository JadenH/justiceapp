using Utility.Singleton;
using _scripts.Model;

public partial class Dragon : MonoSingleton<Dragon>
{
    public DragonStore<JusticeModel> Model { get; private set; }

    private void Awake()
    {
        InitStores();
        Model = new DragonStore<JusticeModel>(new JusticeModel());
        InitGameState();
    }
}