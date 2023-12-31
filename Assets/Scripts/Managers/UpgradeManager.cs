using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    #region Inspector
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private TextMeshProUGUI panelTitle;
    [SerializeField] private GameObject[] stats;
    [SerializeField] private Image panelIcon;

    [Header("Button Color")]
    [SerializeField] private Color buttonDisabledColor;
    [SerializeField] private Color buttonEnabledColor;

    [Header("Buttons")]
    [SerializeField] private GameObject[] upgradeButtons;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI upgradeCost;
    [SerializeField] private TextMeshProUGUI currentStat1;
    [SerializeField] private TextMeshProUGUI currentStat2;
    [SerializeField] private TextMeshProUGUI currentStat3;
    [SerializeField] private TextMeshProUGUI currentStat4;
    [SerializeField] private TextMeshProUGUI stat1Title;
    [SerializeField] private TextMeshProUGUI stat2Title;
    [SerializeField] private TextMeshProUGUI stat3Title;
    [SerializeField] private TextMeshProUGUI stat4Title;

    [Header("Upgraded")]
    [SerializeField] private TextMeshProUGUI statUpgraded1;
    [SerializeField] private TextMeshProUGUI statUpgraded2;
    [SerializeField] private TextMeshProUGUI statUpgraded3;
    [SerializeField] private TextMeshProUGUI statUpgraded4;

    [Header("Images")]
    [SerializeField] private Image stat1Icon;
    [SerializeField] private Image stat2Icon;
    [SerializeField] private Image stat3Icon;
    [SerializeField] private Image stat4Icon;

    [Header("Shaft Icons")]
    [SerializeField] private Sprite shaftMinerIcon;
    [SerializeField] private Sprite minerIcon;
    [SerializeField] private Sprite walkingIcon;
    [SerializeField] private Sprite miningIcon;
    [SerializeField] private Sprite workerCapacityIcon;

    [Header("Elevator Icon")]
    [SerializeField] private Sprite elevatorMinerIcon;
    [SerializeField] private Sprite loadIcon;
    [SerializeField] private Sprite movementIcon;
    [SerializeField] private Sprite loadingIcon;

    [Header("Warhouse Icon")]
    [SerializeField] private Sprite warehouseMinerIcon;
    [SerializeField] private Sprite transporterIcon;
    [SerializeField] private Sprite transportationIcon;
    [SerializeField] private Sprite warehouseLoadingIcon;
    [SerializeField] private Sprite warehouseWalkingIcon;

    #endregion

    public int TimesToUpgrade { get; set; }

    private Shaft selectedShaft;
    private Warehouse currentWarhouse;
    private ShaftUpgrade selectedShaftUpgrade;
    private BaseUpgrade currentUpgrade;

    private int activeButtonIndex = 0;

    private void Start()
    {
        ActivateButton(0);
        TimesToUpgrade = 1;
    }
    public void Upgrade()
    {
        if(GoldManager.Instance.CurrentGold >= GetUpgradeCost(TimesToUpgrade,currentUpgrade))//currentUpgrade.UpgradeCost)
        {
            Debug.Log("Times to" + TimesToUpgrade);
            currentUpgrade.Upgrade(TimesToUpgrade);
            UpdateUpgradePanel();
            UpdateTimesToUpgrade();
        }
    }
    public void UpdateUpgradePanel()
    {
        if (currentUpgrade is ShaftUpgrade)
        {
            UpdateUpgradePanel(currentUpgrade);
        }
        if (currentUpgrade is ElevatorUpgrade)
        {
            UpdateElevatorPanel(currentUpgrade);
        }
        if (currentUpgrade is WarehouseUpgrade)
        {
            UpdateWarhousePanel(currentUpgrade);
        }
    }
    public void OpenUpgradePanel(bool status)
    {
        upgradePanel.SetActive(status);
    }
    #region Upgrade Button
    public void UpgradeX1()
    {
        ActivateButton(0);
        TimesToUpgrade = 1;
        UpdateUpgradePanel();
        upgradeCost.text = Currency.DisplayCurrency((int)currentUpgrade.UpgradeCost);
    }
    public void UpgradeX10()
    {
        ActivateButton(1);
        TimesToUpgrade = CanUpgradeManyTimes(10, currentUpgrade) ? 10 : 0;
        UpdateUpgradePanel();
        upgradeCost.text = Currency.DisplayCurrency(GetUpgradeCost(10, currentUpgrade));
    }
    public void UpgradeX50()
    {
        ActivateButton(2);
        TimesToUpgrade = CanUpgradeManyTimes(50, currentUpgrade) ? 50 : 0;
        UpdateUpgradePanel();
        upgradeCost.text = Currency.DisplayCurrency(GetUpgradeCost(50, currentUpgrade));
    }
    public void UpgradeMax()
    {
        ActivateButton(3);
        TimesToUpgrade = CalculateUpgradeCount(currentUpgrade);
        UpdateUpgradePanel();
        upgradeCost.text = Currency.DisplayCurrency(GetUpgradeCost(TimesToUpgrade, currentUpgrade));
    }
    public void UpdateTimesToUpgrade()
    {
        switch (activeButtonIndex)
        {
            case 0:
                UpgradeX1();
                break;
            case 1:
                UpgradeX10();
                break;
            case 2:
                UpgradeX50();
                break;
            case 3:
                UpgradeMax();
                break;
        }
    }
    #endregion

    #region Update Shaft Panel
    private void UpdateUpgradePanel(BaseUpgrade upgrade)
    {
        panelTitle.text = $"Mine Shaft {selectedShaft.ShaftID + 1} Level {upgrade.CurrentLevel}";

        upgradeCost.text = Currency.DisplayCurrency((int)upgrade.UpgradeCost);
        currentStat1.text = selectedShaft.Miners.Count.ToString("F2");
        currentStat2.text = selectedShaft.Miners[0].MoveSpeed.ToString("F2");
        currentStat3.text = selectedShaft.Miners[0].CollectPerSecond.ToString("F2");
        currentStat4.text = selectedShaft.Miners[0].CollectCapacity.ToString("F2");

        stats[3].SetActive(true);
        panelIcon.sprite = shaftMinerIcon;
        //Update stat icons
        stat1Icon.sprite = minerIcon;
        stat2Icon.sprite = walkingIcon;
        stat3Icon.sprite = miningIcon;
        stat4Icon.sprite = workerCapacityIcon;

        //Update stat titles
        stat1Title.text = "Miners";
        stat2Title.text = "Walking Speed";
        stat3Title.text = "Mining Speed";
        stat4Title.text = "Worker Capacity";

        //Upgrade worker capacity
        float collectCapacity = selectedShaft.Miners[0].CollectCapacity;
        float collectCapacityMultiplier = upgrade.CollectCapacityMultiplier;
        float collectCapacityAdded = Mathf.Abs(collectCapacity - (collectCapacity * collectCapacityMultiplier));
        statUpgraded4.text = collectCapacityAdded.ToString("F2");

        //Upgrade load speed
        float currentLoadSpeed = selectedShaft.Miners[0].CollectPerSecond;
        float currentLoadSpeedMultiplier = upgrade.CollectPerSecondMultiplier;
        float loadSpeedAdded = Mathf.Abs(currentLoadSpeed - (currentLoadSpeed * currentLoadSpeedMultiplier));
        statUpgraded3.text = loadSpeedAdded.ToString("F2");

        //Upgrade move speed
        float walkSpeed = selectedShaft.Miners[0].MoveSpeed;
        float walkSpeedMultiplier = upgrade.MoveSpeedMultiplier;
        float walkSpeedAdded = (int)Mathf.Abs(walkSpeed - (walkSpeed * walkSpeedMultiplier));
        if ((upgrade.CurrentLevel + 1) % 10 == 0)
        {
            statUpgraded2.text = walkSpeedAdded.ToString("F2");
        }
        else
        {
            statUpgraded2.text = $"+0/s";
        }

        //Upgrade miner count
        if ((upgrade.CurrentLevel + 1) % 10 == 0)
        {
            statUpgraded1.text = $"+1";
        }
        else
        {
            statUpgraded1.text = $"+0";
        }
    }
    #endregion
    #region Update Elevator Panel
    public void UpdateElevatorPanel(BaseUpgrade elevatorUpgrade)
    {
        ElevatorMiner miner = elevatorUpgrade.Elevator.Miner;
        panelTitle.text = $"Elevator Level {elevatorUpgrade.CurrentLevel}";
        upgradeCost.text = Currency.DisplayCurrency(elevatorUpgrade.UpgradeCost);
        stats[3].SetActive(false);
        panelIcon.sprite = elevatorMinerIcon;

        //Update stat icons
        stat1Icon.sprite = loadIcon;
        stat2Icon.sprite = movementIcon;
        stat3Icon.sprite = loadingIcon;

        //Update stat titles
        stat1Title.text = "Load";
        stat2Title.text = "Movement";
        stat3Title.text = "Loading Speed";

        //Update current stats
        currentStat1.text = Currency.DisplayCurrency(miner.CollectCapacity);//miner.CollectCapacity.ToString("F2");
        currentStat2.text = Currency.DisplayCurrency(miner.MoveSpeed);// miner.MoveSpeed.ToString("F2");
        currentStat3.text = Currency.DisplayCurrency(miner.CollectPerSecond);// miner.CollectPerSecond.ToString("F2");

        //Update load value upgraded
        float currentCollect =  miner.CollectCapacity;
        float collectMultiplier = elevatorUpgrade.CollectCapacityMultiplier;
        float load = Mathf.Abs(currentCollect - (currentCollect * collectMultiplier));
        statUpgraded1.text = Currency.DisplayCurrency(load);//load.ToString("F2");

        //Update move speed upgraded
        float currentMoveSpeed = miner.MoveSpeed;
        float moveSpeedMultiplier = elevatorUpgrade.MoveSpeedMultiplier;
        float moveSpeedAdded = Mathf.Abs(currentMoveSpeed - (currentMoveSpeed * moveSpeedMultiplier));
        if ((elevatorUpgrade.CurrentLevel + 1) % 10 == 0)
        {
            statUpgraded2.text = Currency.DisplayCurrency(moveSpeedAdded);//moveSpeedAdded.ToString("F2");
        }
        //Update new loading speed added
        float loadingSpeed = miner.CollectPerSecond;
        float loadingSpeedMultiplier = elevatorUpgrade.CollectPerSecondMultiplier;
        float loadingAdded = Mathf.Abs(loadingSpeed - (loadingSpeed * loadingSpeedMultiplier));
        statUpgraded3.text = Currency.DisplayCurrency(loadingAdded);//loadingAdded.ToString("F2") + "/s";
    }
    #endregion
    #region Update Warehouse Panel
    private void UpdateWarhousePanel(BaseUpgrade upgrade)
    {
        panelTitle.text = $"Warhouse Level {upgrade.CurrentLevel}";
        upgradeCost.text = Currency.DisplayCurrency(upgrade.UpgradeCost);
        stats[3].SetActive(true);
        panelIcon.sprite = warehouseMinerIcon;

        //Update Icons
        stat1Icon.sprite = transporterIcon;
        stat2Icon.sprite = transportationIcon;
        stat3Icon.sprite = warehouseLoadingIcon;
        stat4Icon.sprite = warehouseWalkingIcon;

        //Update stats title
        stat1Title.text = "Transporters";
        stat2Title.text = "Transportation";
        stat3Title.text = "Loading Speed";
        stat4Title.text = "Walking Speed";

        //Update current values
        currentStat1.text = currentWarhouse.Miners.Count.ToString("F2");
        currentStat2.text = Currency.DisplayCurrency(currentWarhouse.Miners[0].CollectCapacity);
        currentStat3.text = Currency.DisplayCurrency(currentWarhouse.Miners[0].CollectPerSecond);
        currentStat4.text = Currency.DisplayCurrency(currentWarhouse.Miners[0].MoveSpeed);
        //Update miners count upgraded
        if ((upgrade.CurrentLevel + 1) % 10 == 0)
        {
            statUpgraded1.text = $"+1";
        }
        else
        {
            statUpgraded1.text = $"+0";
        }

        //Update transportation upgraded
        float collectCapacity = currentWarhouse.Miners[0].CollectCapacity;
        float collectCapacityMultiplier = upgrade.CollectCapacityMultiplier;
        float collectCapacityAdded = Mathf.Abs(collectCapacity - (collectCapacity * collectCapacityMultiplier));
        statUpgraded2.text = Currency.DisplayCurrency(collectCapacityAdded); 

        //Upgrade load speed
        float currentLoadSpeed = currentWarhouse.Miners[0].CollectPerSecond;
        float currentLoadSpeedMultiplier = upgrade.CollectPerSecondMultiplier;
        float loadSpeedAdded = (int)Mathf.Abs(currentLoadSpeed - (currentLoadSpeed * currentLoadSpeedMultiplier));
        statUpgraded3.text = Currency.DisplayCurrency(loadSpeedAdded);

        //Upgrade move speed
        float walkSpeed = currentWarhouse.Miners[0].MoveSpeed;
        float walkSpeedMultiplier = upgrade.MoveSpeedMultiplier;
        float walkSpeedAdded = Mathf.Abs(walkSpeed - (walkSpeed * walkSpeedMultiplier));
        if ((upgrade.CurrentLevel + 1) % 10 == 0)
        {
            statUpgraded4.text = "+" + walkSpeedAdded.ToString("F2") + "/s";
        }
        else
        {
            statUpgraded4.text = "+0/s";
        }
    }

    #endregion

    #region Events
    private void ShaftUpgradeRequest(Shaft shaft, ShaftUpgrade shaftUpgrade)
    {
        List<Shaft> shaftList = ShaftManager.Instance.Shafts;
        for (int i = 0; i < shaftList.Count; i++)
        {
            if (shaft.ShaftID == shaftList[i].ShaftID)
            {
                selectedShaft = shaftList[i];
                selectedShaftUpgrade = shaftList[i].GetComponent<ShaftUpgrade>();
            }
        }
        currentUpgrade = shaftUpgrade;
        OpenUpgradePanel(true);
        UpdateUpgradePanel(selectedShaftUpgrade);
    }
    private void WarhouseUpgradeRequest(Warehouse warehouse, WarehouseUpgrade warehouseUpgrade)
    {
        currentUpgrade = warehouseUpgrade;
        currentWarhouse = warehouse;
        OpenUpgradePanel(true);
        UpdateWarhousePanel(warehouseUpgrade);
    }

    private void OnEnable()
    {
        ShaftUI.OnUpgradeRequest += ShaftUpgradeRequest;
        ElevatorUI.OnUpgradeRequest += ElevatorUpgradeRequest;
        WarehouseUI.OnUpgradeRequest += WarhouseUpgradeRequest;
    }

    private void OnDisable()
    {
        ShaftUI.OnUpgradeRequest -= ShaftUpgradeRequest;
        ElevatorUI.OnUpgradeRequest -= ElevatorUpgradeRequest;
        WarehouseUI.OnUpgradeRequest -= WarhouseUpgradeRequest;
    }


    #endregion
    private void ElevatorUpgradeRequest(ElevatorUpgrade elevatorUpgrade)
    {
        currentUpgrade = elevatorUpgrade;
        OpenUpgradePanel(true);
        UpdateElevatorPanel(elevatorUpgrade);
    }
    #region HelpMethods
    public void ActivateButton(int buttonIndex)
    {
        activeButtonIndex = buttonIndex;
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].GetComponent<Image>().color = buttonDisabledColor;
        }
        upgradeButtons[buttonIndex].GetComponent<Image>().color = buttonEnabledColor;
    }
    private int GetUpgradeCost(int amount, BaseUpgrade upgrade)
    {
        int cost = 0;
        int upgradeCost = (int) upgrade.UpgradeCost;
        for (int i = 0; i < amount; i++)
        {
            cost += upgradeCost;
            upgradeCost *= (int)upgrade.UpgradeCostMultiplier;
        }
        return cost;
    }
    public bool CanUpgradeManyTimes(int upgradeAmount, BaseUpgrade upgrade)
    {
        int count = CalculateUpgradeCount(upgrade, upgradeAmount);
        if (count >= upgradeAmount)
        {
            return true;
        }
        return false;
    }
    public int CalculateUpgradeCount(BaseUpgrade upgrade, int upgradeAmount = -1)
    {
        Debug.Log("calculate upgrade amount" + upgradeAmount);
        int count = 0;
        float currentGold = GoldManager.Instance.CurrentGold;
        float upgradeCost = upgrade.UpgradeCost;
        for (float i = currentGold; i >= 0; i -= upgradeCost)
        {
            count++;
            upgradeCost *= upgrade.UpgradeCostMultiplier;
            if (count == upgradeAmount)
            {

                return count;
            }
        }
        return count;
    }
    #endregion
}
