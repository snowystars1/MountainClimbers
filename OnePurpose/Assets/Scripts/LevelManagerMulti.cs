using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelManagerMulti : NetworkBehaviour
{

    public GameObject[] Boulders;
    public float bSpeed = 0f;
    Rigidbody rb;

    void Start()
    {

        InvokeRepeating("SpawnBoulder", 0f, .2f);
    }

    void SpawnBoulder()
    {

        GameObject boulderclone = Instantiate(Boulders[UnityEngine.Random.Range(0, 3)], new Vector3(160f, 110f, UnityEngine.Random.Range(-37f, 38f)), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        rb = boulderclone.GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(-Mathf.Sqrt(2), -1, 0) * bSpeed, ForceMode.VelocityChange);
        rb.velocity = new Vector3(-Mathf.Sqrt(2), -1, 0) * bSpeed;
        NetworkServer.Spawn(boulderclone);
        Destroy(boulderclone, 7.5f);
    }
    //public void Death()
    //{
    //    //YouDied.text = "You have been bested by the boulders \n Thank you for playing";
    //    //DeathCamera.SetActive(true);

    //}

    public void ButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
