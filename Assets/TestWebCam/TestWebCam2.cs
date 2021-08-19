using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestWebCam2 : MonoBehaviour
{
    public RawImage rawImage;
    public Button changeCameraBtn;

    private bool mIsFront;

    private WebCamTexture camTexture;

    private void Start()
    {
        changeCameraBtn.onClick.AddListener(() => OpenCamera(mIsFront));
    }

    void OpenCamera(bool isFront)
    {
        mIsFront = !mIsFront;
       foreach (var camd in  WebCamTexture.devices)
        {
            if(camd.isFrontFacing == isFront)
            {
                if (camTexture)
                    DestroyImmediate(camTexture);
                camTexture = new WebCamTexture(camd.name, Screen.width, Screen.height);
                rawImage.texture = camTexture;
                camTexture.Play();
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        if(camTexture)
        {
            GUILayout.Label($"videoVerticallyMirrored={camTexture.videoVerticallyMirrored}");
            GUILayout.Label($"videoRotationAngle={camTexture.videoRotationAngle}");

            rawImage.transform.localRotation = Quaternion.Euler(0, 0, camTexture.videoRotationAngle);
        }
        GUILayout.EndVertical();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
