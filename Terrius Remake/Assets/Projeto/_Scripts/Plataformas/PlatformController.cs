using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float speed;
    private bool spawn;
    void LateUpdate()
    {
        if (!FindFirstObjectByType<CharacterController>().Dead())
        {
            transform.position += (Vector3)Vector2.left * speed * Time.deltaTime;

            if (transform.position.x < PlatformGenerator.current.posInitSpawn && !spawn)
            {
                PlatformGenerator.current.GeneratorPlatformer();
                spawn = true;
            }

            if (transform.position.x < PlatformGenerator.current.posDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}