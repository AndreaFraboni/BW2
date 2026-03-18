using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationParamHandler : MonoBehaviour
{
    [SerializeField] private string _jumpName = "Jump";
    [SerializeField] private string _deathName = "Death";
  
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Jump()
    {
        _animator.SetTrigger(_jumpName);
    }

    public void Death()
    {
        _animator.SetTrigger(_deathName);
    }
}
