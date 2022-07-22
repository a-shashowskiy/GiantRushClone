using System;
using UnityEngine;

namespace GiantRushClone
{
    public class UIController : MonoBehaviour
    {
        public MainMenuUI mMenu;
        public LuseUI luseUI;
        public WinUI victoryUI;
        public GameObject gameUI;

        private void OnEnable()
        {
            Managers.GameController.OnAfterStateChanged += OnGameStateChange;
        }

        private void OnDisable()
        {
            Managers.GameController.OnAfterStateChanged -= OnGameStateChange;
        }

        private void OnGameStateChange(Managers.GameState gameState)
        {
            switch (gameState)
            {
                case Managers.GameState.None:
                    break;
                case Managers.GameState.LoadingMainMenu:
                    break;
                case Managers.GameState.MainMenu:
                    mMenu.gameObject.SetActive(true); 
                    break; 
                case Managers.GameState.Game:
                    mMenu.gameObject.SetActive(false);
                    gameUI.SetActive(true);
                    //mainGame.ShowInstantly();
                    break;
                case Managers.GameState.Victory:
                    mMenu.gameObject.SetActive(false); 
                    gameUI.SetActive(false);
                    victoryUI.gameObject.SetActive(true);
                    break;
                case Managers.GameState.Defeat:
                    mMenu.gameObject.SetActive(false);
                    gameUI.SetActive(false);
                    luseUI.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }
    } 
}
