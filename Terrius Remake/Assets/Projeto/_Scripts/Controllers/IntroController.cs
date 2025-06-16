using UnityEngine;
using MaskTransitions;

public class IntroController : MonoBehaviour
{
    public void Iniciar(string scene)
    {
        TransitionManager.Instance.LoadLevel(scene);
    }
}
