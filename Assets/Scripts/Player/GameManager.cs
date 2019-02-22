using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float CurrentTime=0;
    public float timer = 0;
    public int GuestNumber;
    int failedAttempts=1;
    int Attempts = 0;
    private int spawnTime=3;
    private int gameTime=120;
    float spawnChance;
    public float multiplicadorMestreBebida=1;
    public float multiplicadorMestreHumor=1;
    public float multiplicadorMestreFome=1;
    public List<GameObject> Guests = new List<GameObject>();
    public List<GameObject> GuestList = new List<GameObject>();
    public List<int> indexList = new List<int>();
    // Use this for initialization


    bool stopBegginingSapwn = false;
    bool firstGuestSpawn = true;
    static GameManager instance;
    public static GameManager Instance { get { return instance;} }

    void Start () {
		GuestNumber= Random.Range(8, 12);
        Attempts = (gameTime/5) / spawnTime;
    }
	
	// Update is called once per frame
	void Update () {
        CurrentTime = CurrentTime + Time.deltaTime;

        BegginingSection();

    }
    void BegginingSection()
    {        
        timer = timer + Time.deltaTime;
        if (Mathf.FloorToInt(timer)==spawnTime && !stopBegginingSapwn && CurrentTime<=120f)
        {
            timer = 0;
            int rand = Random.Range(0,100);
            spawnChance = SpawnChanceGen();
            //Debug.Log(spawnChance);
            if (rand<= spawnChance || firstGuestSpawn)
            {
                firstGuestSpawn = false;
                SpawnGuest();
                failedAttempts = 1;
            }
            else
            {
                failedAttempts++;
            }
            Attempts--;
            
        }
    }
    void SpawnGuest()
    {
        for (int i = 0 ; i < Random.Range(1, 3); i++)
        {
            int randomIndex = Random.Range(0,Guests.Count);
            GameObject NewGuest = Instantiate(Guests[randomIndex], (new Vector3(Random.Range(9,40),1,-10)), Quaternion.identity);
            GuestList.Add(NewGuest);
            Guests.Remove(Guests[randomIndex]);
            if (GuestList.Count == GuestNumber)
            {
                stopBegginingSapwn = true;
            }
            
        }
    }
    float SpawnChanceGen()
    {
        float guest = GuestNumber - GuestList.Count;
        //Debug.Log(guest);
        float x = Attempts / guest;
        //Debug.Log(x);
        float y = failedAttempts / x;
        //Debug.Log(y);
        float z = y * 100;
        return (z);
    }
}
