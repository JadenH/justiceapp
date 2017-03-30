using System;

namespace _scripts.Tutorial
{
    [GameState(GameState = GameState.MainMenu)]
    public class MainMenuComponent : TutorialComponent
    {
        protected override void HandlePress(object sender, EventArgs eventArgs)
        {
            GameState.MainMenu.Set(State.Disabled);
            GameState.NarrativeTutorial.Set(State.Enabled);
        }
    }
}