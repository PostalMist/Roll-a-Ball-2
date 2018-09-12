using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    //Variable intialization 
    public float speed;
    public Text countText;
    public Text WinText;
    private Rigidbody rb;
    private int count;
    public LayerMask groundLayers;
    public float jumpForce;
    private SphereCollider col;
   // private GameObject ground;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        count = 0;
        SetCountText();
        WinText.text = "";
       // ground = GameObject.Find("Ground");

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

	}
    void OnTriggerEnter(Collider other)
    {
        // Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if (count >= 12) {
            WinText.text = "You Win!";
           // ground.gameObject.SetActive(false);
        }
    }

    private bool IsGrounded() {

        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayers );
    }
}
