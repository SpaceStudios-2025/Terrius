using MaskTransitions;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    public void Exit()
    {
        if (TransitionManager.Instance)
        {
            TransitionManager.Instance.LoadLevel("Menu");
        }
    }
}
