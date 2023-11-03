using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElevatorUI : MonoBehaviour
{
    public static Action<ElevatorUpgrade> OnUpgradeRequest; 
    [SerializeField] private TextMeshProUGUI elevatorDepositGold;
    [SerializeField] private TextMeshProUGUI currentLevelTMP;
    
    private Elevator elevator;
    private ElevatorUpgrade elevatorUpgrade;

    private void Start()
    {
        elevatorUpgrade = GetComponent<ElevatorUpgrade>();
        elevator = GetComponent<Elevator>();
    }
    public void RequestUpgrade()
    {
        OnUpgradeRequest?.Invoke(elevatorUpgrade);
    }
    private void OnEnable()
    {
        ShaftUpgrade.OnUpgrade += UpgradeElevator;
    }
    private void OnDisable()
    {
        ShaftUpgrade.OnUpgrade -= UpgradeElevator;
    }

    private void UpgradeElevator(BaseUpgrade upgrade, int currentLevel)
    {
        if (upgrade == elevatorUpgrade)
        {
            currentLevelTMP.text = $"Level\n {currentLevel}";
        }
    }
}
