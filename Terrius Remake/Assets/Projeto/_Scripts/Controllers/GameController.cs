using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController current;
    void Awake() => current = !current ? this : current;

    [HideInInspector] public int Coins;
    [HideInInspector] public int points;

    [SerializeField] private TextMeshProUGUI txt_points;

    [HideInInspector] public float point = 0f;
    void Update()
    {
        point += Time.deltaTime;
        points = (int)point;
        txt_points.text = points.ToString("0000") + "m";
    }

    public void Dead()
    {
        CharacterController.dead = true;
        FindFirstObjectByType<CharacterController>().Dead();
    }
}
