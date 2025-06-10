using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadScene(string name)
    {
        TransitionManager.Instance.LoadLevel(name);
    }
}
