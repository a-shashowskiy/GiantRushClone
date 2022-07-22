using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GiantRushClone
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playButon;
        [SerializeField] private Cinemachine.CinemachineVirtualCamera _startCam;
        [SerializeField] private CanvasGroup _canGrMenu;
        [SerializeField] private GameObject _canGame;
        // Start is called before the first frame update
        void Start()
        {
            StartGame();
        }

        void StartGame()
        {
            if (_playButon != null)
            {
                _playButon.onClick.AddListener(() =>
                {
                    _startCam.enabled = false;
                    _canGrMenu.alpha = 0;
                    _canGame.SetActive(true); 
                    Managers.GameController.Instance.ChangeState(Managers.GameState.Game);
                });
            }
        }
    }
}
