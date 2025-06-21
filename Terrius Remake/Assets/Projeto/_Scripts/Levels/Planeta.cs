using UnityEngine;

public class Planeta : MonoBehaviour
{
    [Header("Informações do Planeta")]
    [SerializeField] string nome;
    [SerializeField] string describe;
    [SerializeField] string objetivo;
    [SerializeField] string scene;

    [SerializeField] bool block;

    public void OpenInterface()
    {
        if (!block)
        {
            var lc = FindFirstObjectByType<LevelsController>();

            lc.Planeta();
            lc.InfoPlaneta(nome, describe, objetivo, block, scene);
        }
    } 
}
