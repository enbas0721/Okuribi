using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyObj;
    [SerializeField] private float _spawnTime;
    public bool _isActive;
    private float _nextSpawnTime;
    private void OnEnable() {
        _isActive = true;
        _nextSpawnTime = Time.time + _spawnTime;   
    }
    private void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            GameObject enemy = Instantiate(_enemyObj, this.transform.position, Quaternion.identity);
            enemy.GetComponent<Enemy>().Initialize();

            _nextSpawnTime = Time.time + _spawnTime;
        }
    }
}
