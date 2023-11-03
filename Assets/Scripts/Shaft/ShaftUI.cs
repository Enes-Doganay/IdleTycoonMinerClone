using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShaftUI : MonoBehaviour
{
    public static Action<Shaft, ShaftUpgrade> OnUpgradeRequest;
    [Header("Buttons")]
    [SerializeField] private GameObject buyNewShaftButton;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI currentLevelTMP;
    [SerializeField] private TextMeshProUGUI upgradeCostTMP;
    private Shaft shaft;
    private ShaftUpgrade shaftUpgrade;
    private void Start()
    {
        shaftUpgrade = GetComponent<ShaftUpgrade>();
        shaft = GetComponent<Shaft>();
        upgradeCostTMP.text = Currency.DisplayCurrency(ShaftManager.Instance.GetShaftCost());
    }
    public void BuyNewShaft()
    {
        if(GoldManager.Instance.CurrentGold >= ShaftManager.Instance.GetShaftCost())
        {
            GoldManager.Instance.RemoveGold(ShaftManager.Instance.GetShaftCost());
            ShaftManager.Instance.AddShaft();
            buyNewShaftButton.SetActive(false);
        }
    }
    public void UpgradeRequest()
    {
        OnUpgradeRequest?.Invoke(shaft, shaftUpgrade);
    }
    private void UpgradeShaft(BaseUpgrade upgrade, int currentLevel)
    {
        if(upgrade == shaftUpgrade)
        {
            currentLevelTMP.text = $"Level\n { currentLevel}";
        }
    }
    private void OnEnable()
    {
        ShaftUpgrade.OnUpgrade += UpgradeShaft;
    }
    private void OnDisable()
    {
        ShaftUpgrade.OnUpgrade -= UpgradeShaft;
    }
}
