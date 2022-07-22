using UnityEngine.UI;
using UnityEngine;
namespace GiantRushClone
{
    public class LuseUI : MonoBehaviour
    {
        [SerializeField] private Button _playButon;
        // Start is called before the first frame update
        void Start()
        {
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
