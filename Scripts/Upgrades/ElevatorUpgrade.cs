using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorUpgrade : BaseUpgrade
{
    protected override void RunUpgrade()
    {
        elevator.Miner.CollectCapacity *= (int) collectCapacityMultiplier;
        elevator.Miner.CollectPerSecond *= collectPerSecondMultiplier;
        if(CurrentLevel % 10 == 0)
        {
            elevator.Miner.MoveSpeed *= moveSpeedMultiplier;
        }
    }
}
