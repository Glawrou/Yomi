using System.Linq;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private float _offset;
    [SerializeField] private Transform _head;

    private CollectibleObject[] _collectibleObject;
    private RectTransform _rectTransform;

    public void Initialization(CollectibleObject[] collectible)
    {
        _collectibleObject = collectible;
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_collectibleObject == null || _collectibleObject.Length == 0 || _head == null)
        {
            return;
        }

        var nearestCollectible = GetNearest(_collectibleObject);
        if (nearestCollectible != null)
        {
            var direction = (nearestCollectible.position - _head.transform.position).normalized;
            var targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            targetAngle += _head.transform.eulerAngles.y;
            var targetRotation = Quaternion.Euler(0, 0, targetAngle + _offset);
            _rectTransform.localRotation = targetRotation;
        }
    }

    private Transform GetNearest(CollectibleObject[] collectible)
    {
        var nearestCollectible = collectible
            .OrderBy(item => Vector3.Distance(_head.transform.position, item.transform.position))
            .FirstOrDefault();
        return nearestCollectible != null ? nearestCollectible.transform : null;
    }
}
