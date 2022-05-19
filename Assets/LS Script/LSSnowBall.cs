using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSSnowBall : MonoBehaviour {

    public float ballSpeed;

    private Rigidbody2D theRB;

    public GameObject snowBallEffect;

    // Start is called before the first frame update
    void Start() {
        theRB = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update() {
        theRB.velocity = new Vector2(ballSpeed * transform.localScale.x, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player1")
        {
            FindObjectOfType<LSGameManager>().HurtP1();
        }
        if (other.tag == "Player2")
        {
            FindObjectOfType<LSGameManager>().HurtP2();
        }

        Instantiate(snowBallEffect, transform.position, transform.rotation);
        
        Destroy(gameObject);
    }
}
