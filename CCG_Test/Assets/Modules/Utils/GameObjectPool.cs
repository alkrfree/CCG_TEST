using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour {
    private Queue<GameObject> objectsInPool = new Queue<GameObject>();

    [SerializeField] private bool isCanExpand = true;
    [SerializeField] private Transform poolParent;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialObjectsCount;
    private void Awake() {
        for (int i = 0; i < initialObjectsCount; i++) {
            CreateNewObject();
        }
    }
    public void PoolObject(GameObject obj) {
        obj.SetActive(false);
        objectsInPool.Enqueue(obj);
    }
    public GameObject GetObjectFromPool() {
        if (isCanExpand && objectsInPool.Count == 0) {
            CreateNewObject();
        }
        var obj = objectsInPool.Dequeue();

        obj.GetComponent<IResetable>().Reset();
        obj.gameObject.SetActive(true);
        return obj;
    }

    private void CreateNewObject() {
        PoolObject(Instantiate(prefab, poolParent));
    }
}
