using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    public Image nivel_img;
    public TextMeshProUGUI nivel_txt;
    public TextMeshProUGUI level_txt;

    public TextMeshProUGUI coins_txt;

    void Start()
    {
        nivel_img.fillAmount = (float)GameController.current.PointsLevel / GameController.current.PointsLevelMax;
        nivel_txt.text = GameController.current.Nivel.ToString();
        level_txt.text = GameController.current.PointsLevel.ToString("D2") + "/" + GameController.current.PointsLevelMax;

        coins_txt.text = GameController.current.Coins.ToString("D4");
    }

    public void Planeta(string planeta)
    {
        TransitionManager.Instance.LoadLevel(planeta);
    }

    public void Voltar()
    {
        if (TransitionManager.Instance)
        {
            TransitionManager.Instance.LoadLevel("Menu");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
