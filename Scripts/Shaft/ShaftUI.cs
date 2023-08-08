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
    [SerializeField] private TextMeshProUGUI currentGoldTMP;
    [SerializeField] private TextMeshProUGUI currentLevelTMP;
    private Shaft shaft;
    private ShaftUpgrade shaftUpgrade;
    private void Start()
    {
        shaftUpgrade = GetComponent<ShaftUpgrade>();
        shaft = GetComponent<Shaft>();
    }
    private void Update()
    {
        currentGoldTMP.text = shaft.CurrentDeposit.CurrentGold.ToString(); //Updatede g�ncellemek do�ru de�il
    }
    public void BuyNewShaft() //Sat�n alman�n ShaftUI'da olmas� sa�ma
    {
        if(GoldManager.Instance.CurrentGold >= ShaftManager.Instance.NewShaftCost)
        {
            GoldManager.Instance.RemoveGold(ShaftManager.Instance.NewShaftCost);
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
            //currentLevelTMP.text = "Level\n" + currentLevel.ToString();
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
