using System.Collections.Generic;
using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VestiarioController : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] private TextMeshProUGUI coins_Txt;
    [SerializeField] private TextMeshProUGUI diamonds_Txt;

    [SerializeField] private TextMeshProUGUI name_txt;

    [SerializeField] private Toggle toggle_astronauta;

    [Header("Personagens")]
    public List<Persons> personagens = new();

    [Header("Player")]
    [SerializeField] private Animator anim;
    [SerializeField] private Animator anim_shadow;


    private int indice = 0;

    void Start()
    {
        coins_Txt.text = GameController.current.Coins.ToString("D5");
        diamonds_Txt.text = GameController.current.Diamond.ToString("D4");

        Normal();
    }

    public void Voltar_Btn()
    {
        if (TransitionManager.Instance) TransitionManager.Instance.LoadLevel("Menu");
        else SceneManager.LoadScene("Menu");
    }

    public void ToggleAstronauta()
    {
        if (toggle_astronauta.isOn) Space();
        else Normal();
    }

    public void Space()
    {
        PersonSpace(indice);
        name_txt.text = personagens[indice].name;
    }

    public void Normal()
    {
        PersonNormal(indice);
        name_txt.text = personagens[indice].name;
    }

    void PersonNormal(int index)
    {
        anim.runtimeAnimatorController = personagens[index].anim_normal;
        anim_shadow.runtimeAnimatorController = personagens[index].anim_normal;
    }

    void PersonSpace(int index)
    {
        anim.runtimeAnimatorController = personagens[index].anim_Space;
        anim_shadow.runtimeAnimatorController = personagens[index].anim_Space;
    }
}

[System.Serializable]
public class Persons
{
    public string name;
    public RuntimeAnimatorController anim_normal;
    public RuntimeAnimatorController anim_Space;
}
