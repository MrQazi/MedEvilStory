using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutScene : MonoBehaviour
{
    public PlayableDirector director;
    public CutScene Next;
    private void Awake() {
        director.stopped += ((director) => {
            Next.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}
