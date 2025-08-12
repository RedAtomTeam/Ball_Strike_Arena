using UnityEngine;

public class DirectionVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject _arrow;


    public void Activate()
    {
        _arrow.SetActive(true);
    }

    public void Deactivate() 
    {
        _arrow.SetActive(false);
    }

    public void Visualize(Vector3 startPos, Vector3 endPos)
    {
        if (!_arrow.activeSelf)
            _arrow.SetActive(true);
        Vector3 direction = endPos - startPos;
        float distance = direction.magnitude;
        _arrow.transform.position = (startPos + endPos) / 2f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _arrow.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        _arrow.transform.localScale = new Vector3(_arrow.transform.localScale.x, distance / 200, _arrow.transform.localScale.z);
    }


}
