using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    public PlayableDirector director;
    public CutScene NextScene;
    public Image SkipButton;
    private void Awake() {
        director.stopped += ((director) => {
            Next();
        });
        SkipButton.GetComponent<Button>().onClick.AddListener(Next);
    }
    private void Start() {
        StartCoroutine(Skip());
    }
    IEnumerator Skip() {
        SkipButton.fillAmount = 0f;
        SkipButton.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(3f);
        while (SkipButton.fillAmount < 1) {
            SkipButton.fillAmount += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        SkipButton.GetComponent<Button>().interactable = true;
    }
    public void Next() {
        if (NextScene) {
            NextScene.gameObject.SetActive(true);
        }
        else {
            Game.Inst._Scene = SceneName.Forest;
            Game.Inst._Stage = Stage.MeetUp;
            Game.Save();
            SceneManager.LoadSceneAsync(Game.Inst._Scene.ToString());
        }
        gameObject.SetActive(false);
    }
}
