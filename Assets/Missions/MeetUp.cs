using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class MeetUp : Mission
{
    public PlayableDirector timeline;
    private void Start() {
        timeline.stopped += (director) => {
            Game.Inst._Stage = Next;
            Game.Save();
            (Manager.Inst as Forest).LoadStage();
            gameObject.SetActive(false);
        };
    }
}
