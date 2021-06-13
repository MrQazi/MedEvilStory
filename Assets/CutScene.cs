using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public PlayableDirector director;
    public CutScene Next;
    public bool Last = false;
    private void Awake() {
        director.stopped += ((director) => {
            if (Last) {
                LoadScene(SceneName.City);
            }
            else {
                Next.gameObject.SetActive(true);
            }
            gameObject.SetActive(false);
        });
    }
    public void LoadScene(SceneName name) {
        SceneManager.LoadSceneAsync((int)name);
    }
}
public enum SceneName {
    Splash,
    Menu,
    Forest,
    City,
    Village
}
