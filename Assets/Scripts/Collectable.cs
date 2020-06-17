using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject pivot;
    public float rotationSpeed;
    public float xOffset;
    public bool isActive;
    public Vector3 startPosition;
    public Vector3 deactivatedPosition;

    [Header("Collectable Type")]
    public CollectableType collectableType;
    public Sprite sprite;
    public string type;
    public int score;
    public float staminaIncrease;

    [Header("Player Values")]
    public PlayerValues playerValues;

    // Start is called before the first frame update
    void Start()
    {
        playerValues = GameObject.Find("GameManager").GetComponent<PlayerValues>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pivot && isActive)
            transform.RotateAround(pivot.transform.position, pivot.transform.up, rotationSpeed);

        if (transform.position.x <= -3f)
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        isActive = true;

        transform.position = startPosition + new Vector3(xOffset, 0f, 0f);
    }

    public void Deactivate()
    {
        isActive = false;

        transform.position = deactivatedPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Deactivate();

            playerValues.AddScore(score);
            playerValues.AddCoins(1);
            playerValues.AddStamina(staminaIncrease);

            if (type == "Common")
            {
                
            }
            else if (type == "Stamina")
            {

            }
        }
    }

    public void SetCollectableType(CollectableType collectType)
    {
        sprite = collectType.sprite;
        type = collectType.type;
        score = collectType.score;
        staminaIncrease = collectType.staminaIncrease;
    }
}
