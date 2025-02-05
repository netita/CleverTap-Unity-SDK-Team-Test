using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomTemplate : MonoBehaviour
{
    public Button ToastSnackbarBut;
    public TMP_Text MessageText;

    public void ShowMessage(string message)
    {
        if (MessageText != null)
        {
            MessageText.text = message; // Update UI text on all platforms
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast"))
            {
                currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    using (AndroidJavaObject toast = toastClass.CallStatic<AndroidJavaObject>(
                        "makeText", currentActivity, message, toastClass.GetStatic<int>("LENGTH_SHORT")))
                    {
                        toast.Call("show");
                    }
                }));
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error showing Toast: " + e.Message);
        }
#endif

#if UNITY_EDITOR
        Debug.Log("Toast Message: " + message);
#endif
    }
}



