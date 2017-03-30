using System;

namespace _scripts.Tutorial
{
    [GameState(GameState = GameState.FinalTutorial)]
    public class FinalTutorialComponent : TutorialComponent
    {
        protected override void HandlePress(object sender, EventArgs eventArgs)
        {
            GameState.FinalTutorial.Set(State.Disabled);
            GameState.CoreLoop.Set(State.Enabled);
        }
    }
}