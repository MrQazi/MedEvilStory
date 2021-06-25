using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button btn_Resume;
    public Button btn_NewGame;
    private void Start() {
        btn_NewGame.onClick.AddListener(NewGame);
        btn_Resume.onClick.AddListener(Resume);
    }
    public void Resume() {
        Game.Load();
        SceneManager.LoadSceneAsync(Game.Inst._Scene.ToString());
    }
    public void NewGame() {
        Game.Delete();
        SceneManager.LoadSceneAsync("Forest");
    }
}
