using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallBehavior : MonoBehaviour
{
    [Header("Ball Properties")]
    [SerializeField] private float _launchForce = 7.0f;
    [SerializeField] private float _speedIncrement = 1.1f;
    [SerializeField] private float _steepnessThreshold = 0.25f;
    [SerializeField] private float _paddleInfluence = 0.4f;

    private AudioSource _source;
    
    private Rigidbody2D _rb;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip _wallHit;
    [SerializeField] private AudioClip _paddleHit;
    [SerializeField] private AudioClip _scorePoint;
    [SerializeField] private AudioClip _soundtrack;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();
        
        ResetBall();

        _source.clip = _soundtrack;
        _source.loop = true;
        _source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.simulated = GameBehavior.Instance.State == Utilities.GameState.Play;
    }

    void OnDestroy()
    {
        GameBehavior.Instance.ResetPoint();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (!Mathf.Approximately(collision.rigidbody.linearVelocityX, 0.0f))
            {
                Vector2 direction = _rb.linearVelocity * (1.0f - _paddleInfluence)
                                    + collision.rigidbody.linearVelocity * _paddleInfluence;
                direction.Normalize();
                CheckSteepness(ref direction);
                _rb.linearVelocity = _rb.linearVelocity.magnitude * direction.normalized * _speedIncrement;
            }
            _source.PlayOneShot(_paddleHit);
        }
        else
        {
            _source.PlayOneShot(_wallHit);  
        }
        
        if (collision.gameObject.CompareTag("Brick"))
        {
            //_source.pitch = Random.Range(0.9f, 1.1f);
            _source.PlayOneShot(_scorePoint);
        }
        
    }

    private void CheckSteepness(ref Vector2 direction)
    {
        if (Mathf.Abs(direction.y) < _steepnessThreshold)
        {
            direction.y += 0.5f * Mathf.Sign(direction.y);
            direction.Normalize();
            direction.Abs();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Invoke(nameof(DestroyBall), 0.5f);
    }

    private void DestroyBall()
    {
        Destroy(gameObject);
    }

    private void ResetBall()
    {
        _rb.linearVelocity = Vector2.zero;
        transform.position = Vector3.zero;
        Vector2 direction = Random.insideUnitCircle.normalized;
        CheckSteepness(ref direction);
        _rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }
}
