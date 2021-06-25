using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Game {
    public SceneName _Scene = SceneName.Forest;
    public Stage _Stage = Stage.CutScenes;
    public PlayerData player = new PlayerData();
    public FriendData[] friends = new FriendData[6];

    #region
    public ArrowSearchData arrowSearch = new ArrowSearchData();
    #endregion

    [NonSerialized] static Game Instance;
    public static Game Inst {
        get {
            if (Instance == null)
                Instance = new Game();
            return Instance;
        }
    }
#if UNITY_EDITOR
    [MenuItem("SaveGame/Save")]
#endif
    public static void Save() {
        Instance.player.Save();
        FriendData.SaveAll();
        Instance.arrowSearch.Save();
        BinaryFormatter formatter = new BinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath + "/" + Application.productName))
            Directory.CreateDirectory(Application.persistentDataPath + "/" + Application.productName);
        var path = Application.persistentDataPath + "/" + Application.productName + "/Game.bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, JsonUtility.ToJson(Instance));
        stream.Close();
    }
    public static Game Load() {
        var path = Application.persistentDataPath + "/" + Application.productName + "/Game.bin";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Instance = JsonUtility.FromJson<Game>(formatter.Deserialize(stream) as string);
            stream.Close();
            return Inst;
        }
        else {
            Instance = new Game();
            return Instance;
        }
    }
    public static bool Exists() {
        return File.Exists(Application.persistentDataPath + "/" + Application.productName + "/Game.bin");
    }
#if UNITY_EDITOR
    [MenuItem("SaveGame/Delete")]
#endif
    public static void Delete() {
        if (File.Exists(Application.persistentDataPath + "/" + Application.productName + "/Game.bin")) {
            File.Delete(Application.persistentDataPath + "/" + Application.productName + "/Game.bin");
        }
    }
}
public enum SceneName {
    Splash,
    MainMenu,
    Forest,
    City,
    TeleVillage
}
public enum Stage {
    CutScenes,
    Home,
    Help,
    Visitors,
    Leave,
    MeetUp,
    ArrowSearch,
    FaceOff,
    Reach,
    Kidnap
}
public enum Mini {
    None = 0,
    ChinchillaEscape = 1,
    Bridge = 2,
    Bag = 4,
    Fight = 8,
    Glasses = 16,
}
