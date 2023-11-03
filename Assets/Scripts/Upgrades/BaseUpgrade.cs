using System;
using UnityEngine;

public class BaseUpgrade : MonoBehaviour
{
    public static Action<BaseUpgrade, int> OnUpgrade;
    [Header("Upgrades")]
    [SerializeField] protected UpgradeData upgradeData;
    protected Shaft shaft;
    protected Elevator elevator;
    protected Warehouse warehouse;
    public int CurrentLevel { get; private set; }
    protected float upgradeCost;
    //public float UpgradeCost { get; private set; }
    public float UpgradeCost => upgradeCost;
    public float CollectCapacityMultiplier => upgradeData.collectCapacityMultiplier;
    public float CollectPerSecondMultiplier => upgradeData.collectPerSecondMultiplier;
    public float MoveSpeedMultiplier => upgradeData.moveSpeedMultiplier;
    public float UpgradeCostMultiplier => upgradeData.upgradeCostMultiplier;
    public Elevator Elevator => elevator;

    private void Start()
    {
        upgradeCost = upgradeData.initialUpgradeCost;
        warehouse = GetComponent<Warehouse>();
        shaft = GetComponent<Shaft>();
        elevator = GetComponent<Elevator>();
        CurrentLevel = 1;
    }
    public void Initiliaze(UpgradeData upgradeData)
    {
        this.upgradeData = upgradeData;
    }
    public virtual void Upgrade(int upgradeAmount)
    {
        if (upgradeAmount > 0)
        {
            for(int i = 0; i < upgradeAmount; i++)
            {
                UpgradeSuccess();
                UpdateUpgradeValues();
                RunUpgrade();
            }
        }
    }

    protected virtual void UpgradeSuccess()
    {
        GoldManager.Instance.RemoveGold((int)UpgradeCost);
        CurrentLevel++;
        OnUpgrade?.Invoke(this, CurrentLevel);
    }
    protected virtual void UpdateUpgradeValues()
    {
        //Update values
        upgradeCost *= upgradeData.upgradeCostMultiplier;
    }
    protected virtual void RunUpgrade()
    {
        //Upgrade Logic
    }
}