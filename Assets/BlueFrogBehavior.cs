using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlueFrogBehavior : MonoBehaviour
{
    int score = 0;
    // Use this for initialization
    void Start()
    {
        // Still Not needed, even RedFrog's IgnoreCollision is take care of in the RedFrogBehavior.Start()
        var character = GetComponent<Rigidbody2D>();
        var opponentFrog = GameObject.FindWithTag("RedFrog");
        Physics2D.IgnoreCollision(character.GetComponent<Collider2D>(), opponentFrog.GetComponent<Collider2D>());
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == ("FireflyClone"))
        {
            var fireflyThatWeHit = coll.gameObject;
            Destroy(fireflyThatWeHit);
            score += 1;
            var redScore = GameObject.FindWithTag("BlueScore").GetComponent<Text>();
            redScore.text = "Blue Hugh's Score: " + score + "/20";
            if (score >= 20)
            {
                StartCoroutine("YouWin");
            }
        }

        if (coll.gameObject.tag == ("FrogOverBoard"))
        {
            StartCoroutine("YouWin");
        }
    }

    // Update is called once per frame
    void Update()
    {
        var character = GetComponent<Rigidbody2D>();
        if (character.velocity.y == 0)
        {
            GetComponent<Animator>().speed = 0;
        }
        if (character.velocity.y > 0)
        {
            GetComponent<Animator>().speed = 3f;
        }
        if (character.velocity.y < 0)
        {
            GetComponent<Animator>().speed = 1f;
        }

        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.Space))
        {
            Leap();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Leap()
    {
        Vector2 position = transform.position;
        var character = GetComponent<Rigidbody2D>();
        var startSide = character.position.x > 0 ? "right" : "left";

        if (character.velocity == new Vector2(0, 0))
        {
            if (startSide == "right")
            {
                Vector2 jumpLeft = new Vector2(-3, 5);
                character.AddForce(jumpLeft * 99f);
            }
            if (startSide == "left")
            {
                Vector2 jumpRight = new Vector2(3, 5);
                character.AddForce(jumpRight * 99f);
            }
        }
    }

    IEnumerator YouWin()
    {
        var winnerText = GameObject.FindWithTag("WinnerIs").GetComponent<Text>();
        winnerText.text = "Blue Hugh Wins!";
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene("main");
    }
}