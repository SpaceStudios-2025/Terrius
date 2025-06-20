using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController current { get; set; }

    void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);

        Load();
    }

    [HideInInspector] public int Coins;
    [HideInInspector] public int Points;
    [HideInInspector] public int PointsLevel;
    [HideInInspector] public int Nivel;
    [HideInInspector] public int Diamond;

    [HideInInspector] public int PointsLevelMax = 240;

    [HideInInspector] public bool space;
    [HideInInspector] public int index;

    [HideInInspector] public bool genero;

    [Header("Personagens")]
    public List<Persons> personagens = new();

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

        space = PlayerPrefs.GetInt("Space", 0) == 1;
        index = PlayerPrefs.GetInt("Person", 0);

        genero = PlayerPrefs.GetInt("Genero", 0) == 1;
    }

    public void Dead()
    {
        CharacterController.dead = true;
        FindFirstObjectByType<CharacterController>().Dead();
    }
}

[System.Serializable]
public class Persons
{
    public string name_male;
    public RuntimeAnimatorController anim_normal_male;
    public RuntimeAnimatorController anim_Space_male;

    [Space]

    public string name_female;
    public RuntimeAnimatorController anim_normal_female;
    public RuntimeAnimatorController anim_Space_female;
}

