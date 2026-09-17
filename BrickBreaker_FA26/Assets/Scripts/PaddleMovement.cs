using UnityEngine;

public class PaddleMovement : MonoBehaviour
{

    private float _speed = 6.0f;
    
    public KeyCode LeftDirection = KeyCode.LeftArrow;
    public KeyCode RightDirection = KeyCode.RightArrow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movement = 0.0f;
        
        if (Input.GetKey(LeftDirection))
        {
            movement -= _speed;
        }

        if (Input.GetKey(RightDirection))
        {
            movement += _speed;
        }
        
        movement *= Time.deltaTime;
        transform.Translate(movement, 0.0f, 0.0f);
    }
}
