using UnityEngine;

[CreateAssetMenu(fileName = "Data/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public float collectCapacityMultiplier = 2f;
    public float collectPerSecondMultiplier = 2f;
    public float moveSpeedMultiplier = 1.25f;
    
    public float initialUpgradeCost = 200f;
    public float upgradeCostMultiplier = 2f;
}
