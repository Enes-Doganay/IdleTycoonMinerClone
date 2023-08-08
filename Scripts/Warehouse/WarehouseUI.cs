using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarehouseUI : MonoBehaviour
{
    public static Action<Warehouse, WarehouseUpgrade> OnUpgradeRequest;

    [SerializeField] private TextMeshProUGUI globalGoldTMP;
    private Warehouse warehouse;
    private WarehouseUpgrade warehouseUpgrade;
    private void Start()
    {
        warehouse = GetComponent<Warehouse>();
        warehouseUpgrade = GetComponent<WarehouseUpgrade>();
    }
    private void Update()
    {
        globalGoldTMP.text = GoldManager.Instance.CurrentGold.ToString();
    }
    public void UpgradeRequest()
    {
        OnUpgradeRequest?.Invoke(warehouse, warehouseUpgrade);
    }
}
