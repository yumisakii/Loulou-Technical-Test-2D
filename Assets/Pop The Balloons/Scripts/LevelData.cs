using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [Header("Level Info")]
    public string levelName;
    public string instructionText;

    [Header("Balloon Pools")]
    public List<BalloonData> correctBalloons;
    public List<BalloonData> incorrectBalloons;

    [Header("Rules & Difficulty")]
    public float timeLimit;
    public int targetScore;
    public float balloonMinSpeed;
    public float balloonMaxSpeed;
    public float spawnRate;
}
