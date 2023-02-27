using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject exitPrompt;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject resetMenu;
    [SerializeField] private GameObject resetTimeConfirm;
    [SerializeField] private GameObject resetOptionsConfirm;
    [SerializeField] private GameObject resetAllConfirm;
    [SerializeField] private OptionsManager optionsManager;

    private void Start() {
        canvas.SetActive(true);
        ShowMenu();
    }
    
    public void LoadGame() {
        AudioManager.instance.Play("button");
        canvas.SetActive(false);
        GameManager.instance.LoadGame();
    }

    public void ShowMenu() {
        AudioManager.instance.Play("button");
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        exitPrompt.SetActive(false);
        creditsMenu.SetActive(false);
        resetMenu.SetActive(false);
        resetTimeConfirm.SetActive(false);
        resetOptionsConfirm.SetActive(false);
        resetAllConfirm.SetActive(false);
    }

    public void ShowOptions() {
        AudioManager.instance.Play("button");
        optionsManager.ReadyOptions();
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        exitPrompt.SetActive(false);
        creditsMenu.SetActive(false);
        resetMenu.SetActive(false);
        resetTimeConfirm.SetActive(false);
        resetOptionsConfirm.SetActive(false);
        resetAllConfirm.SetActive(false);
    }

    public void ShowExit() {
        AudioManager.instance.Play("button");
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        exitPrompt.SetActive(true);
        creditsMenu.SetActive(false);
        resetMenu.SetActive(false);
        resetTimeConfirm.SetActive(false);
        resetOptionsConfirm.SetActive(false);
        resetAllConfirm.SetActive(false);
    }

    public void ShowCredits() {
        AudioManager.instance.Play("button");
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        exitPrompt.SetActive(false);
        creditsMenu.SetActive(true);
        resetMenu.SetActive(false);
        resetTimeConfirm.SetActive(false);
        resetOptionsConfirm.SetActive(false);
        resetAllConfirm.SetActive(false);
    }

    public void ShowResetMenu() {
        AudioManager.instance.Play("button");
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        exitPrompt.SetActive(false);
        creditsMenu.SetActive(false);
        resetMenu.SetActive(true);
        resetTimeConfirm.SetActive(false);
        resetOptionsConfirm.SetActive(false);
        resetAllConfirm.SetActive(false);
    }

    public void ShowOptionsResetPrompt() {
        AudioManager.instance.Play("button");
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        exitPrompt.SetActive(false);
        creditsMenu.SetActive(false);
        resetMenu.SetActive(false);
        resetTimeConfirm.SetActive(false);
        resetOptionsConfirm.SetActive(true);
        resetAllConfirm.SetActive(false);
    }
    
    public void ShowTimeResetPrompt() {
        AudioManager.instance.Play("button");
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        exitPrompt.SetActive(false);
        creditsMenu.SetActive(false);
        resetMenu.SetActive(false);
        resetTimeConfirm.SetActive(true);
        resetOptionsConfirm.SetActive(false);
        resetAllConfirm.SetActive(false);
    }

    public void ShowAllResetPrompt() {
        AudioManager.instance.Play("button");
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        exitPrompt.SetActive(false);
        creditsMenu.SetActive(false);
        resetMenu.SetActive(false);
        resetTimeConfirm.SetActive(false);
        resetOptionsConfirm.SetActive(false);
        resetAllConfirm.SetActive(true);
    }

    public void QuitGame() {
        AudioManager.instance.Play("button");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit(0);
    }
}
