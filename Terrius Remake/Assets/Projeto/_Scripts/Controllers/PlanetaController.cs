using TMPro;
using UnityEngine;

public class PlanetaController : MonoBehaviour
{
    public float speed;

    [Header("Interface")]
    [SerializeField] private GameObject interface_obj;
    [SerializeField] private TextMeshProUGUI points_txt;
    [HideInInspector] public int points;
    private float ponto;

    [Header("GameOver")]
    [SerializeField] private GameObject gameOver_interface;
    [SerializeField] private TextMeshProUGUI txt_dead;

    public static bool gamestart { get; set; }

    void Start()
    {
        points = 0;
        ponto = 0;

        gamestart = false;
    }

    void Update()
    {
        if (!CharacterController.dead)
        {
            ponto += Time.deltaTime * speed;
            points = (int)ponto;
            points_txt.text = points.ToString("D5");
        }
    }

    public void Dead()
    {
        txt_dead.text = PlatformGenerator.Biome_name_dead;
        interface_obj.SetActive(false);
        gameOver_interface.SetActive(true);
        GameController.current.Dead();
    }
}
