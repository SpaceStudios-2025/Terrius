using UnityEngine;

public class Asteroides : MonoBehaviour
{
    public float floatStrength = 0.2f;    // Intensidade do movimento de flutuação
    public float moveFrequency = 1f;      // Frequência do movimento

    public float angleAmount = 15f;       // Ângulo máximo de rotação (Z)
    public float swingSpeed = 2f;         // Velocidade da rotação

    private Vector3 startPosition;
    private float offset;

    void Start()
    {
        startPosition = transform.localPosition;
        offset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Movimento de flutuação suave em X e Y
        float x = Mathf.Sin(Time.time * moveFrequency + offset) * floatStrength;
        float y = Mathf.Cos(Time.time * moveFrequency + offset) * floatStrength;

        transform.localPosition = startPosition + new Vector3(x, y, 0f);

        // Rotação apenas no eixo Z (visualmente “para os lados” em 2D)
        float zAngle = Mathf.Sin(Time.time * swingSpeed + offset) * angleAmount;
        transform.localRotation = Quaternion.Euler(0f, 0f, zAngle);
    }
}
