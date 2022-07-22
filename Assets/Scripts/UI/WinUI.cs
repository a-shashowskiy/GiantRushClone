using UnityEngine;
using UnityEngine.UI;
namespace GiantRushClone
{
    public class WinUI : MonoBehaviour
    {
        [SerializeField] private PlayerController hero;
        [SerializeField] private Button _playButon;
        [SerializeField] private TMPro.TextMeshProUGUI _scoreText;
        // Start is called before the first frame update
        void Start()
        {
            _scoreText.text = "SCORE: "+hero.score.ToString();

            if (_playButon != null)
            {
                _playButon.onClick.AddListener(() =>
                {
                    //TODO LEVEL MANAGER
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                });
            }
        }
    }
}
