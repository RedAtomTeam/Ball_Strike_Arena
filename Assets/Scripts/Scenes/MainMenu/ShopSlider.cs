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
    [SerializeField] private GameObject _priceBlock;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _stuffImage;

    [SerializeField] private GameObject _isNotBuy;
    [SerializeField] private GameObject _isSelected;
    [SerializeField] private GameObject _isNotSelected;


    private void Awake()
    {
        _leftSliderBtn.onClick.AddListener(SlideLeft);
        _rightSliderBtn.onClick.AddListener(SlideRight);
        _buyButton.onClick.AddListener(Buy);
    }

    private void OnEnable()
    {
        SaveLoadConfigsService.Instance.SaveAll();
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
        //Обновление отображения кнопок
        _leftSliderBtn.gameObject.SetActive(true);
        _rightSliderBtn.gameObject.SetActive(true);

        _isNotBuy.SetActive(false);
        _isSelected.SetActive(false);
        _isNotSelected.SetActive(false);

        if (_targetStuff.isBuy)
            if (_targetStuff.isSelected)
                _isSelected.SetActive(true); 
            else
                _isNotSelected.SetActive(true);
        else
            _isNotBuy.SetActive(true);


        if (_targetStuff == _storeConfig.stuff[0])
            _leftSliderBtn.gameObject.SetActive(false);

        if (_targetStuff == _storeConfig.stuff[_storeConfig.stuff.Count-1])
            _rightSliderBtn.gameObject.SetActive(false);

        // Обновление отображения цены
        _priceText.text = _targetStuff.price.ToString();
        if (_targetStuff.isBuy)
            _priceBlock.SetActive(false);
        else 
            _priceBlock.SetActive(true);

        // Обновление отображения картинки товара
        _stuffImage.sprite = _targetStuff.sprite;
    }

    public void Buy()
    {
        if (_storeConfig.money >= _targetStuff.price)
        {
            _storeConfig.money -= _targetStuff.price;
            _targetStuff.isBuy = true;
            SaveLoadConfigsService.Instance.SaveAll();
            UpdateView();
        }
    }

    public void Select()
    {
        foreach(var stuff in _storeConfig.stuff)
            if (stuff == _targetStuff)
                stuff.isSelected = true;
            else
                stuff.isSelected = false;
        UpdateView();
    }
}
