using UnityEngine;
using UnityEngine.UI;

public class FieldSetter : MonoBehaviour
{
    [SerializeField] private StoreConfig _storeConfig;
    [SerializeField] private Image _fieldImage;

    private void Awake()
    {
        foreach (var field in _storeConfig.stuff)
            if (field.isSelected)
                _fieldImage.sprite = field.sprite;
    }

}
