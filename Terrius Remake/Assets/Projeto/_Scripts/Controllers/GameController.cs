using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController current { get; set; }

    void Awake()
    {
        current = !current ? this : current;

        Load();
    }

    [HideInInspector] public int Coins;
    [HideInInspector] public int Points;
    [HideInInspector] public int PointsLevel;
    [HideInInspector] public int Nivel;
    [HideInInspector] public int Diamond; 

    [HideInInspector] public int PointsLevelMax = 240;
 
    public void Save()
    {
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.SetInt("Diamond", Diamond);
        PlayerPrefs.SetInt("Points", Points);
        PlayerPrefs.SetInt("Nivel", Nivel);
        PlayerPrefs.SetInt("PointsLevel", PointsLevel);
    }

    public void Load()
    {
        Coins = PlayerPrefs.GetInt("Coins", 200);
        Diamond = PlayerPrefs.GetInt("Diamond", 250);
        Points = PlayerPrefs.GetInt("Points", 100);
        Nivel = PlayerPrefs.GetInt("Nivel", 5);
        PointsLevel = PlayerPrefs.GetInt("PointsLevel", 200);
    }

    public void Dead()
    {
        CharacterController.dead = true;
        FindFirstObjectByType<CharacterController>().Dead();
    }
}
