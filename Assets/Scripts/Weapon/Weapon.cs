using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public Camera cam;
    [Header("Shooting")]
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;
    public float spreadIntensity = 0.1f;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletVelocity = 30f;// How fast the bullet flies
    public float bulletTime = 3f;// How long until the bullet is destroyed

    [Header("References")]
    private Animator anim;

    private void Awake()
    {
        readyToShoot = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Left Mouse CLick to shoot
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
    }
    private void FireWeapon()
    {
        anim.SetTrigger("RECOIL");
        SoundManager.instance.gunSound.Play();
        readyToShoot = false;
        Vector3 shootingDirection = CalculateDirection().normalized;
        //Instantiate the bullet at the firePoint position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.transform.forward = shootingDirection;//Align the bullet forward direction to the shooting direction
        // Let the bullet fly
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        // Destroy the bullet after 3 seconds
        StartCoroutine(DestroyBullet(bullet, bulletTime));
        // shoot the bullet again
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;// Can shoot again
        allowReset = true;// Can reset again
    }
    public Vector3 CalculateDirection()
    {
        // Shooting from the middle of the screen (like civilized people)
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;// The point the player is aiming at
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;// Hit something solid
        }
        else
        {   
            // Shooting at hopes and dreams
            targetPoint = ray.GetPoint(100);//If nothing is hit, shoot at a point 100 units away
        }
        Vector3 direction = targetPoint - firePoint.position;

        // Add some spread because perfect accuracy is for robots
        float spreadX = Random.Range(-spreadIntensity, spreadIntensity);
        float spreadY = Random.Range(-spreadIntensity, spreadIntensity);

        // Return direction and spread
        return direction + new Vector3(spreadX, spreadY, 0);
    }
    private IEnumerator DestroyBullet(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }
}
