using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerProgress
{
    public int coins = 0;
    public List<string> ownedUpgrades = new List<string>();
    
    //Магазин
    public bool shopLocked = false;
    public int shopPurchasesLeft = 3;
    public List<string> shopOfferIds = new List<string>();
}
