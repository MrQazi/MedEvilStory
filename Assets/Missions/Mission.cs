using UnityEngine;
using UnityEngine.Events;

public class Mission : MonoBehaviour {
    public Stage Name;
    public Stage Next;
    public Mini minimission = Mini.None;
    public Mini mini_Done = Mini.None;

    public UnityEvent _OnEnable;

    private void OnEnable() {
        _OnEnable.Invoke();
    }
    virtual public void Done(Mini mini = Mini.None) {
        Game.Save();
    }
}