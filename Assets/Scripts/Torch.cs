using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private GameObject _torchObj;
    [SerializeField] private GameObject _torchFiredObj;
    [SerializeField] private AudioClip _fireSound;
    private bool _isFired = false;
    public void Initialize()
    {
        _torchObj.SetActive(true);
        _torchFiredObj.SetActive(false);
    }
    void TurnOnFire()
    {
        _isFired = true;
        _torchObj.SetActive(false);
        _torchFiredObj.SetActive(true);
        BGMManager._bgmInstance.PlaySE(_fireSound);
        GameManager.Instance.AddTorchNum();
        GameManager.Instance.HealHP();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Fire")) && !_isFired)
        {
            TurnOnFire();
        }
    }
}
