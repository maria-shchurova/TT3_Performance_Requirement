using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    private int CoinCount;
    [SerializeField] private Text CoinCountText;
    [SerializeField] private GameObject UpgradeA, UpgradeB;

    private PlayerCharacter player;

    private void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
    }
    public void UpdateItems(string type)
    {
        switch (type)
        {
            case "RegularCoin":
                CoinCount++;
                break;
            case "MediumCoin":
                CoinCount += 5;
                break;
            case "LargeCoin":
                CoinCount += 10;
                break;
            case "UpgradeA":
                UpgradeA.SetActive(true);
                player.UpgradeA = true;
                break;
            case "UpgradeB":
                UpgradeB.SetActive(true);
                player.UpgradeB = true;
                break;
            default:
                break;

        }
        CoinCountText.text = CoinCount.ToString();
    }
}

