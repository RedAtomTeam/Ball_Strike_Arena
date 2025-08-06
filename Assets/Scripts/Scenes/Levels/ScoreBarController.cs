using System;
using TMPro;
using UnityEngine;

public class ScoreBarController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Transform _scoreBar;
    private int _scoreNow;
    private int _scoreMax;

    public event Action enoughScore;
    
    public void Init(int scoreMax)
    {
        _scoreNow = 0;
        _scoreMax = scoreMax;
        UpdateScores();
    }

    public void AddScore(int score)
    {
        _scoreNow += score;
        UpdateScores();
        if (_scoreNow >= _scoreMax)
            enoughScore?.Invoke();
    }

    private void UpdateScores()
    {
        _scoreText.text = _scoreNow.ToString();
        _scoreBar.localScale = new Vector3((float)_scoreNow / (float)_scoreMax, 1, 1);
    }

}
