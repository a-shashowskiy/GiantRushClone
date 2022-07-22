using System; 
using UnityEngine;
using GiantRushClone.Utils;

namespace GiantRushClone.Managers
{
    [Serializable]
    public enum GameState
    {
        None,
        LoadingMainMenu,
        MainMenu,
        SettingsMenu, //in case made bigger
        Game,
        Victory,
        Defeat
    }

    public class GameController : StaticInstance<GameController>
    {
        public static event Action<GameState> OnBeforeStateChanged;
        public static event Action<GameState> OnAfterStateChanged;

        public GameState State { get; private set; }

        // Kick the game off with the first state
        void Start() => ChangeState(GameState.MainMenu);

        public void ChangeState(GameState newState)
        {
            OnBeforeStateChanged?.Invoke(newState);

            State = newState;
            switch (newState)
            {
                case GameState.None:
                    break;
                case GameState.LoadingMainMenu:
                    break;
                case GameState.MainMenu:
                    break;
                case GameState.SettingsMenu:
                    break;
                case GameState.Game:
                    break;
                case GameState.Victory:
                    break;
                case GameState.Defeat:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            OnAfterStateChanged?.Invoke(newState);

            Debug.Log($"New state: {newState}");
        }
    }
}