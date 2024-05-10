using Palmmedia.ReportGenerator.Core.Plugin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject muzzleFlash; 
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float gravity = 9.81f;
    private CharacterController controller;
    [SerializeField]
    private GameObject hitMarkerPrefab;
    [SerializeField]
    private AudioSource weaponAudio;

    private int maxAmmo=200;
    [SerializeField]
    private int currentAmmo;
    private bool reloading = false;
    private UI_Manager ui_manager;
    private bool hasCoin = false;
    [SerializeField]
    private GameObject weapon;

    void Start()
    {
        //Ocultar el cursor
        Cursor.visible = false;
        //Bloquear cursor al centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        currentAmmo = maxAmmo;
        ui_manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
        if (currentAmmo < maxAmmo && Input.GetKeyDown(KeyCode.R) && !reloading) { StartCoroutine(reload()); }
        calculateMoveMent();
    }
    void calculateMoveMent()
    {
        Vector3 direccion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direccion * speed;
        velocity.y -= gravity;
        velocity = transform.transform.TransformDirection(velocity);
        controller.Move(velocity * Time.deltaTime);
        if (Input.GetKey(KeyCode.Escape)) { Cursor.visible = true; Cursor.lockState = CursorLockMode.None; }
    }
    void shoot()
    {
        if (currentAmmo > 0 && Input.GetMouseButton(0) && !reloading)
        {
            muzzleFlash.SetActive(true);
            if (!weaponAudio.isPlaying) { weaponAudio.Play(); }
            currentAmmo--;
            ui_manager.updateAmmo(currentAmmo);
            //Instanciacion del raycast (linea imaginaria disparada desde el centro de la pantalla)
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            //Objeto instanciado cuando el Ray golpea un objeto con collider
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("El rayhit a golpeado a: " + hitInfo.transform.name);
                GameObject hitMarker = Instantiate(hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitMarker, 1f);
                Destructible crate = hitInfo.transform.GetComponent<Destructible>();
                if (crate != null)
                {crate.DestroyBox();}
            }
          
        } else
        {
            weaponAudio.Stop();
            muzzleFlash.SetActive(false);
        }
    }
    IEnumerator reload()
    {
        reloading = true;
        ui_manager.setText("Recargando...");
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        ui_manager.updateAmmo(currentAmmo);
        reloading = false;
    }
    public void setCoin(bool value) { 
        this.hasCoin = value;
        ui_manager.setCoinVisible(value);
    }
    public bool isCoin() { return this.hasCoin; }
    public void setWeapon(bool visible)
    {
        weapon.SetActive(visible);
    }
}
