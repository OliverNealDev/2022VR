using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private InputDevice leftController;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        InitializeLeftController();
    }

    void Update()
    {
        if (!leftController.isValid)
        {
            InitializeLeftController();
            return;
        }
        
        if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 inputAxis))
        {
            if (inputAxis.sqrMagnitude > 0.01f)
            {
                Vector3 worldDirection = transform.forward * inputAxis.y + transform.right * inputAxis.x;
                worldDirection.y = 0;
                worldDirection.Normalize();
                
                Vector3 newDestination = transform.position + worldDirection * agent.speed * Time.deltaTime;
                
                agent.SetDestination(newDestination);
            }
        }
    }

    void InitializeLeftController()
    {
        List<InputDevice> leftHandedDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandedDevices);
        if (leftHandedDevices.Count > 0)
        {
            leftController = leftHandedDevices[0];
        }
    }
}
