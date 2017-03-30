namespace _scripts
{
    public class GameStateSubscriber : DragonBehaviour
    {
        public GameState[] GameState;

        protected override void Awake()
        {
            base.Awake();
            foreach (var gameState in GameState)
            {
                SubscribeTo(gameState);
            }
        }
    }
}