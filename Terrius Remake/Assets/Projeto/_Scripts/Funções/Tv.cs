using UnityEngine;
using UnityEngine.UI;

public class Tv : MonoBehaviour
{
    [SerializeField] private Sprite tvLigada;
    [SerializeField] private Sprite tvDesligada;

    private bool enableTv;

    public void Televisao()
    {
        enableTv = !enableTv;
        GetComponent<Image>().sprite = enableTv ? tvLigada : tvDesligada;
    }
}
