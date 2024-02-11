using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
            // Определяем направление к ближайшему объекту
            var direction = (nearestCollectible.position - _head.transform.position).normalized;

            // Определяем угол поворота относительно оси Z с учетом поворота головы
            var targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            targetAngle += _head.transform.eulerAngles.y; // Учитываем поворот головы

            // Создаем новую ротацию только по оси Z
            var targetRotation = Quaternion.Euler(0, 0, targetAngle + _offset);

            // Устанавливаем ротацию объекта UI
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
