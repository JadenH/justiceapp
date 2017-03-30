using System;

namespace _scripts.Tutorial
{
    [GameState(GameState = GameState.SwipingTutorial)]
    public class SwipingTutorialComponent : TutorialComponent
    {
        protected override void HandlePress(object sender, EventArgs eventArgs)
        {
            GameState.SwipingTutorial.Set(State.Disabled);
            GameState.SwipeUpTutorial.Set(State.Enabled);
        }
    }
}