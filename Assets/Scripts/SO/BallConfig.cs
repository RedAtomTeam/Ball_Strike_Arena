using UnityEngine;

[CreateAssetMenu(fileName = "BallConfig", menuName = "Scriptable Objects/BallConfig")]
public class BallConfig : ScriptableObject
{
    public string ballName;
    public Sprite ballSprite;
    public GameObject ballPrefab;

    public BallConfig nextBallConfig;
    public BallConfig prevBallConfig;

    public int scoreForSpawn;
}
