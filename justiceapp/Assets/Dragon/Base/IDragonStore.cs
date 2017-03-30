using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDragonStore
{
    Action OnDestroyed { get; set; }
    List<IDragonBehaviour> Bindings { get; set; }
    void Prioritize();
    void BindWith(IDragonBehaviour behaviour);
    void UnbindWith(IDragonBehaviour behaviour);
        
}