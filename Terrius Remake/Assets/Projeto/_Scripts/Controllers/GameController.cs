using System.Text;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController current { get; set; }

    void Awake() => current = !current ? this : current;

    [HideInInspector] public int Coins;
    [HideInInspector] public int points;

    [SerializeField] private TextMeshProUGUI txt_points;

    [HideInInspector] public float point = 0f;
    private float lastPoints;
    private StringBuilder stringBuilder = new StringBuilder(16); // tamanho inicial
    void Update()
    {
        point += Time.deltaTime;
        points = (int)point;

        if (points != lastPoints)
        {
            stringBuilder.Clear();
            stringBuilder.Append(points.ToString("0000"));
            stringBuilder.Append("m");

            txt_points.text = stringBuilder.ToString();
            lastPoints = points;
        }
    }

    public void Dead()
    {
        CharacterController.dead = true;
        FindFirstObjectByType<CharacterController>().Dead();
    }
}
