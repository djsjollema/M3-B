using UnityEngine;

public class TDPlayer : MonoBehaviour
{
    float speed = 5f;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float getHorizontal = Input.GetAxis("Horizontal");
        rb.linearVelocityX = getHorizontal * speed;

        float getVertical = Input.GetAxis("Vertical");
        rb.linearVelocityY = getVertical * speed;
        
    }
}
