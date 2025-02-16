using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.AI;

public class VignetteController : MonoBehaviour
{
    [Header("Post-Processing Settings")]
    public Volume postProcessVolume;
    private Vignette vignette;

    [Header("Vignette Intensity Settings")]
    public float minIntensity = 0.0f;
    public float maxIntensity = 0.5f;
    public float intensitySmoothSpeed = 2.0f;

    [Header("Movement Settings")]
    public float speedThreshold = 0.1f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        if (postProcessVolume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.Override(minIntensity);
        }
        else
        {
            Debug.LogWarning("Vignette override not found in the Volume profile.");
        }
    }

    void Update()
    {
        if (vignette == null) return;
        
        float speed = agent.velocity.magnitude;
        float targetIntensity = speed > speedThreshold ? maxIntensity : minIntensity;
        
        float newIntensity = Mathf.Lerp(vignette.intensity.value, targetIntensity, Time.deltaTime * intensitySmoothSpeed);
        vignette.intensity.Override(newIntensity);
    }
}