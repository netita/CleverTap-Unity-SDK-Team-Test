using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class WeatherManager : MonoBehaviour
{
    public CustomTemplate customTemplate;//attach from Resources folder exported CustomTemplate Unity package
    //public TextMeshProUGUI weatherText;//used only for test

    private const string ApiUrl = "https://api.open-meteo.com/v1/forecast?latitude=19.07&longitude=72.87&timezone=IST&daily=temperature_2m_max";
    private const string LocationUrl = "https://nominatim.openstreetmap.org/reverse?format=json";

    private string weatherReportToday;
    private string weatherReportTomorrow;
    private const string customTemplateName= "CustomTemplate";

    private void Start()
    {
        LoadCustomTemplate();
    }

    public void FetchWeatherData() => StartCoroutine(GetWeatherDataCoroutine());

    public void GetLocationName(float latitude, float longitude, Action<string> callback)
    {
        StartCoroutine(FetchLocationName(latitude, longitude, callback));
    }

    private IEnumerator FetchLocationName(float lat, float lon, Action<string> callback)
    {
        string url = $"{LocationUrl}&lat={lat}&lon={lon}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                LocationData locationData = JsonUtility.FromJson<LocationData>(json);

                string location = GetLocation(locationData.address);
                //customTemplate.ShowMessage($"Location: {location}, {weatherReportToday}, {weatherReportTomorrow}");
                Toast.Show($"Location: {location}, {weatherReportToday}, {weatherReportTomorrow}");
                callback?.Invoke(location);
            }
            else
            {
                Debug.LogError("Error fetching location: " + request.error);
                callback?.Invoke("Error fetching location");
            }
        }
    }

    private string GetLocation(Address address)
    {
        return !string.IsNullOrEmpty(address.country) ? address.country :
               !string.IsNullOrEmpty(address.city) ? address.city :
               address.town ?? address.village ?? "Unknown location";
    }

    private IEnumerator GetWeatherDataCoroutine()
    {
       // weatherText.text = "Fetching Weather...";

        using (UnityWebRequest request = UnityWebRequest.Get(ApiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
               // weatherText.text = "Failed to load weather data";
                Debug.LogError("Weather API request failed: " + request.error);
            }
            else
            {
                HandleWeatherResponse(request.downloadHandler.text);
            }
        }
    }

    private void HandleWeatherResponse(string jsonResponse)
    {
        WeatherResponse weatherResponse = JsonUtility.FromJson<WeatherResponse>(jsonResponse);

        if (weatherResponse.daily.time.Length < 2)
        {
           // weatherText.text = "Weather data is incomplete.";
            return;
        }

        string todayDate = weatherResponse.daily.time[0];
        float todayTemp = weatherResponse.daily.temperature_2m_max[0];

        string tomorrowDate = weatherResponse.daily.time[1];
        float tomorrowTemp = weatherResponse.daily.temperature_2m_max[1];

        weatherReportToday = $"Today: {todayTemp}°C";
        weatherReportTomorrow = $"Tomorrow: {tomorrowTemp}°C";

        string weatherReport = $"Weather Forecast:\n{weatherReportToday}\n{weatherReportTomorrow}";
        //weatherText.text = weatherReport;

        GetLocationName(weatherResponse.latitude, weatherResponse.longitude, location =>
        {
            Debug.Log($"Location: {location}");
        });
    }

    private void LoadCustomTemplate()
    {
        if (customTemplate != null)
        {
            CustomTemplate customTemplateObj = Instantiate(customTemplate, Vector3.zero, Quaternion.identity);
            customTemplateObj.GetComponent<CustomTemplate>().ToastSnackbarBut.onClick.AddListener(FetchWeatherData);
            Debug.Log("Prefab loaded and instantiated.");
        }
        else
        {
            Debug.LogError("Prefab not found in Resources folder.");
        }
    }

    [System.Serializable]
    public class WeatherResponse
    {
        public float latitude;
        public float longitude;
        public DailyWeather daily;

        [System.Serializable]
        public class DailyWeather
        {
            public string[] time;
            public float[] temperature_2m_max;
        }
    }

    [System.Serializable]
    public class LocationData
    {
        public Address address;
    }

    [System.Serializable]
    public class Address
    {
        public string country;
        public string city;
        public string town;
        public string village;
    }
}
