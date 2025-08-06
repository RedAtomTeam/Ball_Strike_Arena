using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private AllLevelsConfig _allLevelConfig;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private ScoreBarController _scoreBarController;


    private void Awake()
    {
        _scoreBarController.Init(_levelConfig.scoreForWin);
        _scoreBarController.enoughScore += Win;
    }

    private void Win()
    {
        Time.timeScale = 0f;
        _winWindow.SetActive(true);
        
    }
}
