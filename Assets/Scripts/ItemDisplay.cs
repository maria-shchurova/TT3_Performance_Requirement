using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemDisplay : MonoBehaviour
{
    private int CoinCount;
    private bool upgradeAbool, upgradeBbool; // are needed for saving data, because gameobject themselves are destroyed before OnSceneUnoaded() is called

    [SerializeField] private Text CoinCountText;
    [SerializeField] private GameObject UpgradeA, UpgradeB;

    private PlayerCharacter player;

    private void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        //loading stats
        if (SceneManager.GetActiveScene().name != "Level_0") //loading only if it is not the first level TODO clean stats in the end
        {
            CoinCount = StatsKeeper.coinCount;
            CoinCountText.text = CoinCount.ToString();

            UpgradeA.SetActive(StatsKeeper.Upgrade_A);
            player.UpgradeA = upgradeAbool = StatsKeeper.Upgrade_A;

            UpgradeB.SetActive(StatsKeeper.Upgrade_B);
            player.UpgradeB = upgradeBbool = StatsKeeper.Upgrade_B;
        }
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
                player.UpgradeA = upgradeAbool = true;
                break;
            case "UpgradeB":
                UpgradeB.SetActive(true);
                player.UpgradeB = upgradeBbool = true;
                break;                        
            case "UpdateA_Lost":
                UpgradeA.SetActive(false);
                upgradeAbool = false;
                break;            
            case "UpdateB_Lost":
                UpgradeB.SetActive(false);
                upgradeBbool = false;
                break;
            default:
                break;

        }
        CoinCountText.text = CoinCount.ToString();
    }

    void OnSceneUnloaded(Scene current) //called before next level is loaded
    {
        StatsKeeper.coinCount = CoinCount;
        StatsKeeper.Upgrade_A = upgradeAbool;
        StatsKeeper.Upgrade_B = upgradeBbool;

        Debug.Log(current.name + " is unloaded");
    }
}

