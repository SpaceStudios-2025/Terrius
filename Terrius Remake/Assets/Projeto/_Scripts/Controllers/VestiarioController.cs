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

    [Header("Player")]
    [SerializeField] private Animator anim;
    [SerializeField] private Animator anim_shadow;


    private int indice = 0;
    private bool space;

    void Start()
    {
        GameController.current.Load();
        toggle_astronauta.isOn = GameController.current.space;

        coins_Txt.text = GameController.current.Coins.ToString("D5");
        diamonds_Txt.text = GameController.current.Diamond.ToString("D4");

        indice = GameController.current.index;
        space = GameController.current.space;

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
        if (toggle_astronauta.isOn) Space();
        else Normal();
    }

    public void Space()
    {
        PersonSpace(indice);
        name_txt.text = GameController.current.personagens[indice].name;

        PlayerPrefs.SetInt("Space", 1);
        space = true;
    }

    public void Normal()
    {
        PersonNormal(indice);
        name_txt.text = GameController.current.personagens[indice].name;

        PlayerPrefs.SetInt("Space", 0);
        space = false;
    }

    void PersonNormal(int index)
    {
        anim.runtimeAnimatorController = GameController.current.personagens[index].anim_normal;
        anim_shadow.runtimeAnimatorController = GameController.current.personagens[index].anim_normal;

        anim.SetInteger("transition", 2);
        anim_shadow.SetInteger("transition", 2);
    }

    void PersonSpace(int index)
    {
        anim.runtimeAnimatorController = GameController.current.personagens[index].anim_Space;
        anim_shadow.runtimeAnimatorController = GameController.current.personagens[index].anim_Space;

        anim.SetInteger("transition", 2);
        anim_shadow.SetInteger("transition", 2);
    }


    public void RightButton()
    {
        if (indice < GameController.current.personagens.Count - 1)
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
            indice = GameController.current.personagens.Count - 1;
            OtherPlayer();
        }

        SelectButton();
    }

    public void Selecionar()
    {
        if (indice != GameController.current.index)
        {
            PlayerPrefs.SetInt("Person", indice);
            GameController.current.Load();

            InteractableSelect();
        }
    }

    void SelectButton()
    {
        if (indice == GameController.current.index) InteractableSelect();
        else ActiveSelect();
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