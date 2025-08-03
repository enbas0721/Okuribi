using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public enum EnemyStatus { Idle, Dead }
    [SerializeField] private float _moveSpeed;
    [SerializeField] private AudioClip _deadSound;
    private GameObject _tanukiObj;
    private GameObject _enemyObj;
    private EnemyStatus _status;
    public void Initialize()
    {
        _status = EnemyStatus.Idle;
        _tanukiObj = GameObject.Find("Tanuki");
    }

    private void Update()
    {
        if (_status == EnemyStatus.Idle)
        {
            float direction = _tanukiObj.transform.position.x - this.transform.position.x;
            this.transform.position = Vector2.MoveTowards(this.transform.position, _tanukiObj.transform.position, _moveSpeed * Time.deltaTime);
            if (direction > 0)
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        if (!GameManager.Instance._isActive)
        {
            _status = EnemyStatus.Dead;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            FireBall.FireBallStatus status = collision.gameObject.GetComponent<FireBall>().GetStatus();
            if (status == FireBall.FireBallStatus.Firing)
            {
                _status = EnemyStatus.Dead;
                BGMManager._bgmInstance.PlaySE(_deadSound);
                GameManager.Instance.AddHitodamaNum();
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            _status = EnemyStatus.Dead;
            Destroy(this.gameObject);
        }
    }
}
