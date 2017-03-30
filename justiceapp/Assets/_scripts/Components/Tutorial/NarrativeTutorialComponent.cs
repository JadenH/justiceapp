using System;

namespace _scripts.Tutorial
{
    [GameState(GameState = GameState.NarrativeTutorial)]
    public class NarrativeTutorialComponent : TutorialComponent
    {
        protected override void HandlePress(object sender, EventArgs eventArgs)
        {
            GameState.NarrativeTutorial.Set(State.Disabled);
            GameState.SwipingTutorial.Set(State.Enabled);
        }
    }
}