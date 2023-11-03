using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorMiner : BaseMiner
{
    [SerializeField] private Elevator elevator;
    private int currentShaftIndex = -1;
    private Deposit currentDeposit;

    private void Start()
    {
        Initiliaze();
    }
    public void Initiliaze()
    {
        MoveSpeed = minerData.moveSpeed;
        CollectCapacity = minerData.initialCollectCapacity;
        CollectPerSecond = minerData.goldCollectPerSecond;
        MoveToNextLocation();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
        }
    }
    public void MoveToNextLocation()
    {
        currentShaftIndex++;
        Shaft currentShaft = ShaftManager.Instance.Shafts[currentShaftIndex]; 
        Vector2 nextPosition = new Vector2(transform.position.x, currentShaft.DepositLocation.position.y);
        currentDeposit = currentShaft.CurrentDeposit;
        MoveMiner(nextPosition);
    }
    protected override void CollectGold()
    {
        //if (!currentDeposit.CanCollectGold() && currentDeposit != null && currentShaftIndex == ShaftManager.Instance.Shafts.Count - 1)
        if(currentDeposit == null)
        {
            currentShaftIndex = -1;
            ChangeGoal();
            Vector3 elevatorDepositPos = new Vector3(transform.position.x, elevator.DepositLocation.position.y);
            MoveMiner(elevatorDepositPos);
            return;
        }
        float amountToCollect = currentDeposit.CollectGold(this);
        float collectTime = amountToCollect / CollectPerSecond;
        OnLoading?.Invoke(this, collectTime);
        StartCoroutine(IECollect(amountToCollect, collectTime));
    }
    protected override IEnumerator IECollect(float collectGold, float collectTime)
    {
        yield return new WaitForSeconds(collectTime);
        if(CurrentGold > 0 && CurrentGold < CollectCapacity)
        {
            CurrentGold += collectGold;
        }
        else
        {
            CurrentGold = collectGold;
        }
        currentDeposit.RemoveGold(collectGold);
        //yield return new WaitForSeconds(0.5f);

        if(CurrentGold == CollectCapacity || currentShaftIndex == ShaftManager.Instance.Shafts.Count - 1)
        {
            currentShaftIndex = -1;
            ChangeGoal();
            Vector3 elevatorDepositPos = new Vector3(transform.position.x, elevator.DepositLocation.position.y);
            MoveMiner(elevatorDepositPos);
        }
        else
        {
            MoveToNextLocation();
        }
    }
    protected override void DepositGold()
    {
        if(CurrentGold <= 0)
        {
            currentShaftIndex = -1;
            ChangeGoal();
            MoveToNextLocation();
            return;
        }
        float depositTime = CurrentGold / CollectPerSecond;
        OnLoading?.Invoke(this, depositTime);
        StartCoroutine(IEDeposit(CurrentGold, depositTime));
    }
    protected override IEnumerator IEDeposit(float goldCollected, float depositTime)
    {
        yield return new WaitForSeconds(depositTime);
        elevator.ElevatorDeposit.DepositGold(goldCollected);
        CurrentGold = 0;
        currentShaftIndex = -1;

        ChangeGoal();
        MoveToNextLocation();
    }
    private void ElevatorBoost(ElevatorManagerLocation elevatorManager)
    {
        switch (elevatorManager.Manager.BoostType)
        {
            case BoostType.Movement:
                ManagersController.Instance.RunMovementBoost(this, 
                    elevatorManager.Manager.boostDuration, elevatorManager.Manager.boostValue);
                break;
                
            case BoostType.Loading:
                ManagersController.Instance.RunLoadingBoost(this, 
                    elevatorManager.Manager.boostDuration, elevatorManager.Manager.boostValue);
                break;
        }
    }
    private void OnEnable()
    {
        ElevatorManagerLocation.OnBoost += ElevatorBoost;
    }
    private void OnDisable()
    {
        ElevatorManagerLocation.OnBoost -= ElevatorBoost;
    }


}
