using UnityEngine;

public class Lua : MonoBehaviour
{
    public float speed;

    void LateUpdate(){
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
