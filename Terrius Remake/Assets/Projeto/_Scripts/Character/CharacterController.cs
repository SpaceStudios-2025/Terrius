using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Trilha")]
    [SerializeField] private float[] posicoesY = { -0.323f, -0.627f, -0.946f };
    [SerializeField] private float velocidadeTrocaTrilha = 5f;
    private int trilhaAtual = 1;

    private Rigidbody2D rb;
    private Animator anim;

    private float posicaoYAlvo;

    [Header("Pulo")]
    [SerializeField] private float forcaPulo = 8f;
    [SerializeField] private float gravidade = -10f;

    private float velocidadeVertical = 0f;
    private bool pulando = false;
    private bool isGround = true;

    [Header("Morte")]
    public static bool dead { get; set; }
    private bool death;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        dead = false; death = false; isGround = true;

        trilhaAtual = 1;
        posicaoYAlvo = posicoesY[trilhaAtual];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        if (Input.GetKeyDown(KeyCode.DownArrow)) Down();
        else if (Input.GetKeyDown(KeyCode.UpArrow)) Up();
    }

    public void Down()
    {
        if (trilhaAtual < posicoesY.Length - 1 && !pulando)
        {
            trilhaAtual++;
            MudarTrilha();
        }
    }

    public void Up()
    {
        if (trilhaAtual > 0 && !pulando)
        {
            trilhaAtual--;
            MudarTrilha();
        }
    }

    void MudarTrilha()
    {
        GetComponent<SpriteRenderer>().sortingOrder = trilhaAtual + 1;
        posicaoYAlvo = posicoesY[trilhaAtual];
    }

    void FixedUpdate()
    {
        Vector2 pos = rb.position;

        // Se está pulando, simula gravidade
        if (pulando)
        {
            velocidadeVertical += gravidade * Time.fixedDeltaTime;
            pos.y += velocidadeVertical * Time.fixedDeltaTime;

            // Chegou ou passou da trilha, então aterrissou
            if (pos.y <= posicaoYAlvo)
            {
                pos.y = posicaoYAlvo;
                velocidadeVertical = 0f;
                pulando = false;
                isGround = true;

                anim.SetTrigger("endJump");
            }
        }
        else
        {
            // Se não está pulando, suaviza a troca de trilha
            pos.y = Mathf.Lerp(pos.y, posicaoYAlvo, velocidadeTrocaTrilha * Time.fixedDeltaTime);
        }

        rb.MovePosition(pos);
    }


    public bool Dead()
    {
        if (dead)
        {
            if (!death)
            {
                anim.SetTrigger("death");
                FindFirstObjectByType<Camera_Controller>().TriggerDeathEffect();

                death = true;
            }
        }

        return dead;
    }

    void Jump()
    {
        if (isGround)
        {
            anim.SetTrigger("jump");

            velocidadeVertical = forcaPulo;
            pulando = true;
            isGround = false;
        }    
    }
}
