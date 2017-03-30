using System;

namespace _scripts.Tutorial
{
    [GameState(GameState = GameState.SwipeUpTutorial)]
    public class SwipeUpTutorialComponent : TutorialComponent
    {
        protected override void HandlePress(object sender, EventArgs eventArgs)
        {
            GameState.SwipeUpTutorial.Set(State.Disabled);
            GameState.FinalTutorial.Set(State.Enabled);
        }
    }
}