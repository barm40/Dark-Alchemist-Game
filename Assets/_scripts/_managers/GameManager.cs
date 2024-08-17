using System;
using Infra;

namespace _managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public GameState state;
    
        public void UpdateGameState(GameState newState)
        {
            state = newState;

            switch (newState)
            {
                case GameState.MainMenu:
                    // HandleMainMenu();
                    break;
                case GameState.Playing:
                    // HandlePlay()
                    break;
                case GameState.PauseMenu:
                    // HandlePause();
                    break;
                case GameState.DeathScreen:
                    // HandleDeath();
                    break;
                case GameState.NextLevel:
                    // HandleLevel();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        public enum GameState
        {
            MainMenu,
            Playing,
            PauseMenu,
            DeathScreen,
            NextLevel
        }
    }
}
