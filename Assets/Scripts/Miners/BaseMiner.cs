using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class BaseMiner : MonoBehaviour
{
    public static Action<BaseMiner, float> OnLoading;

    [SerializeField] protected MinerData minerData;
    public float MoveSpeed { get; set; }
    public float CurrentGold { get; set; }
    public float CollectCapacity { get; set; }
    public float CollectPerSecond { get; set; }
    public bool IsTimeToCollect { get; set; }
    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        IsTimeToCollect = true;
        CurrentGold = 0;
    }
    public virtual void MoveMiner(Vector3 newPosition)
    {
        transform.DOMove(newPosition, 10 / MoveSpeed).OnComplete(() =>
        {
            if (IsTimeToCollect)
            {
                CollectGold();
            }
            else
            {
                DepositGold();
            }
        }).Play();
    }

    protected virtual void CollectGold()
    {
        
    }

    protected virtual IEnumerator IECollect(float collectGold, float collectTime)
    {
        yield return null;
    }
    protected virtual void DepositGold()
    {

    }
    protected virtual IEnumerator IEDeposit(float goldCollected, float depositTime)
    {
        yield return null;
    }
    public void ChangeGoal()
    {
        IsTimeToCollect = !IsTimeToCollect;
    }
    public void RotateMiner(int direction)
    {
        if (direction == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
