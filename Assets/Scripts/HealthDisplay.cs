using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField]    private GameObject heartPrefab;
    [SerializeField]    private List<GameObject> heartImages;

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
            life.transform.parent = gameObject.transform;
            heartImages.Add(life);
        }
    }
}
