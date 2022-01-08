using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Boot {
    public class BootInitializer : MonoBehaviour {
        [SerializeField] private NetResourceLoader netResourceLoader;
        [SerializeField] private Vector2Int cardCountRange;
        private Sprite[] sprites;
        private bool isNetImagesLoaded = false;
        void Start() {
            StartCoroutine(LoadingProgress());
            netResourceLoader.OnDownloadDone += DownloadDone;
            netResourceLoader.Load(Random.Range(cardCountRange.x, cardCountRange.y));
        }
        private IEnumerator LoadingProgress() {
            yield return new WaitUntil(() => isNetImagesLoaded);
            MainGame.CardDataManager.Instance.InitCardDataByImages(sprites);
            SceneManager.LoadScene(1);
        }

        private void DownloadDone(Sprite[] sprites) {
            this.sprites = sprites;
            isNetImagesLoaded = true;
        }
        private void OnDestroy() {
            netResourceLoader.OnDownloadDone -= DownloadDone;
        }
    }
}