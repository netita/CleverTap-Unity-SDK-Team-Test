using UnityEngine;

public class LocationManager : MonoBehaviour
{
    private void Start()
    {
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
        }
    }

    private void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;
            GetWeatherData(latitude, longitude);
        }
    }

    private void GetWeatherData(float latitude, float longitude)
    {
        // API Call Logic here (see next step)
    }
}

