using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Deposit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    public float CurrentGold { get; set; } //Gold �ekilene kadar ba�ka gold �ekilememesi i�in �ekilebilir gold ad�nda bir de�i�ken tan�mlay�p remove gold diyene kadar collect goldun onun �zerinden i�lem yapmas�n� sa�layabiliriz
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
