using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] int maxSpeed = 5;
    [SerializeField] TextMeshProUGUI tmpCurrentSpeed;

    int currentSpeed = 1;
    public void ToggleSpeed()
    {
        if (currentSpeed == maxSpeed)
            currentSpeed = 0;

        currentSpeed++;

        Time.timeScale = currentSpeed;
        tmpCurrentSpeed.text = $"{currentSpeed}x";
    }
}
