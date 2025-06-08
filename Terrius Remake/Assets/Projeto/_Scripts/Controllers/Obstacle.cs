using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Velocidade")]
    public float minSpeed;
    public float maxSpeed;

    private float speed = 1f;

    private bool dead = false;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        dead = false;
    }

    void Update()
    {
        if (!FindFirstObjectByType<CharacterController>().Dead())
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (transform.position.x < FindFirstObjectByType<ObstaclesManager>().max_x) Destroy(gameObject);
        }
        else
        {
            if (GetComponent<Animator>())
            {
                if (!dead)
                {
                    GetComponent<Animator>().SetTrigger("dead");
                    gameObject.AddComponent<Rigidbody2D>();
                    GetComponent<PolygonCollider2D>().isTrigger = false;
                    dead = true;
                }
            }
        }
    }
}
