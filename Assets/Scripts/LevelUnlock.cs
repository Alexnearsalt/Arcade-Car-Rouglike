using System;
using UnityEngine;

public class LevelUnlock : MonoBehaviour
{
    [SerializeField] private int unlockPrice;
    [SerializeField] private GameObject locked;
    private ProgressController _progress;

    private void Awake()
    {
        _progress = ProgressController.Instance;
    }

    public void Unlock()
    {
        if (_progress.Model.TrySpendCoins(unlockPrice)) 
            locked.SetActive(false);
    }
}
