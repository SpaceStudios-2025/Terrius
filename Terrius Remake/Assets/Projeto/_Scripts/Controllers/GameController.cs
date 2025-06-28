using System.Collections;
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
        LoadPersons();

        dead = false;
        victory = false;

        audioSource.GetComponent<AudioSource>();
    }
    [HideInInspector] public int Coins;
    [HideInInspector] public int Points;
    [HideInInspector] public int PointsLevel;
    [HideInInspector] public int Nivel;
    [HideInInspector] public int Diamond;

    private bool delayCompra;

    [HideInInspector] public int PointsLevelMax = 240;

    [HideInInspector] public bool space;
    [HideInInspector] public int index;

    [HideInInspector] public bool genero;

    [Header("Personagens")]
    public List<Persons> personagens = new();
    public RuntimeAnimatorController anim_silhouette;

    [Header("Statics")]
    public static bool victory { get; set; }
    public static bool dead { get; set; }

    public static int trilha { get; set; }

    public AudioSource audioSource;


    void LoadPersons()
    {
        foreach (var person in personagens)
        {
            if (PlayerPrefs.HasKey(person.id + "desblock"))
            {
                person.blocked = false;
            }
        }
    }

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
        dead = true;
        FindFirstObjectByType<CharacterController>().Dead();
    }

    public void Victory()
    {
        victory = true;
        FindFirstObjectByType<CharacterController>().gameObject.SetActive(false);
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    #region Payment
    public bool PaymentMoney(int value)
    {
        if (Coins >= value && !delayCompra)
        {
            if (!delayCompra)
            {
                Coins -= value;
                PlayerPrefs.SetInt("Coins", Coins);

                StartCoroutine(PaymentDelay());

                return true;
            }
            return false;
        }
        else return false;
    }

    public bool PaymentDiamond(int value)
    {
        if (Diamond >= value)
        {
            if (!delayCompra)
            {
                Diamond -= value;
                PlayerPrefs.SetInt("Diamond", Diamond);

                StartCoroutine(PaymentDelay());

                return true;
            }

            return false;
        }
        else return false;
    }

    IEnumerator PaymentDelay()
    {
        delayCompra = true;
        yield return new WaitForSeconds(1f);
        delayCompra = false;
    }
    #endregion
}

[System.Serializable]
public class Persons
{
    [Header("Identificador")]
    public string id;
    [Space]

    public string name_male;
    public RuntimeAnimatorController anim_normal_male;
    public RuntimeAnimatorController anim_Space_male;

    [Space]

    public string name_female;
    public RuntimeAnimatorController anim_normal_female;
    public RuntimeAnimatorController anim_Space_female;

    [Space]
    public bool blocked;
    public bool silhouette;

    [Space]
    [Header("Buy")]
    public Payment payment;
    public int value;
}

public enum Payment: int{
    diamond = 0,
    money = 1,
}
