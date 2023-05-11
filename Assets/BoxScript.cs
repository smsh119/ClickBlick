using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    [SerializeField] GameObject destroyParticle;

    float speed;
    float xDir;
    float yDir;
    float noEffectRadius;

    Collider2D thisCol;
    GameManager gm;

    void Start()
    {
        thisCol = GetComponent<Collider2D>();
        thisCol.enabled = false;
        
        //variable initialization from GameManager Script

        gm = FindObjectOfType<GameManager>();
        speed = gm.getSpeed();
        //xDir = gm.getXDir();
        //yDir = gm.getYDir();
        noEffectRadius = gm.getNoEffectRadius();

        //setting direction of the box
        xDir = Random.Range(-1f, 1f);
        yDir = Random.Range(-1f, 1f);
        //Debug.Log("xDir = " + xDir + "   yDir = " + yDir);

    }

    void Update()
    {
        transform.Translate(new Vector2(xDir, yDir) * speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, Vector2.zero) > noEffectRadius && !thisCol.enabled)
        {
            thisCol.enabled = true;
        }
        if (Vector2.Distance(transform.position, Vector2.zero) > 10f)
        {
            gm.ProcessGameOver();
            Destroy(gameObject);
        }


    }
    private void OnMouseDown()
    {
        //print("clicked on the box");
        gm.setScore(1);
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
