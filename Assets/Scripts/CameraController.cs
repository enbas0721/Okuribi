using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector2 _threshold;
    [SerializeField] private Vector2 _minPosition;
    [SerializeField] private Vector2 _maxPosition;

    private Vector3 _offset;

    void Start()
    {
        _offset = new Vector3(0,0,this.transform.position.z);
    }

    void LateUpdate()
    {
        Vector3 targetPosition = _playerTransform.position + _offset;

        if (Mathf.Abs(this.transform.position.x - targetPosition.x) > _threshold.x)
        {
            if (this.transform.position.x < targetPosition.x)
            {
                targetPosition.x = _playerTransform.position.x - _offset.x;
            }
            else
            {
                targetPosition.x = _playerTransform.position.x + _offset.x;
            }
        }
        else
        {
            targetPosition.x = this.transform.position.x;
        }

        if (Mathf.Abs(this.transform.position.y - targetPosition.y) > _threshold.y)
        {
            if (this.transform.position.y < targetPosition.y)
            {
                targetPosition.y = _playerTransform.position.y - _offset.y;
            }
            else
            {
                targetPosition.y = _playerTransform.position.y + _offset.y;
            }
        }
        else
        {
            targetPosition.y = this.transform.position.y;
        }

        targetPosition.x = Mathf.Clamp(targetPosition.x, _minPosition.x, _maxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, _minPosition.y, _maxPosition.y);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed);
        this.transform.position = smoothedPosition;
    }
}
