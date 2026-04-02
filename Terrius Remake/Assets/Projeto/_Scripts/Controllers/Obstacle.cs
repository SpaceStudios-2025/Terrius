using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Velocidade")]
    public float minSpeed;
    public float maxSpeed;

    private float speed = 1f;

    private bool dead = false;

    [Space]
    public Obstaculos type;

    public int platform;

    [Space]
    [SerializeField] private AudioClip soundCoin;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        dead = false;
    }

    void Update()
    {
        if (PlanetaController.gamestart)
        {
            if (!GameController.dead)
            {
                if (!GameController.victory) Move();
                else if (GameController.victory && type == Obstaculos.obstaculoAlto) Move();
            }
            else
            {
                if (GetComponent<Animator>())
                {
                    if (!dead)
                    {
                        if (GetComponent<Animator>())
                            GetComponent<Animator>().SetTrigger("dead");
                            
                        GetComponent<Rigidbody2D>().gravityScale = 1f;
                        GetComponent<PolygonCollider2D>().isTrigger = false;
                        dead = true;
                    }
                }
            }
        }
    }

    void Move()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < FindFirstObjectByType<ObstaclesManager>().max_x) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && type == Obstaculos.Coin)
        {
            if (platform == GameController.trilha)
            {
                //Coins ++;
                PlanetaController.instance.AddCoins();
                GameController.current.PlayAudio(soundCoin);
                Destroy(gameObject);
            }
        }
    }
}
