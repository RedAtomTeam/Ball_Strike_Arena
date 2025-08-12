using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private StoreConfig _storeConfig;
    [SerializeField] private AllLevelsConfig _allLevelConfig;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private ScoreBarController _scoreBarController;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _levelText;


    private void Awake()
    {
        _scoreBarController.Init(_levelConfig.scoreForWin);
        _scoreBarController.enoughScore += Win;
        _levelText.text = $"LVL {_levelConfig.level}";
    }

    private void Win()
    {
        Time.timeScale = 0f;
        _winWindow.SetActive(true);
        _scoreText.text = _levelConfig.scoreForWin.ToString();
        _storeConfig.money += 10;
        SaveLoadConfigsService.Instance.SaveAll();
        if (_levelConfig.level != _allLevelConfig.levels[_allLevelConfig.levels.Count - 1].level)
            _allLevelConfig.levels.Where((e) => e.level == _levelConfig.level + 1).ToArray()[0].status = true;
    }

    public void NextLevel()
    {
        Time.timeScale = 1.0f;
        if (_levelConfig != _allLevelConfig.levels[_allLevelConfig.levels.Count - 1])
            SceneManager.LoadSceneAsync(_allLevelConfig.levels.Where((e) => e.level == _levelConfig.level + 1).ToList()[0].sceneName);
    }
}
