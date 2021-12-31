using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    private int CoinCount;
    [SerializeField] private Text CoinCountText;
    [SerializeField] private GameObject UpgradeA, UpgradeB;

    public void UpdateItems(string type)
    {
        switch (type)
        {
            case "Coin":
                CoinCount++;
                CoinCountText.text = CoinCount.ToString();
                break;
            case "UpgradeA":
                UpgradeA.SetActive(true);
                break;
            case "UpgradeB":
                UpgradeB.SetActive(true);
                break;
            default:

                break;
        }
    }
}

