using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatModeController : MonoBehaviour
{

    public bool inCombatMode = false;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            ToggleCombatMode();
        }
    }

    private void ToggleCombatMode()
    {
        inCombatMode = !inCombatMode;

        if(inCombatMode)
        {
            EnterCombatMode();
        }
        else
        {
            ExitCombatMode();
        }

    }

    private void EnterCombatMode()
    {
        _animator.SetTrigger("DrawWeapon");
    }

    private void ExitCombatMode()
    {
        _animator.SetTrigger("SheathWeapon");
    }

}
