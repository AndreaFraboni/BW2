using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : GenericSingleton<CharacterSelectionManager>
{
    [SerializeField] private TMP_InputField _nameInput;

    public enum CharacterType { Female, Male }
    public CharacterType SelectedType { get; private set; }

    public void SelectFemale() => SelectedType = CharacterType.Female;
    public void SelectMale() => SelectedType = CharacterType.Male;

    public void OnBackPressed() => SelectedType = default;

    public void OnConfirmPressed()
    {
        //if (string.IsNullOrWhiteSpace(_nameInput.text)) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
