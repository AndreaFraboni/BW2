using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationParamHandler : MonoBehaviour
{
    [SerializeField] private string _jumpName = "Jump";
    [SerializeField] private string _deathName = "Death";
    [SerializeField] private string _selectedName = "IsSelected";
    [SerializeField] private string _changeLaneRName = "ChangeLaneR";
    [SerializeField] private string _changeLaneLName = "ChangeLaneL";

    private Animator _animator;
    private AnimatorLayerSwitcher _animatorLayerSwitcher;

    public AnimatorLayerSwitcher AnimatorLayerSwitcher => _animatorLayerSwitcher;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _animatorLayerSwitcher = GetComponentInChildren<AnimatorLayerSwitcher>();
    }

    public void Jump()
    {
        _animator.SetTrigger(_jumpName);
    }

    public void Death()
    {
        _animator.SetTrigger(_deathName);
    }

    public void SetSelectedBool(bool value)
    {
        _animator.SetBool(_selectedName, value);
    }

    public void ChangeLaneR()
    {
        _animator.SetTrigger(_changeLaneRName);
    }

    public void ChangeLaneL()
    {
        _animator.SetTrigger(_changeLaneLName);
    }
}
