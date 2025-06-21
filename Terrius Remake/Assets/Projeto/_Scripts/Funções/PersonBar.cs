using UnityEngine;

public class PersonBar : MonoBehaviour
{
    public RectTransform personagem; // A Image ou objeto do personagem
    public float valorAtual = 0f;    // Valor atual (entre 0 e valorMaximo)
    public float valorMaximo = 100f; // Valor máximo

    public float pontoMinX = -513f;
    public float pontoMaxX = 513f;

    void Update()
    {
        // Garante que valorAtual esteja no intervalo válido
        valorAtual = Mathf.Clamp(valorAtual, 0f, valorMaximo);

        valorAtual = FindFirstObjectByType<PlanetaController>().points;

        // Calcula a posição X com base na proporção
        float proporcao = valorAtual / valorMaximo;
        float novaPosX = Mathf.Lerp(pontoMinX, pontoMaxX, proporcao);

        // Atualiza a posição do personagem (apenas eixo X)
        Vector2 pos = personagem.anchoredPosition;
        pos.x = novaPosX;
        personagem.anchoredPosition = pos;
    }
}
