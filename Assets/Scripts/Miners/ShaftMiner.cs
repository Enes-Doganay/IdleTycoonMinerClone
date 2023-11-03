using System.Collections;
using UnityEngine;

public class ShaftMiner : BaseMiner
{
    public Shaft CurrentShaft { get; set; }
    private int miningAnimatorParametor = Animator.StringToHash("Mining");
    private int walkingAnimatorParametor = Animator.StringToHash("Walking");
    public void Initiliaze(MinerData minerData)
    {
        this.minerData = minerData;
        MoveSpeed = minerData.moveSpeed;
        CollectCapacity = minerData.initialCollectCapacity;
        CollectPerSecond = minerData.goldCollectPerSecond;
    }
    public override void MoveMiner(Vector3 newPosition)
    {
        base.MoveMiner(newPosition);
        animator.SetTrigger(walkingAnimatorParametor);
    }
    protected override void CollectGold()
    {
        float collectTime = CollectCapacity / CollectPerSecond;
        OnLoading?.Invoke(this, collectTime);
        animator.SetTrigger(miningAnimatorParametor);
        StartCoroutine(IECollect(CollectCapacity, collectTime));
    }
    protected override IEnumerator IECollect(float collectGold, float collectTime)
    {
        yield return new WaitForSeconds(collectTime);

        CurrentGold = collectGold;
        ChangeGoal();
        RotateMiner(-1);
        MoveMiner(CurrentShaft.DepositLocation.position);
    }
    protected override void DepositGold()
    {
        CurrentShaft.CurrentDeposit.DepositGold(CurrentGold);
        CurrentGold = 0;
        ChangeGoal();
        RotateMiner(1);
        MoveMiner(CurrentShaft.MiningLocation.position);
    }
}