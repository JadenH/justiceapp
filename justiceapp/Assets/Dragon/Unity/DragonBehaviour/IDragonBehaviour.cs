using System;
using UnityEngine.UI;
using _scripts;

public interface IDragonBehaviour
{
    bool IsActive { get; }

    Action OnEnabled { get; set; }
    Action OnDisabled { get; set; }
    Action OnDestroyed { get; set; }

    void OnGameStateChanged(GameState gameState, State state, bool skipAnimation = false);

    void DoDestroy();

    bool ShouldActivate(Selectable selectable);
}