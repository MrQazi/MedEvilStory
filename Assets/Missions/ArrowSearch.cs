using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSearch : Mission
{
    public GameObject ChanchillaEscapeMission;
    public Chinchilla chinchilla = null;

    private void Awake() {
        Game.Inst.player.Load();
        FriendData.LoadAll();
        Game.Inst.arrowSearch.Load();
        if ((Game.Inst.arrowSearch.mini | Mini.ChinchillaEscape) != Mini.ChinchillaEscape) ChanchillaEscapeMission.SetActive(true);
    }
    public void StartChinchillaEscape() {
        chinchilla.Rogue = true;
        ChanchillaEscapeMission.SetActive(false);
        minimission = Mini.ChinchillaEscape;
    }
    public override void Done(Mini mini = Mini.None) {
        switch (mini) {
            case Mini.None:
                Manager.Inst.data._Stage = Next;
                gameObject.SetActive(false);
                (Manager.Inst as Forest).LoadStage();
                break;
            case Mini.ChinchillaEscape:
                chinchilla.Rogue = false;
                mini_Done = mini_Done | minimission;
                minimission = Mini.None;
                chinchilla.transform.position = chinchilla.Owner.transform.position;
                break;
        }
        base.Done(mini);
    }
}
[Serializable]
public class ArrowSearchData {
    public Mini mini;

    public void Save() {
        mini = (Manager.Inst as Forest).arrowSearch.mini_Done;
    }
    public void Load() {
        (Manager.Inst as Forest).arrowSearch.mini_Done = mini;
    }
}
