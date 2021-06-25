using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    public Player player;
    public Friend[] Friends;
    public Game data;
    public static Manager Inst;

    public virtual void Awake() {
        data = Game.Inst;
    }
    public virtual void Start() {
    }
}