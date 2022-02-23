using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField]    private GameObject heartPrefab;
    [SerializeField]    private List<GameObject> heartImages;

    private void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        if (SceneManager.GetActiveScene().name != "Level_0") //loading only if it is not the first level TODO clean stats in the end
        {
            while(heartImages.Count > 0)
            {
                RemoveHP(); //clear the list
            }

            AddHP(StatsKeeper.HealthPointsCount);
        }    
    }

    void OnSceneUnloaded(Scene current) //called before next level is loaded
    {
        StatsKeeper.HealthPointsCount = heartImages.Count;
        Debug.Log("HealthPointsCount saved = " + StatsKeeper.HealthPointsCount);
    } 

    public void RemoveHP()
    {
        if (heartImages.Count > 0)
        {
            Destroy(heartImages[heartImages.Count - 1]);
            heartImages.RemoveAt(heartImages.Count - 1);
        }
    }

    public void AddHP(int healthPointsCount)
    {
        var i = new int[healthPointsCount];

        for(var e = 0; e < i.Length; e++)
        {
            GameObject life = Instantiate(heartPrefab);
            life.transform.SetParent(gameObject.transform);
            heartImages.Add(life);
        }
    }
}
