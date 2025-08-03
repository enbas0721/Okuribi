using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager _bgmInstance;
    private AudioSource _audioSource;

    void Awake()
    {
        if (_bgmInstance == null)
        {
            _bgmInstance = this;
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
        }
        else if (_bgmInstance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayBGM();
    }

    // BGMを再生するメソッド
    private void PlayBGM()
    {
        _audioSource.Play();
    }

    public void PlaySE(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    // 再生位置を取得するメソッド
    public float GetPlaybackPosition()
    {
        return _audioSource.time;
    }

    // 再生位置を設定するメソッド
    public void SetPlaybackPosition(float position)
    {
        _audioSource.time = position;
        _audioSource.Play();
    }
}
