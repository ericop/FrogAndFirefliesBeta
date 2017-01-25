using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirefliesBehavior : MonoBehaviour
{
    public GameObject firefly;

    public float speed = 1f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("WaitForNextFirefly");

    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitForNextFirefly()
    {
        while (true)
        {
            AddFirefly();
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    void AddFirefly()
    {
        // Get the renderer component of the spawn object
        var rd = GetComponent<Collider2D>();
        // Position of the left edge of the spawn object
        // It's: (position of the center) minus (half the width)
        var y1 = transform.position.y - rd.bounds.size.y / 2;

        // Same for the right edge
        var y2 = transform.position.y + rd.bounds.size.y / 2;

        // Randomly pick a point within the spawn object
        var spawnPoint = new Vector2(transform.position.x, Random.Range(y1, y2));

        // Create an enemy at the 'spawnPoint' position
        var cloneFirefly = Instantiate(firefly, spawnPoint, transform.rotation);
        cloneFirefly.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(speed, speed * 0.2f), 0);
    }
}
