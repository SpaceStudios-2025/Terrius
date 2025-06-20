using System.Collections;
using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI coins_txt;
    [SerializeField] private TextMeshProUGUI diamond_txt;
    [SerializeField] private TextMeshProUGUI pointsLevel_txt;
    [SerializeField] private TextMeshProUGUI nivel_txt;

    [Space]
    [SerializeField] private Image points_img;

    [Space]
    [SerializeField] private Animator anim_Player;
    [SerializeField] private TextMeshProUGUI name_txt;

    private int pointsMax;
    private int points;

    private int levels;
    private int coins;
    private int diamond;
    private int nivel = 1;

    public void LoadScene(string name)
    {
        TransitionManager.Instance.LoadLevel(name);
    }

    void Start()
    {
        points = GameController.current.PointsLevel;
        pointsMax = GameController.current.PointsLevelMax;

        Load();

        StartCoroutine(Coins());
        StartCoroutine(Diamond());
        StartCoroutine(PointsLevel());
        StartCoroutine(Nivels());

        var gc = GameController.current;
        gc.Load();

        if (gc.space) anim_Player.runtimeAnimatorController = gc.genero ? gc.personagens[gc.index].anim_Space_female : gc.personagens[gc.index].anim_Space_male;
        else anim_Player.runtimeAnimatorController = gc.genero ? gc.personagens[gc.index].anim_normal_female : gc.personagens[gc.index].anim_normal_male;

        name_txt.text = gc.genero ? gc.personagens[gc.index].name_female : gc.personagens[gc.index].name_male;

        anim_Player.SetInteger("transition", 2);
    }

    IEnumerator Coins()
    {
        for (coins = 0; coins <= GameController.current.Coins; coins++)
        {
            coins_txt.text = coins.ToString("D4");
            yield return new WaitForSeconds(.005f);
        }
    }

    IEnumerator Diamond()
    {
        for (diamond = 0; diamond <= GameController.current.Diamond; diamond++)
        {
            diamond_txt.text = diamond.ToString("D4");
            yield return new WaitForSeconds(.005f);
        }
    }

    IEnumerator PointsLevel()
    {
        for (levels = 0; levels <= points; levels++)
        {
            pointsLevel_txt.text = levels.ToString("D2") + "/" + pointsMax.ToString("D2");
            points_img.fillAmount = (float)levels / pointsMax;
            yield return new WaitForSeconds(.005f);
        }
    }

    IEnumerator Nivels()
    {
        for (nivel = 1; nivel <= GameController.current.Nivel; nivel++)
        {
            nivel_txt.text = nivel.ToString();
            yield return new WaitForSeconds(.07f);
        }
    }

    void Load()
    {
        pointsLevel_txt.text = levels.ToString("D2") + "/" + pointsMax.ToString("D2");
        points_img.fillAmount = levels / pointsMax;
        nivel_txt.text = nivel.ToString();
        coins_txt.text = coins.ToString("D4");
        diamond_txt.text = diamond.ToString("D4");
    }
}
