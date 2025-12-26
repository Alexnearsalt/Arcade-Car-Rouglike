using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private GameObject shopCanvas;
    private bool flag;
    public void OpenShop()
    {
        if (!flag)
        {
            shopCanvas = Instantiate(shopCanvas);
            flag = true;
        }
            
        shopCanvas.SetActive(true);
    }

    public void CloseShop()
    {
        shopCanvas.SetActive(false);
    }
}
