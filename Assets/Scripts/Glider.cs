using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{

    public bool goingUp;
    public float speed;
    public int enemyNum;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalScreenSize = gameManager.horizontalScreenSize;

        if (enemyNum == 1)
        {
            
            if (goingUp)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else if (goingUp == false)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
        }

        else
        {
            
            if (goingUp)
            {
                transform.Translate(new Vector3(Random.Range(-60f, 60f), 1f, 0) * speed * Time.deltaTime);

            }
            else if (goingUp == false)
            {
                transform.Translate(new Vector3(Random.Range(-40f, 40f), -1f, 0) * speed * Time.deltaTime);

                if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
                {
                    transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
                }
            }
        }

        if (transform.position.y >= gameManager.verticalScreenSize * 1.25f || transform.position.y <= -gameManager.verticalScreenSize * 1.25f)
        {
            Destroy(this.gameObject);
        }
    }
}