using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plataforma", menuName = "Plataformas/Plataforma", order = 1)]
public class Plataforma : ScriptableObject
{
    public string nome;    
    public int mapa;

    [Space]
    public string nome_dead_bioma;

    [Space]
    public List<GameObject> plataformas;
    public List<Sprite> obstaculos;

    [Space]
    public List<RuntimeAnimatorController> ObstaculosAlto;
}
