using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Boot {
    public class NetResourceLoader : MonoBehaviour {
        public delegate void DownloadHandler(Sprite[] sprites);
        public event DownloadHandler OnDownloadDone;
        private const string url = "https://picsum.photos/120/150";
        private Sprite[] sprites;
        private int currentCount;
        private int targetCount;
        public void Load(int imageCount) {
            targetCount = imageCount;
            Debug.Log("targetCount  = " + targetCount);
            sprites = new Sprite[targetCount];
            for (int i = 0; i < targetCount; i++) {
                StartCoroutine(DownloadImage(url));
            }
        }

        IEnumerator DownloadImage(string MediaUrl) {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError) {

                Debug.Log(request.error);
            } else {
                var tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                AddSprite(Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f));
            }
        }

        private void AddSprite(Sprite sprite) {
            sprites[currentCount] = sprite;
            if (currentCount + 1 > targetCount - 1) {
                Debug.Log("Net loading Done ");
                OnDownloadDone?.Invoke(sprites);
            } else {
                currentCount++;
            }
        }
    }
}