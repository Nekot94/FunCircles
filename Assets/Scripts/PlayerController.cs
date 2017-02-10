using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float speed = 1.0f;
    public Text winText;
    public Text scoreText;
    public int winCount = 40;
    private int count;
    private bool isInvisible;

    private Rigidbody2D _rigidbody;

    // Use this for initialization
    void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        scoreText.text = "Очки:" + count.ToString() + "/" + winCount.ToString();
        isInvisible = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        _rigidbody.AddForce(movement * speed);

    }
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PickUp")) {
            collision.gameObject.SetActive(false);
            count += 1;
            scoreText.text = "Очки:" + count.ToString() + "/" + winCount.ToString();
            if (count >= winCount) {
                winText.text = "Ты победил";
                StartCoroutine(NextScene(7f));
                GetComponent<CircleCollider2D>().enabled = false;
            }
        }

        if (collision.gameObject.CompareTag("InvPotion")) {

            collision.gameObject.SetActive(false);
            StartCoroutine(InvisibleTime(10f));
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Enemy" && !isInvisible) {
            winText.text = "ты проиграл";
            winText.color = Color.red;
            StartCoroutine(Restart(7f));
            GetComponent<CircleCollider2D>().enabled = false;

        }
    }
    private IEnumerator Restart(float time) {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator InvisibleTime(float time) {
        isInvisible = true;
        transform.localScale += new Vector3(2f, 2f, 0);
        yield return new WaitForSeconds(time);
        transform.localScale -= new Vector3(2f, 2f, 0);
        isInvisible = false;
    }
    private IEnumerator NextScene(float time) {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Scene2");
    }
}
