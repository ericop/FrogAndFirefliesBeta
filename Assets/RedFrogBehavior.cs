using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RedFrogBehavior : MonoBehaviour
{
    int score = 0;
    // Use this for initialization
    void Start()
    {
        var character = GetComponent<Rigidbody2D>();
        var opponentFrog = GameObject.FindWithTag("BlueFrog");
        Physics2D.IgnoreCollision(character.GetComponent<Collider2D>(), opponentFrog.GetComponent<Collider2D>());
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == ("FireflyClone"))
        {
            var fireflyThatWeHit = coll.gameObject;
            Destroy(fireflyThatWeHit);
            score += 1;
            var redScore = GameObject.FindWithTag("RedScore").GetComponent<Text>();
            redScore.text = "Red Fred's Score: " + score + "/20";

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

        if (Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.Return))
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
        winnerText.text = "Red Fred Wins!";
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene("main");
    }

}
