using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private float speedJump;
    [SerializeField] private LayerMask layerMaskFloor;

    private bool jump;
    private bool endJump;


    private Rigidbody2D rb;
    private Animator anim;

    public static bool dead { get; set; }
    private bool death = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        dead = false; jump = false; endJump = false;
        death = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Dead()) Jump();

        if (jump)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, layerMaskFloor);
            if (!hit) endJump = true;

            if (endJump)
            {
                if (hit)
                {
                    anim.SetTrigger("endJump");
                    jump = false;
                    endJump = false;
                }
            }
        }
    }

    public void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, layerMaskFloor);

        if (hit)
        {
            rb.AddForce(Vector2.up * speedJump, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
            jump = true;
        }
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
}
