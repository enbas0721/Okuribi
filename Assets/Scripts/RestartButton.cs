using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private AudioClip _pushSound;
    void Update()
    {
        if (Input.GetKey("space"))
        {
            PushStartButton();
        }
    }
    public void PushStartButton()
    {
        if (!GameManager.Instance._isActive)
        {
            BGMManager._bgmInstance.PlaySE(_pushSound);
            SceneManager.LoadScene("Start");
            Destroy(GameManager.Instance.gameObject);
            Destroy(BGMManager._bgmInstance.gameObject);
        }
    }
}