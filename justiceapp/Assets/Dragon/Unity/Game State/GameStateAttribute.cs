using System;
using _scripts;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class GameStateAttribute : Attribute
{
    public GameState GameState { get; set; }
}