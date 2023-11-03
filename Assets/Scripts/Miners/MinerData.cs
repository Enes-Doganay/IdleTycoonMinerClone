using UnityEngine;
[CreateAssetMenu(fileName = "Data/MinerData")]
public class MinerData : ScriptableObject
{
    public float moveSpeed = 5f;
    public int initialCollectCapacity = 200;
    public float goldCollectPerSecond = 50f;
}
