using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadBar : MonoBehaviour
{
    [SerializeField] private GameObject loadingBarPrefab;
    [SerializeField] private Transform loadingBarPosition;
    public Transform BarContainer { get; set; }

    private Image fillImage;
    private BaseMiner miner;
    private GameObject barCanvas;
    private void Start()
    {
        miner= GetComponent<BaseMiner>();
        CreateLoadBar();
        BarContainer = barCanvas.transform;
    }
    private void CreateLoadBar()
    {
        barCanvas = Instantiate(loadingBarPrefab, loadingBarPosition.position, Quaternion.identity);
        barCanvas.transform.SetParent(transform);
        fillImage = barCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
    }
    private void LoadingBar(BaseMiner minerSender, float duration)
    {
        if(miner == minerSender)
        {
            barCanvas.SetActive(true);
            fillImage.fillAmount = 0f;
            fillImage.DOFillAmount(1f, duration).OnComplete(() => barCanvas.SetActive(false));
        }
    }
    private void OnEnable()
    {
        BaseMiner.OnLoading += LoadingBar;
    }
    private void OnDisable()
    {
        BaseMiner.OnLoading -= LoadingBar;
    }
}
