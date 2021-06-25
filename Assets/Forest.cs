using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : Manager
{
    public GameObject CutScenes;
    public MeetUp meetUp;
    public ArrowSearch arrowSearch;
    public GameObject Gameplay;

    override public void Awake() {
        base.Awake();
        if (Inst) {
            Destroy(gameObject);
            return;
        }
        else {
            Inst = this;
        }
    }
    // Start is called before the first frame update
    public override void Start()
    {
        LoadStage();
        base.Start();

    }
    public void LoadStage() {
        switch (data._Stage) {
            case Stage.CutScenes: {
                    CutScenes.SetActive(true);
                }
                break;
            case Stage.MeetUp: {
                    meetUp.gameObject.SetActive(true);
                    Gameplay.SetActive(true);
                }
                break;
            case Stage.ArrowSearch:
                arrowSearch.gameObject.SetActive(true);
                Gameplay.SetActive(true);
                break;
        }
    }
}
