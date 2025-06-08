using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [Header("Camera Shake")]
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;
    public float dampingSpeed = 1.0f;
    private Vector3 initialPosition;
    private float currentShakeDuration = 0f;

    [Header("Death Effect")]
    public bool isDying = false;
    public float deathZoomSpeed = 5f;
    public float targetFOV = 30f;
    private float defaultFOV;

    private Camera cam;

    void Start()
    {
        initialPosition = transform.localPosition;
        cam = GetComponent<Camera>();
        if (cam != null)
        {
            defaultFOV = cam.fieldOfView;
        }
    }

    void Update()
    {
        // Camera Shake
        if (currentShakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            transform.localPosition = initialPosition;
        }

        // Death Effect (Zoom dramático)
        if (isDying && cam != null)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * deathZoomSpeed);
        }
    }

    // 🎭 Ativa o shake
    public void ShakeCamera(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        currentShakeDuration = duration;
    }

    // ☠️ Ativa efeito de morte
    public void TriggerDeathEffect()
    {
        isDying = true;
        ShakeCamera(0.3f, 0.1f); // Aquele tremor final do colapso
    }

    // 🧙 Resetar a câmera (opcional)
    public void ResetCamera()
    {
        isDying = false;
        currentShakeDuration = 0;
        transform.localPosition = initialPosition;
        if (cam != null)
        {
            cam.fieldOfView = defaultFOV;
        }
    }
}
