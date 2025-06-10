using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private float delay_min = 1f;
    [SerializeField] private float delay_max = 3f;

    private float delay;
    private bool spawn = true;

    public float max_x;

    [Header("Obstacles")]
    [SerializeField] private GameObject obj_obstacle;
    [SerializeField] private GameObject obstaculo_alto;

    [Header("Obstaculos Alto")]
    [SerializeField] private float y_min;
    [SerializeField] private float y_max;

    [SerializeField] private float delay_alto_min = 1.5f;
    [SerializeField] private float delay_alto_max = 4f;

    private float delay_alto;
    private bool spawnAlto = true;

    void Start()
    {
        delay = Delay(delay_min, delay_max);
        StartCoroutine(SpawnObstacles());

        delay_alto = Delay(delay_alto_min, delay_alto_max);
    }

    IEnumerator SpawnObstacles()
    {
        CreateObstacle();

        spawn = false;
        yield return new WaitForSeconds(delay);
        spawn = true;

        delay = Delay(delay_min, delay_max);
    }

    IEnumerator SpawnObstaclesAlto()
    {
        CreateObstacleAlto();

        spawnAlto = false;
        yield return new WaitForSeconds(delay_alto);
        spawnAlto = true;

        delay_alto = Delay(delay_alto_min, delay_alto_max);
    }

    void LateUpdate()
    {
        if (!FindFirstObjectByType<CharacterController>().Dead())
        {
            if (spawn) StartCoroutine(SpawnObstacles());

            if (spawnAlto) StartCoroutine(SpawnObstaclesAlto());
        }
    }

    void CreateObstacle()
    {
        var obstacle = Instantiate(obj_obstacle, transform.position, Quaternion.identity);

        var obstaculo = PlatformGenerator.current.MapaValue(PlatformGenerator.mapa).obstaculos;
        obstacle.GetComponent<SpriteRenderer>().sprite = obstaculo[Random.Range(0, obstaculo.Count)];

        AtualizarCollider(obstacle);
    }

    void AtualizarCollider(GameObject obj)
    {
        var pc = obj.GetComponent<PolygonCollider2D>();
        if (pc != null) Destroy(pc);

        obj.AddComponent<PolygonCollider2D>(); // único ponto de AddComponent
        obj.GetComponent<PolygonCollider2D>().isTrigger = true;
    }

    void CreateObstacleAlto()
    {
        var pos = new Vector3(transform.position.x, Random.Range(y_min, y_max));

        var obstacle = Instantiate(obstaculo_alto, pos, Quaternion.identity);

        var rand = Random.Range(0, 2);
        List<RuntimeAnimatorController> obstaculo;
        if (rand == 1)
        {
            obstaculo = PlatformGenerator.current.MapaValue(PlatformGenerator.mapa).ObstaculosAlto.left;
        }
        else
        {
            obstaculo = PlatformGenerator.current.MapaValue(PlatformGenerator.mapa).ObstaculosAlto.right;
            obstacle.GetComponent<SpriteRenderer>().flipX = true;
        }

        obstacle.GetComponent<Animator>().runtimeAnimatorController = obstaculo[Random.Range(0, obstaculo.Count)];

        AtualizarCollider(obstacle);
    }

    float Delay(float min, float max)
    {
        return Random.Range(min, max);
    }
}

[System.Serializable]
public class ObstaculoAlto
{
    public List<RuntimeAnimatorController> left;
    public List<RuntimeAnimatorController> right;
}
