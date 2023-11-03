using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : Singleton<GoldManager>
{
    [SerializeField] private int testGold = 0;
    [SerializeField] private TextMeshProUGUI goldTMP;
    private float currentGold;
    public float CurrentGold=> currentGold;
    private readonly string GOLD_KEY = "GoldKey";

    private void Start()
    {
        LoadGold();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddGold(25000);
        }
    }
    private void LoadGold()
    {
        currentGold = PlayerPrefs.GetFloat(GOLD_KEY, testGold);
        goldTMP.text = Currency.DisplayCurrency(currentGold);
    }
    public void AddGold(float amount)
    {
        currentGold += amount;
        goldTMP.text = Currency.DisplayCurrency(currentGold);
        PlayerPrefs.SetFloat(GOLD_KEY, currentGold);
        PlayerPrefs.Save();
    }
    public void RemoveGold(float amount)
    {
        currentGold -= amount;
        goldTMP.text = Currency.DisplayCurrency(currentGold);
        PlayerPrefs.SetFloat(GOLD_KEY, currentGold);
        PlayerPrefs.Save();
    }
}
