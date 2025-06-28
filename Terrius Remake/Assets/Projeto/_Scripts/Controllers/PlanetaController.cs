using TMPro;
using UnityEngine;

public class PlanetaController : MonoBehaviour
{
    public static PlanetaController instance { get; set; }

    void Awake() => instance = !instance ? this : instance;

    public float speed;

    [Header("Interface")]
    [SerializeField] private GameObject interface_obj;
    [SerializeField] private TextMeshProUGUI points_txt;
    [SerializeField] private TextMeshProUGUI coins_txt;

    [HideInInspector] public int points;
    private float ponto;

    [Header("GameOver")]
    [SerializeField] private GameObject gameOver_interface;
    [SerializeField] private TextMeshProUGUI txt_dead;

    public static bool gamestart { get; set; }

    [Header("Fim")]
    [SerializeField] private GameObject prefab_fim;
    [SerializeField] private Transform position_fim;

    [HideInInspector] public int coins;

    void Start()
    {
        points = 0;
        ponto = 0;

        gamestart = false;
    }

    void Update()
    {
        if (!GameController.dead && !GameController.victory)
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

    public void Victory()
    {
        Instantiate(prefab_fim, position_fim.position, Quaternion.identity);
        interface_obj.SetActive(false);
        GameController.current.Victory();
    }

    public void AddCoins()
    {
        coins++;
        coins_txt.text = coins.ToString("D5");
    }
}
