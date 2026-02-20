using UnityEngine;

public class PatrolAgent : MonoBehaviour
{
    [Header("Movement Points")]
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 2f;

    private Transform _target;

    private void Start()
    {
        _target = _pointB;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            _target.position,
            _moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, _target.position) < 0.05f)
        {
            SwitchTarget();
        }
    }

    private void SwitchTarget()
    {
        if (_target == _pointA)
            _target = _pointB;
        else
            _target = _pointA;

        Flip();
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}