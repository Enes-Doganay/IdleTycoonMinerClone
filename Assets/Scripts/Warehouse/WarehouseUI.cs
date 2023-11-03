using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarehouseUI : MonoBehaviour
{
    public static Action<Warehouse, WarehouseUpgrade> OnUpgradeRequest;

    [SerializeField] private TextMeshProUGUI currentLevelTMP;
    private Warehouse warehouse;
    private WarehouseUpgrade warehouseUpgrade;

    private void Start()
    {
        warehouse = GetComponent<Warehouse>();
        warehouseUpgrade = GetComponent<WarehouseUpgrade>();
    }
    public void UpgradeRequest()
    {
        OnUpgradeRequest?.Invoke(warehouse, warehouseUpgrade);
    }
    private void OnEnable()
    {
        ShaftUpgrade.OnUpgrade += UpgradeWarehouse;
    }
    private void OnDisable()
    {
        ShaftUpgrade.OnUpgrade -= UpgradeWarehouse;
    }

    private void UpgradeWarehouse(BaseUpgrade upgrade, int currentLevel)
    {
        if (upgrade == warehouseUpgrade)
        {
            currentLevelTMP.text = $"Level\n {currentLevel}";
        }
    }
}
