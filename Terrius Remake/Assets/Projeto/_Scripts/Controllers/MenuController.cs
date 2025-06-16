using MaskTransitions;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void LoadScene(string name)
    {
        TransitionManager.Instance.LoadLevel(name);
    }
}
