using UnityEngine;
[CreateAssetMenu(menuName = "Data/ShaftData")]
public class ShaftData : ScriptableObject
{
    public int ShaftCost;
    public MinerData MinerData;
    public UpgradeData UpgradeData;
}
