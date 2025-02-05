/*using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class WeatherAppTests
{
    [Test]
    public void TestWeatherApiResponseParsing()
    {
        // Mock the JSON response
        string mockJson = "{\"daily\":{\"temperature_2m_max\":[32.0, 34.5, 33.5]}}";

        // Create an instance of the WeatherResponse class and parse the mock JSON
        WeatherResponse weatherResponse = JsonUtility.FromJson<WeatherResponse>(mockJson);

        // Assert the parsed temperature matches the mock data
        Assert.AreEqual(32.0f, weatherResponse.daily.temperature_2m_max[0]);
        Assert.AreEqual(34.5f, weatherResponse.daily.temperature_2m_max[1]);
    }
}*/
