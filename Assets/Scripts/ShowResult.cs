using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using unityroom.Api;

public class ShowResult : MonoBehaviour
{
    [SerializeField] private Text _HPNumText;
    [SerializeField] private Text _hitodamaNumText;
    [SerializeField] private Text _torchNumText;
    [SerializeField] private Text _totalText;

    void Start()
    {
        _HPNumText.gameObject.SetActive(false);
        _hitodamaNumText.gameObject.SetActive(false);
        _torchNumText.gameObject.SetActive(false);
        _totalText.gameObject.SetActive(false);

        ShowResultText();
    }

    void ShowResultText() 
    { 
        float totalScore = (float)GameManager.Instance.GetTotalScore();
        _HPNumText.text = GameManager.Instance._HP.ToString();
        _hitodamaNumText.text = GameManager.Instance._hitodamaNum.ToString();
        _torchNumText.text = GameManager.Instance._torchNum.ToString() + " / 9";
        _totalText.text = totalScore.ToString();

        UnityroomApiClient.Instance.SendScore(1, totalScore, ScoreboardWriteMode.HighScoreDesc);

        _HPNumText.gameObject.SetActive(true);
        _hitodamaNumText.gameObject.SetActive(true);
        _torchNumText.gameObject.SetActive(true);
        _totalText.gameObject.SetActive(true);
    }
}
