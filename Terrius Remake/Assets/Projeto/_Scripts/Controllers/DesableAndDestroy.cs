using UnityEngine;

public class DesableAndDestroy : MonoBehaviour
{
    public void Desable()
    {
        gameObject.SetActive(false);
    }

    public void Destruir()
    {
        gameObject.SetActive(true);
    }
}
