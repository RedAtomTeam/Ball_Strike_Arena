using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButtonOpenCloseController : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;

    [SerializeField] private Image _buttonImage;
    [SerializeField] private Color _openColor;
    [SerializeField] private Color _lockColor;

    [SerializeField] private GameObject _openImage;
    [SerializeField] private GameObject _lockImage;

    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Color _openTextColor;
    [SerializeField] private Color _lockTextColor;

    [SerializeField] private Button _button;


    private void Awake()
    {
        _button.onClick.AddListener(OpenScene);
    }

    private void OnEnable()
    {
        if (_levelConfig.status)
            SetOpenStatus();
        else
            SetClosedStatus();
    }

    private void SetOpenStatus()
    {
        if (_openImage != null)
            _openImage.SetActive(true);
        if(_lockImage != null)
            _lockImage.SetActive(false);

        _button.interactable = true;
        _buttonImage.color = _openColor;
        _buttonText.color = _openTextColor;
    }

    private void SetClosedStatus()
    {
        if (_openImage != null)
            _openImage.SetActive(false);
        if (_lockImage != null)
            _lockImage.SetActive(true);

        _button.interactable = false;
        _buttonImage.color = _lockColor;
        _buttonText.color = _lockTextColor;
    }

    private void OpenScene()
    {
        SceneManager.LoadSceneAsync(_levelConfig.sceneName);
    }


}
