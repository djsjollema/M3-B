using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform[] points;
    float speed = 2f;
    SpriteRenderer spriteRenderer;

    int i;
    void Start()
    {
        transform.position = points[i].position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

        spriteRenderer.flipX = (transform.position.x - points[i].position.x) < 0;
    }


}
