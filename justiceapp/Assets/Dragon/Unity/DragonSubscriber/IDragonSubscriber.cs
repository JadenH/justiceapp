using System;
using System.Collections.Generic;
using _scripts;

public interface IDragonSubscriber : IDragonBehaviour
{
    Action OnUnsubscribed { get; set; }

    IDragonStore DragonStore { get; }

    void SubscribeTo(GameState gameState);

}