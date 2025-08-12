using UnityEngine;


public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _defaultBallPref;
    [SerializeField] private float _relaxTime;
    private float _timeNow = 0;

    private bool _isBallWait = false;

    [SerializeField] private float _maxForce = 20f; 
    [SerializeField] private float _maxDragDistance = 200f; 
    private Vector2 _touchStartPos;
    private bool _isDragging = false;
    private BallController _lastBall;
    [SerializeField] private DirectionVisualizer _directionVisualizer;
    [SerializeField] private ScoreBarController _scoreBarController;


    private void Awake()
    {
        Spawn();    
    }

    private void Spawn()
    {
        GameObject ball = Instantiate(_defaultBallPref);
        ball.transform.SetParent(transform, false);
        ball.transform.localPosition = Vector3.zero;
        _isBallWait = true;
        _lastBall = ball.GetComponent<BallController>();
        _lastBall.Init(_scoreBarController);
    }

    private void Update()
    {
        if (_isBallWait)
        {
            HandleTouchInput();
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

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartDrag(touch.position);
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    ContinueDrag(touch.position);
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    EndDrag(touch.position);
                    break;
            }
        }
    }

    private void StartDrag(Vector2 touchPosition)
    {
        _touchStartPos = touchPosition;
        _isDragging = true;
        _directionVisualizer.Activate();
    }

    private void ContinueDrag(Vector2 touchPosition)
    {
        if (!_isDragging) return;

        Vector2 dragVector = new Vector2(transform.position.x, transform.position.y) - touchPosition;
        float dragDistance = dragVector.magnitude;

        float normalizedDistance = Mathf.Clamp01(dragDistance / _maxDragDistance);
        float force = normalizedDistance * _maxForce;

        _directionVisualizer.Visualize(transform.position, touchPosition);
    }

    private void EndDrag(Vector2 touchEndPos)
    {
        if (!_isDragging) return;

        Vector2 dragVector = new Vector2(transform.position.x, transform.position.y) - touchEndPos;
        float dragDistance = dragVector.magnitude;

        float normalizedDistance = Mathf.Clamp01(dragDistance / _maxDragDistance);
        float force = normalizedDistance * _maxForce;
        Vector2 direction = dragVector.normalized;
        Vector3 worldDirection = new Vector3(-direction.x, -direction.y, 0);

        _isDragging = false;

        Kick(worldDirection * force);
        _directionVisualizer.Deactivate();
    }

    private void Kick(Vector3 force)
    {
        _lastBall.GetComponent<Collider2D>().enabled = true;
        _lastBall.Kick(force);
        _isBallWait = false;
    }
    

}
