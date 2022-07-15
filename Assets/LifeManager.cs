using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LifeManager : MonoBehaviour
{
    [SerializeField] List<GameObject> lifes;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;

        var currentLife = lifes.LastOrDefault();
        currentLife.SetActive(false);
        lifes.Remove(currentLife);
        Destroy(other.gameObject);
    }
}
