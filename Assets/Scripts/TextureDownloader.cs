using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TextureDownloader : MonoBehaviour
{
    [SerializeField] private Image targetImage;

    void Start()
    {
        StartCoroutine(GetTexture());
    }


    IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://picsum.photos/200/300");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Success!");
            var texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));

        }
    }
}
