using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public static CharacterSelectionManager Instance { get; private set; }

    [SerializeField] private TMP_InputField _inputFieldObj;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _clickSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayMusic(_backgroundMusic);
    }


    public enum CharacterType { Female, Male }
    public CharacterType SelectedType { get; private set; }

    public void SelectFemale() => SelectedType = CharacterType.Female;
    public void SelectMale() => SelectedType = CharacterType.Male;

    public void OnBackPressed() => SelectedType = default;

    public void OnConfirmPressed()
    {
        if (string.IsNullOrWhiteSpace(_inputFieldObj.text)) return;

        IOManager.Instance.SetPlayerName(_inputFieldObj.text);

        SceneManager.LoadScene(2);
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlaySFX(_clickSound);
    }
}
