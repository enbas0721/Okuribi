using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
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
        BGMManager._bgmInstance.PlaySE(_pushSound);
        SceneManager.LoadScene("Main");
    }
}
