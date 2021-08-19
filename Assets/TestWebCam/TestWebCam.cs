using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestWebCam : MonoBehaviour
{
    WebCamTexture webCamTex;
    CommandBuffer cmd;
    RenderTexture tempTex;
    IEnumerator Start()
    {
        webCamTex = new WebCamTexture(WebCamTexture.devices[1].name,Screen.width, Screen.height);
        webCamTex.Play();
        webCamTex.wrapMode = TextureWrapMode.Repeat;
        tempTex = new RenderTexture(Screen.width, Screen.height,0);
        tempTex.wrapMode = TextureWrapMode.Repeat;
        cmd = new CommandBuffer();
        cmd.name = "WebCam";
        GetComponent<Camera>().AddCommandBuffer(CameraEvent.BeforeForwardOpaque, cmd);
        yield return new WaitForSeconds(1);
    }
    private void OnGUI()
    {
        GUILayout.BeginVertical();
        if (webCamTex)
        {
            GUILayout.Label($"videoVerticallyMirrored={webCamTex.videoVerticallyMirrored}");
            GUILayout.Label($"videoRotationAngle={webCamTex.videoRotationAngle}");
        }
        GUILayout.EndVertical();
    }
    void Update()
    {
       if(webCamTex)
        {
            cmd.Clear();
            cmd.Blit(webCamTex, tempTex);

            Vector2 rotate = Vector2.one;
            if(webCamTex.videoRotationAngle == 180)
            {
                rotate = new Vector2(-1, -1);
            }
            cmd.Blit(tempTex, BuiltinRenderTextureType.None, new Vector2(-1, webCamTex.videoVerticallyMirrored ? -1 : 1) * rotate, Vector2.zero);
        }
    }
}
