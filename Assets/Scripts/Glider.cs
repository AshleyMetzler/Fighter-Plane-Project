using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{

    public bool goingUp;
    public float speed1;
    public float speed2;
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
        if (enemyNum == 1)
        {
            
            if (goingUp)
            {
                transform.Translate(Vector3.up * speed1 * Time.deltaTime);
            }
            else if (goingUp == false)
            {
                transform.Translate(Vector3.down * speed1 * Time.deltaTime);
            }
        }

        else
        {
            
            if (goingUp)
            {
                transform.Translate(new Vector3(Random.Range(-30f, 30f), 1f, 0) * speed2 * Time.deltaTime);

            }
            else if (goingUp == false)
            {
                transform.Translate(new Vector3(Random.Range(-30f, 30f), -1f, 0) * speed2 * Time.deltaTime);
            }
        }

        if (transform.position.y >= gameManager.verticalScreenSize * 1.25f || transform.position.y <= -gameManager.verticalScreenSize * 1.25f)
        {
            Destroy(this.gameObject);
        }
    }
}