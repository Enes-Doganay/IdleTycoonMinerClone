using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElevatorUI : MonoBehaviour
{
    public static Action<ElevatorUpgrade> OnUpgradeRequest; 
    [SerializeField] private TextMeshProUGUI elevatorDepositGold;
    private Elevator elevator;
    private ElevatorUpgrade elevatorUpgrade;
    private void Start()
    {
        elevatorUpgrade = GetComponent<ElevatorUpgrade>();
        elevator = GetComponent<Elevator>();
    }
    private void Update()
    {
        elevatorDepositGold.text = elevator.ElevatorDeposit.CurrentGold.ToString();
    }
    public void RequestUpgrade()
    {
        OnUpgradeRequest?.Invoke(elevatorUpgrade);
    }
}
