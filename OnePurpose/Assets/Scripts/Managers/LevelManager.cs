using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject[] Boulders;
    public GameObject DeathCamera;
    public Text YouDied;
    public float bSpeed = 0f;
    Rigidbody rb;

	void Start ()
    {
        InvokeRepeating("SpawnBoulder", 0f, .2f);
	}
	
	void Update ()
    {

	}

    void SpawnBoulder()
    {
        
            GameObject boulderclone = Instantiate(Boulders[UnityEngine.Random.Range(0,3)], new Vector3(160f, 110f, UnityEngine.Random.Range(-35f, 35f)), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            Destroy(boulderclone, 7.5f);

            rb = boulderclone.GetComponent<Rigidbody>(); //Bad coding <----- Don't care to fix it.
            rb.AddForce(new Vector3(-Mathf.Sqrt(2), -1, 0) * bSpeed, ForceMode.VelocityChange);
    }

    public void Death()
    {
        YouDied.text = "You have been bested by the boulders \n Thank you for playing";
        DeathCamera.SetActive(true);
        
    }

    public void ButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
