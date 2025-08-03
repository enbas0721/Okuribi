using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireBall : MonoBehaviour
{
    public enum FireBallStatus { Idle, Firing }
    [SerializeField] private float _moveTime;
    [SerializeField] private float _forwardFireHorizontalDistance;
    [SerializeField] private float _forwardFireVerticalDistance;
    [SerializeField] private float _upFireHorizontalDistance;
    [SerializeField] private float _upFireVerticalDistance;
    private FireBallStatus _status;
    private TanukiController _tanukiController;
    public void Initialize()
    {
        _status = FireBallStatus.Idle;
        _tanukiController = GetComponentInParent<TanukiController>();
    }
    public void OnDestroy()
    {
        DOTween.Kill(this.transform);
    }
    public FireBallStatus GetStatus()
    {
        return _status;
    }
    public void Fire() 
    {
        _status = FireBallStatus.Firing;
        TanukiController.FacedDirection facedDirection = _tanukiController.GetFacedDirection();
        var sequence = DOTween.Sequence();
        Vector3 position = this.transform.localPosition;
        /* 左右方向 */
        if ((facedDirection == TanukiController.FacedDirection.Right) || (facedDirection == TanukiController.FacedDirection.Left)) {
            sequence.Append(this.transform.DOLocalMove(new Vector3(position.x-2.0f, position.y, 0), 0.1f));
            sequence.Append(this.transform.DOLocalMove(new Vector3(_forwardFireHorizontalDistance, _forwardFireVerticalDistance, 0), _moveTime).SetLoops(2, LoopType.Yoyo));
            sequence.Append(this.transform.DOLocalMove(new Vector3(position.x, position.y, 0), 0.1f));
            sequence.Play()
                .OnComplete(() =>
            {
                _status = FireBallStatus.Idle;
            });
        /* 上方向 */
        } else if (facedDirection == TanukiController.FacedDirection.Up) {
            sequence.Append(this.transform.DOLocalMove(new Vector3(position.x, position.y-2.0f, 0), 0.1f));
            sequence.Append(this.transform.DOLocalMove(new Vector3(_upFireHorizontalDistance, _upFireVerticalDistance, 0), _moveTime).SetLoops(2, LoopType.Yoyo));
            sequence.Append(this.transform.DOLocalMove(new Vector3(position.x, position.y, 0), 0.1f));
            sequence.Play()
                .OnComplete(() =>
            {
                _status = FireBallStatus.Idle;
            });
        /* 下方向 */
        } else {
            sequence.Append(this.transform.DOLocalMove(new Vector3(position.x, position.y+2.0f, 0), 0.1f));
            sequence.Append(this.transform.DOLocalMove(new Vector3(_upFireHorizontalDistance, -_upFireVerticalDistance, 0), _moveTime).SetLoops(2, LoopType.Yoyo));
            sequence.Append(this.transform.DOLocalMove(new Vector3(position.x, position.y, 0), 0.1f));
            sequence.Play()
                .OnComplete(() =>
            {
                _status = FireBallStatus.Idle;
            });
        } 
    }
}
