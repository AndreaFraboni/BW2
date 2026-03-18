using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentTimetext;

    private void OnEnable()
    {
        Debug.Log("mi attivo ora e mi registro per update text timer UI !!");

        TimeManager.Instance.OnTimeUpdate += UpdateTextTimerUI;
    }

    private void OnDisable()
    {
        TimeManager.Instance.OnTimeUpdate -= UpdateTextTimerUI;
    }

    private void UpdateTextTimerUI(int currenttime)
    {
        _currentTimetext.text = $"{(int) currenttime} s";
    }

}
