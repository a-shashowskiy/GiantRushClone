using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GiantRushClone
{
    public class PlayerController : MonoBehaviour
    {
        public int score;
        public int maxScore = 10;
        public SickmenColor curentColor;
        [SerializeField] private TMPro.TextMeshProUGUI scoreText;
        [SerializeField] private Animator _animator; //Hero Animator
        [SerializeField] private FloatingJoystick _inputController;
        [Space][SerializeField] private float moveSpeed = 5;
        [SerializeField] private float moveHorizontlMultiplayer = 3;
        [SerializeField] private float rotationSpeed = 5;
        [SerializeField] private float levelWidth = 5f;
        [SerializeField] private AnimationCurve speedDropOnEdges;
        [SerializeField] private AnimationCurve heroTurnCurve;
        [SerializeField] private float characterWidth = 1;
        [Range(0, 0.99f)][Tooltip("1 - half of Level Width")][SerializeField] private float slowDownRange;
        [SerializeField] private SkinnedMeshRenderer _heroMR;
        Transform playerMashTransform;
        private Material heroMaterial;
        
        private Rigidbody _rigB;
        private Vector3 _moveVector; 
        private float _sidewaysSpeed;
        private float _currentSpeed;

        Managers.GameController gameManager;
        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = 60;
            gameManager = Managers.GameController.Instance;
            score = 0;
            _currentSpeed = moveSpeed;
            playerMashTransform = _animator.transform;
            _rigB = GetComponent<Rigidbody>();
            heroMaterial = _heroMR.material;

            int i = Random.Range(0, 3);
            switch (i)
            {
                case 0: heroMaterial.color = Color.red; curentColor = SickmenColor.Red; break;
                case 1: heroMaterial.color = Color.green; curentColor = SickmenColor.Green; break;
                case 2: heroMaterial.color = Color.blue; curentColor = SickmenColor.Blue; break;
                case 3: heroMaterial.color = Color.yellow; curentColor = SickmenColor.Yellow; break;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(gameManager.State == Managers.GameState.Game)
            { 
                HandleMuve();
                transform.position += _moveVector * Time.deltaTime;
            }
            if(gameManager.State == Managers.GameState.Victory)
            {
                _animator.SetBool("Win", true);
            }
            if(gameManager.State == Managers.GameState.Defeat)
            {
                _animator.SetBool("Fail", true);
            }
        }
        void HandleMuve()
        {
            
            //SET ANIMATION FLAG
            if (score >= 0)
            {
                _animator.SetBool("Run", true);
            }
            

            //Muve forward
            _moveVector.z += _currentSpeed;
          
            //Muve Left right
            if (_inputController != null)
            {
                if(_inputController.Horizontal != 0)
                {  
                    _moveVector.x += _inputController.Horizontal* moveHorizontlMultiplayer; 
                }
            }

            ModifySideSpeedRelativeToLevelEdge();
           
            print(_moveVector);
            transform.position += _moveVector * Time.deltaTime; 
            if (Mathf.Abs(transform.position.x) > levelWidth / 2 - characterWidth / 2)
            {
                Vector3 slide = new Vector3((levelWidth / 2 * Mathf.Sign(transform.position.x) -
                    characterWidth / 2 * Mathf.Sign(transform.position.x)) , transform.position.y,
                    transform.position.z);
                transform.position = Vector3.Lerp(transform.position, slide, _currentSpeed);
            }

            RotateMesh(_moveVector);
            // Reset values
            _moveVector = new Vector3(0,0, _currentSpeed);// Vector3.zero;
            _sidewaysSpeed = 0;
        }

        private void ModifySideSpeedRelativeToLevelEdge()
        {
            float slowLength = levelWidth / 2 - levelWidth / 2 * slowDownRange;

            if (transform.position.x>= slowLength && _sidewaysSpeed > 0 || transform.position.x <= -slowLength && _sidewaysSpeed < 0)
            {
                float relativeSlowPosition = Mathf.Abs(transform.position.x) + characterWidth / 2 - slowLength;
                _sidewaysSpeed *= speedDropOnEdges.Evaluate((relativeSlowPosition / slowLength));
            }

            _moveVector.z = _sidewaysSpeed;
        }
        void RotateMesh(Vector3 muveDir)
        {
            print("MUVE DIR" + muveDir);
            var targetRotation = Quaternion.LookRotation( muveDir );
            playerMashTransform.transform.rotation =
                    Quaternion.Euler(new Vector3(0,heroTurnCurve.Evaluate(_inputController.Horizontal), 0)); 
        }

        void AddScore()
        {
            if(score +1 <= maxScore)
            {
                score++;
                playerMashTransform.localScale= new Vector3(playerMashTransform.localScale.x +0.1f,
                    playerMashTransform.localScale.y + 0.1f, playerMashTransform.localScale.z + 0.1f);
                scoreText.text = score.ToString();
            }
            if (score == maxScore) gameManager.ChangeState(Managers.GameState.Victory);
        }
        void RemuveScore()
        {
            if (score - 1 != -2)
            {
                score--;
                playerMashTransform.localScale = new Vector3(playerMashTransform.localScale.x - 0.1f,
                    playerMashTransform.localScale.y - 0.1f, playerMashTransform.localScale.z - 0.1f);
                scoreText.text = score.ToString();
            }
            if (score < 0) gameManager.ChangeState(Managers.GameState.Defeat);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Stikman"))
            {
                if (curentColor == other.GetComponent<Stikman>().colorToSet)
                {
                    AddScore();
                    Destroy(other.gameObject);
                }
                else 
                { 
                    RemuveScore();
                    other.GetComponent<BoxCollider>().enabled = false;
                }

            }
        }
         
    } 
}
