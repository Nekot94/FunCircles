using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float speed = 1.0f;
	public Text winText;
	public Text scoreText;
	public int winCount = 5;
	private int count;

    private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        _rigidbody.AddForce(movement * speed);

    }
	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("PickUp")) {
			collision.gameObject.SetActive (false);
			count += 1;
			scoreText.text = "Очки:" + count.ToString () + "/" + winCount.ToString ();
			if (count >= winCount) {
				winText.text = "Ты победил";
				StartCoroutine (Restart (3f));
				}
		}
	}
	private IEnumerator Restart(float time) {
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
