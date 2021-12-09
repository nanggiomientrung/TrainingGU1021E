using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "TestScriptableObject/LevelData", order = 1)]
public class LevelData_ScriptableObject : ScriptableObject
{
    public List<LevelData> levelData;
}


[Serializable]
public class LevelData
{
    public GameObject prefab;
    public float velocity;
}