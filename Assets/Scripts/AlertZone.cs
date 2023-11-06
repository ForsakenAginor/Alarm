using UnityEngine;
using UnityEngine.Events;

public class AlertZone : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached;
    [SerializeField] private UnityEvent _leaved;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
            _reached?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
            _leaved?.Invoke();
    }
}
