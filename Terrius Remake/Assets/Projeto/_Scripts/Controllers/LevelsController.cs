using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    [SerializeField] private Image nivel_img;
    [SerializeField] private TextMeshProUGUI nivel_txt;
    [SerializeField] private TextMeshProUGUI level_txt;

    [SerializeField] private TextMeshProUGUI coins_txt;

    [SerializeField] private GameObject info_planeta;

    [Header("Info Planetas")]
    [SerializeField] private TextMeshProUGUI nome_planeta_txt;
    [SerializeField] private TextMeshProUGUI describe_planeta_txt;
    [SerializeField] private TextMeshProUGUI objetivo_txt;

    [SerializeField] private Button btn_viajar;

    private string planeta;

    void Start()
    {
        nivel_img.fillAmount = (float)GameController.current.PointsLevel / GameController.current.PointsLevelMax;
        nivel_txt.text = GameController.current.Nivel.ToString();
        level_txt.text = GameController.current.PointsLevel.ToString("D2") + "/" + GameController.current.PointsLevelMax;

        coins_txt.text = GameController.current.Coins.ToString("D4");
    }

    public void Planeta()
    {
        info_planeta.SetActive(true);
    }

    public void InfoPlaneta(string nome, string describe, string objetivo, bool look, string scene)
    {
        btn_viajar.onClick.RemoveAllListeners();

        nome_planeta_txt.text = nome;
        describe_planeta_txt.text = describe;
        objetivo_txt.text = objetivo;

        if (look) btn_viajar.interactable = false;
        else
        {
            planeta = scene;
            btn_viajar.interactable = true;
            btn_viajar.onClick.AddListener(() => Viajar());
        }
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

    public void Viajar()
    {
        TransitionManager.Instance.LoadLevel(planeta);
    }
}
