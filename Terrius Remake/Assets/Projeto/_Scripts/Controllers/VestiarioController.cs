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
    [SerializeField] private Button btn_selecionar;

    [SerializeField] private Toggle toggle_genero;

    [Header("Player")]
    [SerializeField] private Animator anim;
    [SerializeField] private Animator anim_shadow;

    private int indice = 0;
    private bool space;

    public bool genero; //True = female, False = male

    GameController gc;

    void Start()
    {
        gc = GameController.current;

        gc.Load();
        toggle_astronauta.isOn = gc.space;
        toggle_genero.isOn = gc.genero;

        coins_Txt.text = gc.Coins.ToString("D5");
        diamonds_Txt.text = gc.Diamond.ToString("D4");

        indice = gc.index;
        space = gc.space;

        genero = gc.genero;

        SelectButton();
        OtherPlayer();
    }

    public void OtherPlayer()
    {
        if (space) Space();
        else Normal();
    }

    public void Voltar_Btn()
    {
        if (TransitionManager.Instance) TransitionManager.Instance.LoadLevel("Menu");
        else SceneManager.LoadScene("Menu");
    }

    public void ToggleAstronauta()
    {
        space = toggle_astronauta.isOn;

        if (space) Space();
        else Normal();

        PlayerPrefs.SetInt("Space", space ? 1 : 0);
    }

    public void ToggleGenero()
    {
        genero = toggle_genero.isOn;

        if (toggle_astronauta.isOn) Space();
        else Normal();

        SelectButton();
    }

    public void Space()
    {
        PersonSpace(indice);
        name_txt.text = genero ? gc.personagens[indice].name_female : gc.personagens[indice].name_male;
    }

    public void Normal()
    {
        PersonNormal(indice);
        name_txt.text = genero ? gc.personagens[indice].name_female : gc.personagens[indice].name_male;
    }

    void PersonNormal(int index)
    {
        anim.runtimeAnimatorController = genero ? gc.personagens[index].anim_normal_female : gc.personagens[index].anim_normal_male;
        anim_shadow.runtimeAnimatorController = genero ? gc.personagens[index].anim_normal_female : gc.personagens[index].anim_normal_male;

        Anim();
    }

    void PersonSpace(int index)
    {
        anim.runtimeAnimatorController = genero ? gc.personagens[index].anim_Space_female : gc.personagens[index].anim_Space_male;
        anim_shadow.runtimeAnimatorController = genero ? gc.personagens[index].anim_Space_female : gc.personagens[index].anim_Space_male;

        Anim();
    }

    void Anim()
    {
        anim.SetInteger("transition", 2);
        anim_shadow.SetInteger("transition", 2);
    }

    public void RightButton()
    {
        if (indice < gc.personagens.Count - 1)
        {
            indice++;
            OtherPlayer();
        }
        else
        {
            indice = 0;
            OtherPlayer();
        }

        SelectButton();
    }

    public void LeftButton()
    {
        if (indice > 0)
        {
            indice--;
            OtherPlayer();
        }
        else
        {
            indice = gc.personagens.Count - 1;
            OtherPlayer();
        }

        SelectButton();
    }

    public void Selecionar()
    {
        if (indice != gc.index)
        {
            PlayerPrefs.SetInt("Person", indice);
            PlayerPrefs.SetInt("Genero", genero ? 1 : 0);
            gc.Load();

            InteractableSelect();
        }
    }

    void SelectButton()
    {
        if (indice == gc.index && gc.genero == genero) InteractableSelect();
        else if (indice != gc.index || gc.genero != genero) ActiveSelect();
    }

    void InteractableSelect()
    {
        btn_selecionar.interactable = false;
        btn_selecionar.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SELECIONADO";
    }

    void ActiveSelect()
    {
        btn_selecionar.interactable = true;
        btn_selecionar.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SELECIONAR";
    }
}

// ||