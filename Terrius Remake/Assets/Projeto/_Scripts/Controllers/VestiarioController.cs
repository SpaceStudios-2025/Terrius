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

    [Header("Comprar Button")]
    [SerializeField] private Sprite btn_comprar_sprite;
    [SerializeField] private Sprite btn_normal_male_sprite;
    [SerializeField] private Sprite btn_normal_famale_sprite;

    [Header("Payment")]
    [SerializeField] private GameObject obj_payment;
    [SerializeField] private Image icon_payment;
    [SerializeField] private TextMeshProUGUI value_payment;

    public List<Sprite> paymentIcon = new();

    private int indice = 0;
    private bool space;

    private bool genero; //True = female, False = male

    GameController gc;

    void Start()
    {
        gc = GameController.current;

        gc.Load();
        Load();

        SelectButton();
        Person();
    }

    void Load()
    {
        toggle_astronauta.isOn = gc.space;
        toggle_genero.isOn = gc.genero;

        coins_Txt.text = gc.Coins.ToString("D5");
        diamonds_Txt.text = gc.Diamond.ToString("D5");

        indice = gc.index;
        space = gc.space;

        genero = gc.genero;
    }

    public void Voltar_Btn()
    {
        if (TransitionManager.Instance) TransitionManager.Instance.LoadLevel("Menu");
        else SceneManager.LoadScene("Menu");
    }

    #region Astronauta e Genero
    public void ToggleAstronauta()
    {
        space = toggle_astronauta.isOn;
        Person();
        PlayerPrefs.SetInt("Space", space ? 1 : 0);
    }

    public void ToggleGenero()
    {
        genero = toggle_genero.isOn;
        Person();
        SelectButton();
    }

    void Person()
    {
        if (!Player().silhouette)
        {
            anim.runtimeAnimatorController = GenAst();
            anim_shadow.runtimeAnimatorController = GenAst();

            name_txt.text = genero ? Player().name_female : Player().name_male;
        }
        else
        {
            anim.runtimeAnimatorController = gc.anim_silhouette;
            anim_shadow.runtimeAnimatorController = gc.anim_silhouette;

            name_txt.text = "Desconhecido!";
        }


        Anim();
    }

    Persons Player()
    {
        return gc.personagens[indice];
    }

    RuntimeAnimatorController GenAst()
    {
        if (genero)
        {
            if (space) return Player().anim_Space_female;
            else return Player().anim_normal_female;
        }
        else
        {
            if (space) return Player().anim_Space_male;
            else return Player().anim_normal_male;
        }
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
            Person();
        }
        else
        {
            indice = 0;
            Person();
        }

        SelectButton();
    }

    public void LeftButton()
    {
        if (indice > 0)
        {
            indice--;
            Person();
        }
        else
        {
            indice = gc.personagens.Count - 1;
            Person();
        }

        SelectButton();
    }

    #endregion

    public void Selecionar()
    {
        if (!Player().blocked)
        {
            if (indice != gc.index || genero != gc.genero)
            {
                PlayerPrefs.SetInt("Person", indice);
                PlayerPrefs.SetInt("Genero", genero ? 1 : 0);
                gc.Load();

                InteractableSelect();
            }
        }
        else
        {
            if (!Player().silhouette)
            {
                if (Player().payment == 0)
                {
                    if (gc.PaymentDiamond(Player().value))
                        CompraRealizada();
                    else print("Compra não realizada!");
                }
                else if (Player().payment == (Payment)1)
                {
                    if (gc.PaymentDiamond(Player().value))
                        CompraRealizada();
                    else print("Compra não realizada!");
                }
            }
        }
    }

    void CompraRealizada()
    {
        print("Compra Realizada Com Sucesso!");
        Player().blocked = false;
        PlayerPrefs.SetInt(Player().id + "desblock", 1);
        SelectButton();
    }

    void SelectButton()
    {
        if (!Player().blocked)
        {
            if (indice == gc.index && gc.genero == genero) InteractableSelect();
            else if (indice != gc.index || gc.genero != genero) ActiveSelect();
        }
        else
        {
            icon_payment.sprite = paymentIcon[(int)Player().payment];
            value_payment.text = Player().value.ToString("D4");

            DesblockSelect();
        }

        toggle_astronauta.interactable = !Player().silhouette;
        toggle_genero.interactable = !Player().silhouette;
        
        obj_payment.SetActive(Player().blocked);
    }

    void InteractableSelect()
    {
        btn_selecionar.interactable = false;
        btn_selecionar.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SELECIONADO";
    }

    void ActiveSelect()
    {
        btn_selecionar.interactable = true;
        
        if (!genero)
            btn_selecionar.GetComponent<Image>().sprite = btn_normal_male_sprite;
        else
            btn_selecionar.GetComponent<Image>().sprite = btn_normal_famale_sprite;

        btn_selecionar.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SELECIONAR";
    }

    void DesblockSelect()
    {
        btn_selecionar.interactable = true;
        btn_selecionar.GetComponent<Image>().sprite = btn_comprar_sprite;
        btn_selecionar.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "DESBLOQUEAR";
    }
}

// ||