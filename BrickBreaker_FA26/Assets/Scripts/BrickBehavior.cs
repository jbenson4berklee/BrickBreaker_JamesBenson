using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (Utilities.BrickState.One == _state)
            {
                
            }

            if (Utilities.BrickState.Two == _state)
            {
                
            }

            if (Utilities.BrickState.Three == _state)
            {
                
            }
            
            GameBehavior.Instance.ScorePoint(transform.position.x < 0 ? 1 : 0);
            Destroy(gameObject);
        }
    }
    
    //Brick Health Code?
    
    private Utilities.BrickState _state;
    
    public Utilities.BrickState State
    {
        get => _state;
        
        set
        {
            _state = value;
        }
    }
    
    //stuck on how to get bricks to change color with _spriteRenderer and how to keep them on screen
}
