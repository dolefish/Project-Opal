using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateModule : MonoBehaviour {

    [Header("Instantiate Settings")]
    public List<GameObject> civilian_Prefabs = new List<GameObject>();
    public float spawnCooldown = 2f;
    public Vector3 spawnLocation;

    [Header("Directions")]
    public bool goUp = false;
    public bool goRight = false;
    public bool goDown = false;
    public bool goLeft = false;

    private int civilianCount = 0;
    private bool isSpawning = false;



    void Start ()
    {
        civilianCount = civilian_Prefabs.Count;
    }
	

	void Update ()
    {
        if (InstantiateHQ.currentCivilian < InstantiateHQ.maxCivilian && isSpawning == false)
        {
            StartCoroutine(SpawnCivilian());
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnCivilian());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Civilian")
        {
            InstantiateHQ.currentCivilian--;
            Destroy(other.gameObject);
        }
    }

    IEnumerator SpawnCivilian()
    {
        isSpawning = true;

        while (InstantiateHQ.currentCivilian < InstantiateHQ.maxCivilian)
        {
            bool direction = false;

            int rng = Random.Range(0, civilianCount);

            yield return new WaitForSecondsRealtime(spawnCooldown);

            GameObject civ = (GameObject)Instantiate(civilian_Prefabs[rng], spawnLocation, Quaternion.identity);

            for (int i = 0; direction == false; i++)
            {
                if (goUp)
                {
                    civ.GetComponent<Rigidbody2D>().velocity = Vector2.up * civ.GetComponent<Civilian>().moveSpeed;
                    civ.GetComponent<Civilian>().dir = 0;
                    direction = true;
                }
                else if (goRight)
                {
                    civ.GetComponent<Rigidbody2D>().velocity = Vector2.right * civ.GetComponent<Civilian>().moveSpeed;
                    civ.GetComponent<Civilian>().dir = 1;
                    direction = true;
                }
                else if (goDown)
                {
                    civ.GetComponent<Rigidbody2D>().velocity = Vector2.down * civ.GetComponent<Civilian>().moveSpeed;
                    civ.GetComponent<Civilian>().dir = 2;
                    direction = true;
                }
                else if (goLeft)
                {
                    civ.GetComponent<Rigidbody2D>().velocity = Vector2.left * civ.GetComponent<Civilian>().moveSpeed;
                    civ.GetComponent<Civilian>().dir = 3;
                    direction = true;
                }
                else
                {
                    print("Instantiate module has not been assigned a direction, so has been removed from runtime.");
                    Destroy(this.gameObject);
                }

            }

            direction = false;

            InstantiateHQ.currentCivilian++;
        }

        isSpawning = false;
    }
}
