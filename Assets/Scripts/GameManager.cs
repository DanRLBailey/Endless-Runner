using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class floatList
    {
        public float[] list;
    }

    public int poolSize;
    public List<Collectable> _collectableList = new List<Collectable>();
    public GameObject collectablePrefab;
    public GameObject ground;
    int offsetIndex;
    public float rotationSpeed;

    [Header("Collectable Percentage Thresholds")]
    [Range(0.0f, 1.0f)]
    public float commonThreshold;
    [Range(0.0f, 1.0f)]
    public float staminaThreshold;

    [Header("Collectable Types")]
    public List<CollectableType> _collectableTypeList = new List<CollectableType>();
    public floatList[] xOffsetLists;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCollectables(); //
        ground.GetComponent<Ground>().rotationSpeed = rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //Collectable temp = FindNextAvailableCollectable();

            //if (temp)
            //    temp.Activate();

            offsetIndex = Random.Range(0, xOffsetLists.Length);

            StartCoroutine(Timer(xOffsetLists[offsetIndex].list.Length - 1, 0, 0.2f));
        }
    }

    public void SpawnCollectables()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject temp = Instantiate(collectablePrefab, new Vector3(5.5f, -5.5f, 0f), Quaternion.identity);
            Collectable collectable = temp.GetComponent<Collectable>();

            collectable.pivot = GameObject.Find("Ground");
            collectable.Deactivate();
            collectable.transform.parent = GameObject.Find("CollectableContainer").transform;
            collectable.SetCollectableType(_collectableTypeList[0]);
            collectable.rotationSpeed = rotationSpeed;

            _collectableList.Add(collectable);
        }
    }

    Collectable FindNextAvailableCollectable(int xOffsetListIndex, int current)
    {
        Collectable toActivate;

        float rand = Random.Range(0.0f, poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            if (!_collectableList[i].isActive)
            {
                    toActivate = _collectableList[i];

                if (rand > poolSize - (poolSize * commonThreshold)) //Common
                    toActivate.SetCollectableType(_collectableTypeList[0]); 
                else if (rand > poolSize - (poolSize * staminaThreshold))
                    toActivate.SetCollectableType(_collectableTypeList[1]);

                toActivate.xOffset = xOffsetLists[xOffsetListIndex].list[current];

                return toActivate;
            }
        }

        return null;
    }

    IEnumerator Timer(int amount, int current, float waitTime)
    {
        Collectable temp = FindNextAvailableCollectable(offsetIndex, current);

        if (temp)
            temp.Activate();

        yield return new WaitForSeconds(waitTime);

        if (current < amount)
        {
            StartCoroutine(Timer(amount, current + 1, waitTime));
        }
        else
        {
            offsetIndex = Random.Range(0, xOffsetLists.Length);

            StartCoroutine(Timer(xOffsetLists[offsetIndex].list.Length - 1, 0, 0.2f));
        }
    }
}
