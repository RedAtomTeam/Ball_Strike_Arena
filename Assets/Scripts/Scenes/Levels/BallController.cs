using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{ 
    [SerializeField] private BallConfig _ballConfig;
    [SerializeField] private Image _ballImage;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private ScoreBarController _scoreBarController;

    public BallConfig GetConfig {  get { return _ballConfig; } }


    public void Init(ScoreBarController scoreBarController)
    {
        _scoreBarController = scoreBarController;
    }

    public void Kick(Vector3 force)
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void UpdateConfig()
    {
        _ballImage.sprite = _ballConfig.ballSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_ballConfig.nextBallConfig != null)
            if (collision.gameObject.GetComponent<BallController>())
                if (collision.gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
                    if (GetConfig == collision.gameObject.GetComponent<BallController>().GetConfig)
                    {
                        Destroy(collision.gameObject);
                        _ballConfig = _ballConfig.nextBallConfig;
                        _scoreBarController.AddScore(_ballConfig.scoreForSpawn);
                        UpdateConfig();
                    }
    }
}
