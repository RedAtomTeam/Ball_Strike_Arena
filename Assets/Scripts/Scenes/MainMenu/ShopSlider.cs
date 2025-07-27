using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlider : MonoBehaviour
{
    [SerializeField] private StoreConfig _storeConfig;

    [SerializeField] private Button _leftSliderBtn;
    [SerializeField] private Button _rightSliderBtn;

    private StoreStuff _targetStuff;
    private int _targetSuffIndex; 
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _stuffImage;


    private void Awake()
    {
        _leftSliderBtn.onClick.AddListener(SlideLeft);
        _rightSliderBtn.onClick.AddListener(SlideRight);
        _buyButton.onClick.AddListener(Buy);
    }

    private void OnEnable()
    {
        _targetSuffIndex = 0;
        _targetStuff = _storeConfig.stuff[_targetSuffIndex];
        UpdateView();
    }

    private void SlideLeft()
    {
        _targetSuffIndex--;
        _targetStuff = _storeConfig.stuff[_targetSuffIndex];
        UpdateView();
    }

    private void SlideRight() 
    {
        _targetSuffIndex++;
        _targetStuff = _storeConfig.stuff[_targetSuffIndex];
        UpdateView();
    }

    private void UpdateView()
    {
        // Обновление отображения кнопок
        _leftSliderBtn.gameObject.SetActive(true);
        _rightSliderBtn.gameObject.SetActive(true);

        if (_targetStuff == _storeConfig.stuff[0])
            _leftSliderBtn.gameObject.SetActive(false);

        if (_targetStuff == _storeConfig.stuff[_storeConfig.stuff.Count-1])
            _rightSliderBtn.gameObject.SetActive(false);

        // Обновление отображения цены
        _price.text = _targetStuff.price.ToString();

        // Обновление отображения кнопки покупки
        _buyButton.gameObject.SetActive(!_targetStuff.isBuy);

        // Обновление отображения картинки товара
        _stuffImage.sprite = _targetStuff._sprite;
    }

    public void Buy()
    {
        if (_targetStuff.isBuy)
            return;

        if (_storeConfig.money >= _targetStuff.price)
        {
            _storeConfig.money -= _targetStuff.price;
            _targetStuff.isBuy = true;
            SaveLoadConfigsService.Instance.SaveAll();
            UpdateView();
        }
    }
}
