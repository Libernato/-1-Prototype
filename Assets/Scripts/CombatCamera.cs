using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CombatCamera : MonoBehaviour
{

    public CinemachineCameraOffset combatCamera;

    void Start()
    {
        combatCamera.enabled = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            bool cameraEnebled = combatCamera.enabled;
            combatCamera.enabled = !cameraEnebled;
        }
    }
}
