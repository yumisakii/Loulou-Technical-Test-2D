using UnityEngine;

[CreateAssetMenu(fileName = "BalloonData", menuName = "Scriptable Objects/BalloonData")]
public class BalloonData : ScriptableObject
{
    public string balloonName;
    public Sprite balloonSprite;
    public bool isHotColor;
}
