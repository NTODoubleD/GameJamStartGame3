using UnityEngine;

public class ObjectRaycaster : MonoBehaviour
{
    [SerializeField] private int _maxDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Camera _camera;

    public T GetAtMousePoint<T>() where T : MonoBehaviour
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _maxDistance, _layerMask))
        {
            if (hit.collider.TryGetComponent(out T component))
                return component;

            return null;
        }

        return null;
    }
}