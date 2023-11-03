using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Deposit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    public float CurrentGold { get; set; } //Gold çekilene kadar baþka gold çekilememesi için çekilebilir gold adýnda bir deðiþken tanýmlayýp remove gold diyene kadar collect goldun onun üzerinden iþlem yapmasýný saðlayabiliriz
    private float CollectibleGold;
    private void Start()
    {
        goldText.text = Currency.DisplayCurrency(CurrentGold);
    }
    public void DepositGold(float amount)
    {
        CurrentGold += amount;
        goldText.text = Currency.DisplayCurrency(CurrentGold);
    }

    public void RemoveGold(float amount) 
    {
        CurrentGold -= amount;
        goldText.text = Currency.DisplayCurrency(CurrentGold);
    }
    public float CollectGold(BaseMiner miner)
    {
        float collectCapacity = miner.CollectCapacity;
        float currentGold = miner.CurrentGold;
        float minerCapacity = collectCapacity - currentGold;
        return EvaluateAmountToCollect(minerCapacity);
    }
    private float EvaluateAmountToCollect(float minerCollectCapacity)
    {
        if (minerCollectCapacity <= CurrentGold)
        {
            return minerCollectCapacity;
        }
        return CurrentGold;
    }
    public bool CanCollectGold()
    {
        return CurrentGold > 0;
    }
}
