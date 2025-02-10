using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimation : MonoBehaviour
{
    private InputDevice controller;
    Animator animator;
    private float thumbValue;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        InitializeController();
    }
    
    void Update()
    {
        if (!controller.isValid)
        {
            InitializeController();
        }
        else
        {
            UpdateAnimation();
        }
    }

    void InitializeController()
    {
        XRNode node = GetComponentInParent<XRController>().controllerNode;
        controller = InputDevices.GetDeviceAtXRNode(node);
    }

    void UpdateAnimation()
    {
        if (controller.TryGetFeatureValue(CommonUsages.trigger, out float index))
        {
            animator.SetFloat("IndexBend", index);
        }
        
        if (controller.TryGetFeatureValue(CommonUsages.grip, out float finger))
        {
            animator.SetFloat("FingerBend", finger);
        }
    }
}
