using UnityEngine;
using Random = UnityEngine.Random;

public class BallBehavior : MonoBehaviour
{

    [SerializeField] private float _launchForce = 7.0f;
    [SerializeField] private float _speedIncrement = 1.1f;
    [SerializeField] private float _paddleInfluence = 0.4f;

    private Rigidbody2D _rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (!Mathf.Approximately(collision.rigidbody.linearVelocityX, 0.0f))
            {
                Vector2 direction = _rb.linearVelocity * (1.0f - _paddleInfluence)
                                    + collision.rigidbody.linearVelocity * _paddleInfluence;
                _rb.linearVelocity = _rb.linearVelocity.magnitude * direction.normalized * _speedIncrement;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ResetBall();
    }

    private void ResetBall()
    {
        _rb.linearVelocity = Vector2.zero;
        transform.position = Vector3.zero;
        Vector2 direction = Random.insideUnitCircle.normalized;
        _rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }
}
