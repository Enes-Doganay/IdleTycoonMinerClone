using System;
using UnityEngine;

public class BaseUpgrade : MonoBehaviour
{
    public static Action<BaseUpgrade, int> OnUpgrade;
    [Header("Upgrades")]
    [SerializeField] protected float collectCapacityMultiplier = 2f;
    [SerializeField] protected float collectPerSecondMultiplier = 2f;
    [SerializeField] protected float moveSpeedMultiplier = 1.25f;

    [Header("Cost")]
    [SerializeField] private float initialUpgradeCost = 200f;
    [SerializeField] private float upgradeCostMultiplier = 2f;
    protected Shaft shaft;
    protected Elevator elevator;
    protected Warehouse warehouse;
    public int CurrentLevel { get; private set; }
    public float UpgradeCost { get; private set; }
    public float CollectCapacityMultiplier => collectCapacityMultiplier;
    public float CollectPerSecondMultiplier => collectPerSecondMultiplier;
    public float MoveSpeedMultiplier => moveSpeedMultiplier;
    public float UpgradeCostMultiplier => upgradeCostMultiplier;
    public Elevator Elevator => elevator;

    private void Start()
    {
        warehouse = GetComponent<Warehouse>();
        shaft = GetComponent<Shaft>();
        elevator = GetComponent<Elevator>();
        CurrentLevel = 1;
        UpgradeCost = initialUpgradeCost;
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
        UpgradeCost *= upgradeCostMultiplier;
    }
    protected virtual void RunUpgrade()
    {
        //Upgrade Logic
    }
}