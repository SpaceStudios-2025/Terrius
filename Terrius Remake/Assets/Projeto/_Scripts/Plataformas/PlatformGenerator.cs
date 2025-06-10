using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class PlatformGenerator : MonoBehaviour
{
    public static PlatformGenerator current { get; set; }

    [Header("Plataformas")]
    public List<Plataforma> plataformas = new();

    public static int mapa { get; set; }

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

    private CharacterController characterController;

    void Awake()
    {
        current = !current ? this : current;
    }
    
    void Start()
    {
        characterController = FindFirstObjectByType<CharacterController>();
    }

    public void GeneratorPlatformer()
    {
        if (!characterController.Dead())
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
            Instantiate(MapaValue(mapa).plataformas[(int)Rand(0, MapaValue(mapa).plataformas.Count)],posSpawn,Quaternion.identity);
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
