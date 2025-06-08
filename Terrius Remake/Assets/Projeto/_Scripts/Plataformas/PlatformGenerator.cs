using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public static PlatformGenerator current;

    void Awake() => current = !current ? this : current;

    [Header("Plataformas")]
    public List<Plataforma> plataformas = new();

    public static int mapa;

    [Space]
    [SerializeField] private float minTimer;
    [SerializeField] private float maxTimer;

    private float timerPlatform;
    private bool spawnPlatform;

    [Header("Positions")]
    public Vector2 posSpawn;
    public float posDestroy;
    public float posInitSpawn;

    private bool generate;

    [Header("Interface")]
    [SerializeField] private TextMeshProUGUI biome_name; 

    public void GeneratorPlatformer()
    {
        if (!FindFirstObjectByType<CharacterController>().Dead())
        {
            if (!spawnPlatform)
            {
                mapa = (int)Rand(0, plataformas.Count);
                timerPlatform = Rand(minTimer, maxTimer);

                StartCoroutine(Spawn());
            }

            if (spawnPlatform) { Platform(); }
        }
    }

    void Platform()
    {
        if (!generate)
        {
            Instantiate(MapaValue(mapa).plataformas[(int)Rand(0, MapaValue(mapa).plataformas.Count)], posSpawn, Quaternion.identity);
            StartCoroutine(Generator());
        }
    }

    IEnumerator Generator()
    {
        generate = true;
        yield return new WaitForSeconds(.5f);
        generate = false;
    }

    IEnumerator Spawn()
    {
        spawnPlatform = true;
        yield return new WaitForSeconds(timerPlatform);
        spawnPlatform = false;
    }

    float Rand(float min, float max)
    {
        return Random.Range(min, max);
    }

    public Plataforma MapaValue(int value)
    {
        foreach (var plat in plataformas)
        {
            if (plat.mapa == value)
            {
                biome_name.text = plat.nome;
                return plat;
            }
        }

        return null;
    }
}
