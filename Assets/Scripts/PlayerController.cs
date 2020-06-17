using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody body;
    public float jumpForce;
    bool canJump;
    public GameObject pivot;
    bool isHurt;
    public float hurtTime;

    // Start is called before the first frame update
    void Start()
    {
        isHurt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (body.velocity == Vector3.zero)
        {
            canJump = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (canJump && !isHurt)
            {
                canJump = false;
                body.AddForce(Vector3.up * jumpForce);
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (!isHurt)
                StartCoroutine(HurtPlayer());
        }
    }

    IEnumerator HurtPlayer()
    {
        isHurt = true;

        yield return new WaitForSeconds(hurtTime);

        isHurt = false;
    }
}
