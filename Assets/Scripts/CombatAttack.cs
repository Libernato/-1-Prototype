using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAttack : MonoBehaviour
{

    private float _timePassed;
    private float _clipLength;
    private float _clipSpeed;
    private bool _attack = false;

    public CombatModeController combatModeController;
    public Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        bool combatMode = combatModeController.inCombatMode;
        if(Input.GetKeyDown(KeyCode.Mouse0) && combatMode)
        {
            _attack = true;
            _timePassed = 0;
            animator.SetTrigger("Attacking");

            _clipLength = animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
            _clipSpeed = animator.GetCurrentAnimatorStateInfo(1).speed;

            if(_timePassed >= _clipLength / _clipSpeed && _attack)
            {
                animator.SetTrigger("Attacking");
            }
        }
    }
}
