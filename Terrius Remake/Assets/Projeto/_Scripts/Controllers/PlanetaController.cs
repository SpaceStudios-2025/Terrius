using TMPro;
using UnityEngine;

public class PlanetaController : MonoBehaviour
{
    public float speed;

    [Header("Interface")]
    [SerializeField] private TextMeshProUGUI points_txt;
    [HideInInspector] public int points;
    private float ponto;

    void Start()
    {
        points = 0;
        ponto = 0;
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
}
