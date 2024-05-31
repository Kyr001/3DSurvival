using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOptions: MonoBehaviour
{
    public GameObject maincamera;
    public GameObject subcamera;

    public void OnSwitchCamera(InputValue value)
    {

        if (value != null)
        {
            if (maincamera.activeSelf)
            {
                Debug.Log($"{subcamera} active");
                maincamera.SetActive(false);
                subcamera.SetActive(true);
            }
            else
            {
                Debug.Log($"{maincamera} active");
                subcamera.SetActive(false);
                maincamera.SetActive(true);
            }
        }
    }

}