using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private float[] posicoesY = { -0.323f, -0.627f, -0.946f };

    [SerializeField] private float delay_min = 1f;
    [SerializeField] private float delay_max = 3f;

    private float delay;
    private bool spawn = true;

    public float max_x;

    [Header("Obstacles")]
    [SerializeField] private GameObject obj_obstacle;
    [SerializeField] private GameObject obstaculo_alto;

    [Header("Obstaculos Alto")]
    [SerializeField] private float[] posicoesY_Alto = { -0.323f, -0.627f, -0.946f };

    [SerializeField] private float delay_alto_min = 1.5f;
    [SerializeField] private float delay_alto_max = 4f;

    [Header("Coins")]
    [SerializeField] private float delay_coins_min;
    [SerializeField] private float delay_coins_max;
    [SerializeField] private GameObject coin_prefab;

    private bool coins = true;
    private float delay_coins;

    private float delay_alto;
    private bool spawnAlto = true;


    void Start()
    {
        delay = Delay(delay_min, delay_max);
        StartCoroutine(SpawnObstacles());

        delay_coins = Delay(delay_coins_min, delay_coins_max);
        delay_alto = Delay(delay_alto_min, delay_alto_max);
    }

    IEnumerator SpawnObstacles()
    {
        if (PlanetaController.gamestart)
        {
            CreateObstacle();

            spawn = false;
            yield return new WaitForSeconds(delay);
            spawn = true;

            delay = Delay(delay_min, delay_max);
        }
    }

    IEnumerator SpawnObstaclesAlto()
    {
        CreateObstacleAlto();

        spawnAlto = false;
        yield return new WaitForSeconds(delay_alto);
        spawnAlto = true;

        delay_alto = Delay(delay_alto_min, delay_alto_max);
    }

    IEnumerator SpawnCoins()
    {
        CreateCoins();

        coins = false;
        yield return new WaitForSeconds(delay_coins);
        coins = true;

        delay_coins = Delay(delay_coins_min, delay_coins_max);
    }

    void LateUpdate()
    {
        if (!GameController.dead && !GameController.victory)
        {
            if (spawn) StartCoroutine(SpawnObstacles());
            if (spawnAlto) StartCoroutine(SpawnObstaclesAlto());
            if (coins) StartCoroutine(SpawnCoins());
        }
    }

    void CreateCoins()
    {
        int y = Random.Range(0, posicoesY.Length);
        var pos = new Vector3(transform.position.x, posicoesY[y]);

        var obstacle = Instantiate(coin_prefab, pos, Quaternion.identity);
        obstacle.GetComponent<SpriteRenderer>().sortingOrder = y + 1;

        obstacle.GetComponent<Obstacle>().platform = y;
    }

    void CreateObstacle()
    {
        int y = Random.Range(0, posicoesY.Length);
        var pos = new Vector3(transform.position.x, posicoesY[y]);

        var obstacle = Instantiate(obj_obstacle, pos, Quaternion.identity);
        obstacle.GetComponent<SpriteRenderer>().sortingOrder = y + 1;

        obstacle.GetComponent<Obstacle>().platform = y;

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
        int y = Random.Range(0, posicoesY_Alto.Length);
        var pos = new Vector3(transform.position.x, posicoesY_Alto[y]);

        var obstacle = Instantiate(obstaculo_alto, pos, Quaternion.identity);
        obstacle.GetComponent<SpriteRenderer>().sortingOrder = y + 1;

        obstacle.GetComponent<Obstacle>().platform = y;

        var obstaculo = PlatformGenerator.current.MapaValue(PlatformGenerator.mapa).ObstaculosAlto;
        obstacle.GetComponent<Animator>().runtimeAnimatorController = obstaculo[Random.Range(0, obstaculo.Count)];

        AtualizarCollider(obstacle);
    }

    float Delay(float min, float max)
    {
        return Random.Range(min, max);
    }
}

public enum Obstaculos
{
    obstaculoBaixo,
    obstaculoAlto,
    Coin
}
