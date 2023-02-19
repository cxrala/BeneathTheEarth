using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public int characterID = 0;

    public Player owner;
    public int[] hitpoint;
    public int[] maxHitpoint;
    public bool[] upgraded;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Reset();
            instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Reset()
    {
        Debug.Log("Reset Called");
        //hitpoint = new int[] { 14, 14, 14, 14 };
        //maxHitpoint = new int[] { 14, 14, 14, 14 };
        //upgraded = new bool[] { false, false, false, false };
        characterID = 0;
    }

    public void Respawn()
    {
        Debug.Log("Respawn Called");
        //hitpoint = new int[] { 14, 14, 14, 14 };
        //maxHitpoint = new int[] { 14, 14, 14, 14 };
        //upgraded = new bool[] { false, false, false, false };
        characterID = 0;
    }

    public int[] GetHitpoint()
    {
        return hitpoint;
    }

    public void SetHitpoint(int id, int hp)
    {
        hitpoint[id] = hp;
    }
}
