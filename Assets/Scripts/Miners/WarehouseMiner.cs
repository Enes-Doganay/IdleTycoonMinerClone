using System.Collections;
using UnityEngine;

public class WarehouseMiner : BaseMiner
{
    public Deposit ElevatorDeposit { get; set; }
    public Transform ElevatorDepositLocation { get; set; }
    public Transform WarhouseLocation { get; set; }

    private readonly int walkingNoGold = Animator.StringToHash("WalkingNoGold");
    private readonly int walkingWithGold = Animator.StringToHash("WalkingWithGold");
    private LoadBar loadBar;
    private void Start()
    {
        loadBar = GetComponent<LoadBar>();
        Initiliaze();
    }
    public void Initiliaze()
    {
        MoveSpeed = minerData.moveSpeed;
        CollectCapacity = minerData.initialCollectCapacity;
        CollectPerSecond = minerData.goldCollectPerSecond;
        RotateMiner(-1);
        animator.SetBool(walkingNoGold, true);
        MoveMiner(new Vector2(ElevatorDepositLocation.position.x, transform.position.y));
    }
    protected override void CollectGold()
    {
        if(ElevatorDeposit.CurrentGold <= 0)
        {
            RotateMiner(1);
            ChangeGoal();
            MoveMiner(new Vector2(WarhouseLocation.position.x, transform.position.y));
            return;
        }
        animator.SetBool(walkingNoGold, false);

        float currentGold = ElevatorDeposit.CollectGold(this);
        float collectTime = CollectCapacity / CollectPerSecond;
        loadBar.BarContainer.localScale = new Vector3(-1, 1, 1);
        OnLoading?.Invoke(this, collectTime);
        StartCoroutine(IECollect(currentGold, collectTime));
    }
    protected override IEnumerator IECollect(float collectGold, float collectTime)
    {
        yield return new WaitForSeconds(collectTime);

        if(ElevatorDeposit.CurrentGold == collectGold)
            CurrentGold = collectGold;
        else
            CurrentGold = ElevatorDeposit.CurrentGold;

        ElevatorDeposit.RemoveGold(CurrentGold);
        if(CurrentGold > 0)
            animator.SetBool(walkingWithGold, true);
        else
            animator.SetBool(walkingWithGold, false);

        RotateMiner(1);
        ChangeGoal();
        MoveMiner(new Vector2(WarhouseLocation.position.x,transform.position.y));
    }
    protected override void DepositGold()
    {
        if(CurrentGold <= 0)
        {
            RotateMiner(-1);
            ChangeGoal();
            MoveMiner(new Vector2(ElevatorDepositLocation.position.x, transform.position.y));
            return;
        }
        animator.SetBool(walkingWithGold, false);
        animator.SetBool(walkingNoGold, false);
        float depositTime = CurrentGold / CollectPerSecond;
        loadBar.BarContainer.localScale = new Vector3(1, 1, 1);
        OnLoading?.Invoke(this, depositTime);
        StartCoroutine(IEDeposit(CurrentGold, depositTime));
    }
    protected override IEnumerator IEDeposit(float goldCollected, float depositTime)
    {
        yield return new WaitForSeconds(depositTime);
        GoldManager.Instance.AddGold(CurrentGold);
        CurrentGold = 0;
        RotateMiner(-1);
        ChangeGoal();
        MoveMiner(new Vector2(ElevatorDepositLocation.position.x, transform.position.y));
        animator.SetBool(walkingNoGold, true);
    }
}