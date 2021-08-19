using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Unity 摄像头有两个值
///  GUILayout.Label($"videoVerticallyMirrored={camTexture.videoVerticallyMirrored}");
///  GUILayout.Label($"videoRotationAngle={camTexture.videoRotationAngle}");
///  注意当界面可以自由旋转的时候，videoRotationAngle为0的时候，明显是手机或者平板的真正的 对齐的时候，
///  这个时候texture可以在手机界面上直接放而不用旋转矫正，其他方向都需要 90 180 270 这种旋转之后才能跟手机传感器和屏幕校准对齐。
///  videoVerticallyMirrored就是垂直方向是否镜像，最明显的理解就是当videoRotationAngle为0的时候这个时候手机屏幕“界面”和摄像头方向是对齐的，
///  如果照片上下颠倒那么说明照片上下镜像了，这个时候videoVerticallyMirrored正好为true。
/// 
/// android 有个问题很奇怪，背面摄像头左右正常，而前置摄像头左右感觉反向，且没有属性提醒这个东西，感觉怪怪的。
/// 
/// </summary>
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
