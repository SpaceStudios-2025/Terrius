using UnityEngine;
using UnityEngine.UI;

public class Tv : MonoBehaviour
{
    private bool enableTv;

    public void Televisao()
    {
        enableTv = !enableTv;
        GetComponent<Animator>().SetTrigger(enableTv ? "ligar" : "desligar");
    }
}
