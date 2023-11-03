using System.Collections.Generic;
using UnityEngine;

public class ShaftManager : Singleton<ShaftManager>
{
    [SerializeField] private Shaft shaftPrefab;
    [SerializeField] private ShaftDatabase shaftDatabase;
    [SerializeField] private float newShaftYPosition;
    [SerializeField] private int newShaftCost = 500;

    [Header("Shaft")]
    [SerializeField] private List<Shaft> shafts;
    public List<Shaft> Shafts => shafts;
    public int NewShaftCost => newShaftCost;
    private int currentShaftIndex = 0;
    private void Start()
    {
        //shafts[0].ShaftID = 0;
        //AddShaft();
    }
    public void AddShaft()
    {
        Transform lastShaft = shafts[currentShaftIndex].transform;
        Shaft newShaft = Instantiate(shaftPrefab, lastShaft.position, Quaternion.identity);
        newShaft.Initiliaze(shaftDatabase.shaftDataList[currentShaftIndex]);
        newShaft.GetComponent<BaseUpgrade>().Initiliaze(shaftDatabase.shaftDataList[currentShaftIndex].UpgradeData);
        newShaft.transform.localPosition = new Vector3(lastShaft.position.x, lastShaft.position.y - newShaftYPosition, lastShaft.position.z);
        currentShaftIndex++;
        newShaft.ShaftID = currentShaftIndex;
        shafts.Add(newShaft);
    }
    public int GetShaftCost()
    {
        ShaftData shaft = shaftDatabase.shaftDataList[currentShaftIndex];
        return shaft.ShaftCost;
    }
}
