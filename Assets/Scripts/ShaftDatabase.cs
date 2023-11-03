using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ShaftDatabase")]
public class ShaftDatabase : ScriptableObject
{
    public List<ShaftData> shaftDataList = new List<ShaftData>();
}
