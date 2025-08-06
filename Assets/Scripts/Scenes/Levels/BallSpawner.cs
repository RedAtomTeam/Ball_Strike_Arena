using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _defaultBallPref;
    [SerializeField] private float _relaxTime;
    private float _timeNow = 0;

    private bool _isBallWait = false;


    private void Awake()
    {
        Spawn();    
    }

    private void Spawn()
    {
        GameObject ball = Instantiate(_defaultBallPref);
        _defaultBallPref.transform.position = transform.position;
        _isBallWait = true;
    }

    private void Update()
    {
        if (_isBallWait)
        {
            // Код для выбора направления удара
        }
        else
        {
            if (_timeNow >= _relaxTime)
            {
                Spawn();
                _timeNow = 0;
            }
            else
                _timeNow += Time.deltaTime;
        }
    }

    private void Kick()
    {
        _isBallWait = false;
    }
    

}
