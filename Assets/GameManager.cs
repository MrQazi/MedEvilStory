using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform CutScenes;
    public int CuttScene_Index = 0;
    // Start is called before the first frame update
    void Start()
    {
        CutScenes.GetChild(CuttScene_Index).gameObject.SetActive(true);
    }
}
