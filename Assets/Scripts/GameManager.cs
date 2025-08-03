using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    Transform _playerTransform;
    [SerializeField]
    GameObject _enemySpawner;
    [SerializeField]
    GameObject _torchesObj;
    [SerializeField]
    Text _timerText;
    int _time = 0;
    [SerializeField]
    int _startTime = 60;
    [SerializeField]
    Text _hitodamaNumText; 
    [SerializeField]
    Text _torchNumText;
    [SerializeField]
    HPManager _HPManager;
    [SerializeField] 
    int _initHP = 3;
    
    public int _HP;
    public int _hitodamaNum = 0;
    public int _torchNum = 0;
    bool _timerIsActive = false;
    Vector2 _playerSpawnPosition;
    Coroutine _timer;
    
    public int _score = 0;
    public bool _isActive { get; set; } = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _playerSpawnPosition = _playerTransform.position;

        InitGame();
        StartGame();

        _isActive = true;
        
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

        if (!_timerIsActive)
        {
            if (_timer != null ) {
                StopCoroutine(_timer);
            }
            _timer = StartCoroutine(CountdownTimer());
        }
    }

    public void StartGame()
    {
        _playerTransform.position = _playerSpawnPosition;
        _playerTransform.gameObject.SetActive(true);

        _enemySpawner.SetActive(true);
        _isActive = true;

        InitGame();
    }

    void InitGame()
    {
        _HP = _initHP;
        _hitodamaNum = 0;
        _torchNum = 0;
        _timerIsActive = false;
        _time = _startTime;

        if (_torchesObj != null)
        {
            Torch[] torches = _torchesObj.GetComponentsInChildren<Torch>();

            foreach (Torch torch in torches)
            {
                torch.Initialize();
            }
        }

        UpdateHitodamaNum();
        UpdateHPUI();
    }

    IEnumerator CountdownTimer()
    {
        _timerIsActive = true;
        _time -= 1;
        if (_time < 0)
        {
            GameOver();
        } 
        else
        {
            UpdateTimerUI();
        }

        yield return new WaitForSeconds(1);

        _timerIsActive = false;
    }

    void UpdateTimerUI()
    {
        int minute = _time / 60;
        int second = _time % 60;
        _timerText.text = minute.ToString("D2") + ":" + second.ToString("D2");
    }

    public void AddHitodamaNum()
    {
        _hitodamaNum++;
        UpdateHitodamaNum();
    }

    public void AddTorchNum()
    {
        _torchNum++;
        UpdateTorchNum();
    }

    private void UpdateHitodamaNum()
    {
        _hitodamaNumText.text = "x" + _hitodamaNum.ToString();
    }
    
    private void UpdateTorchNum()
    {
        _torchNumText.text = "x" + _torchNum.ToString();
    }

    public void DecreaseHP()
    {
        _HP--;
        UpdateHPUI();

        if (_HP <= 0)
        {
            GameOver();
        }
    }

    public void HealHP()
    {
        _HP = _initHP;
        UpdateHPUI();
    }

    private void UpdateHPUI()
    {
        _HPManager.HPUpdate(_HP);
    }

    public int GetTotalScore()
    {
        _score = _HP * 100 + _hitodamaNum * 100 + _torchNum * 100;
        return _score;
    }
    public void GameOver()
    { 
        _isActive = false;
        StopCoroutine(_timer);
        _timerIsActive = false;
        _timer = null;
        _playerTransform.gameObject.SetActive(false);
        _enemySpawner.SetActive(false);
        SceneManager.LoadScene("Result");   
    }

}