using UnityEngine;

public class TamashiIA : MonoBehaviour
{
    [SerializeField] string type;

    void Start()
    {

        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
        GameObject[] lasers = GameObject.FindGameObjectsWithTag("Laser");

        if (type == "Damage")
        {
            foreach (var weapon in weapons)
            {
                TowerShoot towerShoot = weapon.GetComponent<TowerShoot>();
                if (towerShoot != null)
                    towerShoot.UpgradeTowersWithTamashi();
            }

        }
        else if (type == "Poison")
        {
            foreach (var weapon in weapons)
            {
                TowerShoot towerShoot = weapon.GetComponent<TowerShoot>();
                if (towerShoot != null)
                    towerShoot.UpgradeTowerWIthPoisonTamashi();
            }

        }

    }
}
