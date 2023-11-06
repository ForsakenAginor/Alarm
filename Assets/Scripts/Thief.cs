using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Thief : MonoBehaviour
{
    [SerializeField] private Transform _path;

    private Transform[] _points;
    private int _currentTargetIndex;
    private float _speed = 1;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private Color _defaultColor;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _defaultColor = _sprite.color;
        _points = new Transform[_path.childCount];
        _animator = GetComponent<Animator>();

        for (int i = 0; i < _path.childCount; i++)        
            _points[i] = _path.GetChild(i);        
    }

    private void Update()
    {
        Transform target = _points[_currentTargetIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        _animator.SetFloat("Speed", _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Point>(out Point point))
        {
            _currentTargetIndex++;

            if (_currentTargetIndex == _points.Length)
                _currentTargetIndex = 0;

            FlipSprite();
        }
        else if (collision.TryGetComponent<AlertSignal>(out AlertSignal zone))
        {
            Color invisibleColor = _defaultColor;
            invisibleColor.a = 0;
            _sprite.color = invisibleColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AlertSignal>(out AlertSignal zone))        
            _sprite.color = _defaultColor;        
    }

    private void FlipSprite()
    {
        Vector2 reflector = transform.localScale;
        Vector3 targetPosition = _points[_currentTargetIndex].transform.position;

        if ((targetPosition.x < transform.position.x && transform.localScale.x > 0) || (targetPosition.x > transform.position.x && transform.localScale.x < 0))
        {
            reflector.x *= -1;
            transform.localScale = reflector;
        }
    }
}
