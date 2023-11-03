using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorUpgrade : BaseUpgrade
{
    protected override void RunUpgrade()
    {
        elevator.Miner.CollectCapacity *= (int) upgradeData.collectCapacityMultiplier;
        elevator.Miner.CollectPerSecond *= upgradeData.collectPerSecondMultiplier;
        if(CurrentLevel % 10 == 0)
        {
            elevator.Miner.MoveSpeed *= upgradeData.moveSpeedMultiplier;
        }
    }
}
