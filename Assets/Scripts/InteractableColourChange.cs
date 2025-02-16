using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class InteractableColourChange : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Material defaultMaterial;
    private Animation anim;

    [SerializeField] private Material hoverMaterial;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;

        anim = GetComponent<Animation>();
    }

    public void OnStartHovered(HoverEnterEventArgs args)
    {
        meshRenderer.material = hoverMaterial;
        anim.Play();
    }

    public void OnEndHovered(HoverExitEventArgs args)
    {
        meshRenderer.material = defaultMaterial;
        if (anim.isPlaying)
        {
            anim.Stop();
        }
    }
}
