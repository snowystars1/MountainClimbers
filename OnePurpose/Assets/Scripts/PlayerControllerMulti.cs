using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerControllerMulti : NetworkBehaviour
{
    Rigidbody rb;
    public GameObject ForceField;
    public GameObject ShotPrefab;
    private GameObject clone;
    private Text YouWin;
    private Text ForceCd;
    public GameObject MainCamera;

    public float maxSpeed;
    public float speedMultiplier;
    public float shotSpeedMulti;
    public float AddedGravMulti = 0f;

    public float fireRate = 1f;
    public float nextFire = 0.0f;
    public float blockRate = 10f;
    public float nextBlock = 0f;
    private float KeyDownTime = float.MinValue;
    //
    private LevelManagerMulti LevelMClass;
    private Vector3 shotDirection = new Vector3(Mathf.Sqrt(3), 1f, 0f);
    Rigidbody sp;

    void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        YouWin = GameObject.Find("Canvas/YouWin").GetComponent<Text>() as Text;
        //LevelMClass = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManagerMulti>();
        ForceCd = GameObject.Find("Canvas/ForceCooldown").GetComponent<Text>() as Text;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            MainCamera.SetActive(false);
            return;
        }

        ForceField.transform.position = transform.position;

        if (Physics.Raycast(this.gameObject.transform.position, Vector3.down, 1.1f) != true)
        {
            rb.AddForce(Vector3.down * AddedGravMulti, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.W))
        {

            RotateWithCam();
            rb.AddForce((MainCamera.transform.TransformDirection(Vector3.forward) * speedMultiplier), ForceMode.VelocityChange);
            CapVelo();
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce((MainCamera.transform.TransformDirection(Vector3.right) * speedMultiplier), ForceMode.VelocityChange);
            RotateWithCam();
            CapVelo();
        }

        if (Input.GetKey(KeyCode.A))
        {
            RotateWithCam();
            rb.AddForce((MainCamera.transform.TransformDirection(Vector3.left) * speedMultiplier), ForceMode.VelocityChange);
            CapVelo();
        }

        if (Input.GetKey(KeyCode.S))
        {
            RotateWithCam();
            rb.AddForce((MainCamera.transform.TransformDirection(Vector3.back) * speedMultiplier), ForceMode.VelocityChange);
            CapVelo();
        }

        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            CmdFireShot();
        }

        if (Input.GetMouseButtonDown(1) && Time.time > nextBlock)
        {
            nextBlock = Time.time + blockRate;
            KeyDownTime = Time.time;
            CmdForceFieldActivate();
        }

        //CmdForceFieldActivate();

        if (KeyDownTime + 10f > Time.time) //Cooldown timer for the forcefield
        {
            ForceCd.text = ((KeyDownTime + 10f) - Time.time).ToString("#.00");
        }
        else
        {
            ForceCd.text = "Ready";
        }
    }


    void CapVelo()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    void RotateWithCam()
    {
        Vector3 targetRotation = MainCamera.transform.TransformDirection(Vector3.forward);
        targetRotation.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
    }

    [Command]
    void CmdFireShot()
    {
        clone = Instantiate(ShotPrefab, new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        sp = clone.GetComponent<Rigidbody>();
        //sp.AddForce(shotDirection * shotSpeedMulti, ForceMode.VelocityChange);
        sp.velocity = shotDirection * shotSpeedMulti;
        NetworkServer.Spawn(clone);
        Destroy(clone, 3.0f);
    }

    [Command]
    void CmdForceFieldActivate()
    {
        //if (Time.time < KeyDownTime + 3f)
        //{
            GameObject clone = Instantiate(ForceField, this.gameObject.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            NetworkServer.Spawn(clone);
            Destroy(clone, 3f);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boulder")
        {
            transform.position = new Vector3(-90f,55f,0f);
            //LevelMClass.Death();
        }
        if (other.gameObject.tag == "WinZone")
        {
            YouWin.text = "Congratulations! \n You've Won";
        }

    }
}