using UnityEngine;

public class PhysicsAgent : MonoBehaviour
{
    public float speed;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (TargetPos.TryGetTargetPos(out var pos))
        {
            var prefVelocity = (pos - (Vector2)transform.position).normalized * speed;
            _rb.velocity = prefVelocity;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }
}