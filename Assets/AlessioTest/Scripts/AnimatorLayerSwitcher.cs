using UnityEngine;

public class AnimatorLayerSwitcher : MonoBehaviour
{
    [SerializeField] private int _gameplayLayer = 0;
    [SerializeField] private int _cinematicLayer = 1;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        SetCinematicMode();
    }
    public void SetCinematicMode()
    {
        _animator.SetLayerWeight(_gameplayLayer , 0f);
        _animator.SetLayerWeight(_cinematicLayer , 1f);
    }

    public void SetGameplayMode()
    {
        _animator.SetLayerWeight(_gameplayLayer , 1f);
        _animator.SetLayerWeight(_cinematicLayer, 0f);
    }
}
