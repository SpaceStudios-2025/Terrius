using UnityEngine;

public class ObjetosFlutuantes : MonoBehaviour
{
    public float floatStrength = 1f;      // Intensidade do movimento no espaço
    public float moveFrequency = 1f;      // Frequência do movimento

    public float angleAmount = 15f;       // Ângulo máximo de rotação (Z)
    public float swingSpeed = 2f;         // Velocidade da rotação

    private Vector3 startPosition;
    private float offset;

    void Start()
    {
        startPosition = transform.position;
        offset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Movimento leve na posição (eixo X e Y)
        float x = Mathf.Sin(Time.time * moveFrequency + offset) * floatStrength;
        float y = Mathf.Cos(Time.time * moveFrequency + offset) * floatStrength;

        transform.position = startPosition + new Vector3(x, y, 0f);

        // Rotação suave no eixo Z (útil para 2D ou efeito visual)
        float zAngle = Mathf.Sin(Time.time * swingSpeed + offset) * angleAmount;
        transform.rotation = Quaternion.Euler(0f, 0f, zAngle);
    }
}
