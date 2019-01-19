using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject c1;
    public GameObject ForceField;
    public GameObject ShotPrefab;
    private GameObject clone;
    public Text YouWin;
    public Text ForceCd;

    public float maxSpeed;
    public float speedMultiplier;
    public float shotSpeedMulti;
    public float AddedGravMulti = 0f;

    public float fireRate = 1f;
    public float nextFire = 0.0f;
    public float blockRate = 10f;
    public float nextBlock = 0f;
    private float KeyDownTime = float.MinValue;

    public LevelManager LevelMClass;
    private Vector3 shotDirection = new Vector3(Mathf.Sqrt(3), 1f, 0f);
    Rigidbody sp;

	void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }
	void Update ()
    {
        if(Physics.Raycast(this.gameObject.transform.position, Vector3.down, 1.1f) != true)
        {
            rb.AddForce(Vector3.down * AddedGravMulti, ForceMode.VelocityChange);
        }

		if(Input.GetKey(KeyCode.W))
        {

            RotateWithCam();
            rb.AddForce((c1.transform.TransformDirection(Vector3.forward) * speedMultiplier), ForceMode.VelocityChange);
            CapVelo();
        }

        if(Input.GetKey(KeyCode.D))
        {
            rb.AddForce((c1.transform.TransformDirection(Vector3.right) * speedMultiplier), ForceMode.VelocityChange);
            RotateWithCam();
            CapVelo();
        }

        if (Input.GetKey(KeyCode.A))
        {
            RotateWithCam();
            rb.AddForce((c1.transform.TransformDirection(Vector3.left) * speedMultiplier), ForceMode.VelocityChange);
            CapVelo();
        }

        if (Input.GetKey(KeyCode.S))
        {
            RotateWithCam();
            rb.AddForce((c1.transform.TransformDirection(Vector3.back) * speedMultiplier), ForceMode.VelocityChange);
            CapVelo();
        }

        if(Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            FireShot();
        }

        if (Input.GetMouseButton(1) && Time.time > nextBlock)
        {
            nextBlock = Time.time + blockRate;
            KeyDownTime = Time.time;
        }
        if (Time.time < KeyDownTime + 3f)
        {
            ForceField.SetActive(true);
        }
        else
            ForceField.SetActive(false);

        if (KeyDownTime + 10f > Time.time) //Cooldown timer for the forcefield
            ForceCd.text = ((KeyDownTime + 10f) - Time.time).ToString("#.00");
        else
            ForceCd.text = "Ready";

    }


    void CapVelo()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    void RotateWithCam()
    {
        Vector3 targetRotation = c1.transform.TransformDirection(Vector3.forward);
        targetRotation.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
    }

    void FireShot()
    {
        clone = Instantiate(ShotPrefab, new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z), Quaternion.Euler(0f,0f,0f)) as GameObject;
        sp = clone.GetComponent<Rigidbody>();
        sp.AddForce(shotDirection * shotSpeedMulti, ForceMode.VelocityChange);
        Destroy(clone, 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Boulder")
        {
            Destroy(this.gameObject);
            LevelMClass.Death();
        }
        if(other.gameObject.tag == "WinZone")
        {
            YouWin.text = "Congratulations! \n You've Won";
        }

    }

}
