using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player")]
    public float playerSpeed = 10f;
    public Rigidbody2D playerGFX;
    public Rigidbody2D playerEntity;
    [Space()]
    public float lightLevel = 35f;
    public float lightDecay = 0.3f;
    public Light2D pointLight;
    [Space()]
    public Camera playerCamera;
    [Header("Gun")]
    public float shotsPerSecond = 10f;
    public float gunCooldownStart = 0.5f;
    public float gunCooldownPerSec = 5f;
    public float gunBarrelCooldown = 3f;
    public float gunStressMax = 7f;
    public float gunStressPerShot = 0.1f;
    public GameObject bulletPrefab;
    public AudioClip gunFire;
    public AudioClip barrelFire;
    [Space()]
    public SpriteRenderer leftGunRenderer;
    public Transform firePointLeft;
    [Space()]
    public SpriteRenderer rightGunRenderer;
    public Transform firePointRight;
    [Space()]
    public Sprite[] gunHeatLevels;


    float nextTimeToFireLeft = 0f;
    float nextTimeToFireRight = 0f;
    int leftBarrel = 0;
    float nextTimeToFireLeftBarrel = 0f;
    int rightBarrel = 0;
    float nextTimeToFireRightBarrel = 0f;
    private float gunStressLeft = 0f;
    private float gunStressRight = 0f;
    private float panicMultiplier = 0f;
    private AudioSource audioSource;

    Vector2 movement;
    Vector2 mousePos;

    bool isDead = false;
    bool boost = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        //Debug.Log(lightLevel);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = playerCamera.ScreenToWorldPoint(Input.mousePosition);


        panicMultiplier = 1.5f - lightLevel / 10f;
        if (panicMultiplier < 1f)
            panicMultiplier = 1f;
        else if (panicMultiplier > 1.5)
            panicMultiplier = 1.5f;
        //Debug.Log(playerSpeed * panicMultiplier);

        if (lightLevel < 0f)
        {
            pointLight.color = new Color(0.75f, 0f, 0f);
            pointLight.pointLightOuterRadius = 10;
            pointLight.pointLightInnerRadius = 80;
            isDead = true;
            SceneManager.LoadScene(0);
            return;
        }
        else if (lightLevel < 0.3f)
        {
            pointLight.pointLightOuterRadius = 0;
            pointLight.pointLightInnerRadius = 0;
        }
        else if (lightLevel < 20f)
        {
            lightLevel -= lightDecay * Time.deltaTime;

            pointLight.color = new Color(1f, lightLevel / 20f, lightLevel / 20f);
            pointLight.pointLightOuterRadius = lightLevel;
            pointLight.pointLightInnerRadius = lightLevel / 5;
        }
        else
        {
            if (boost)
                lightLevel -= lightDecay * Time.deltaTime * 2.5f;
            else
                lightLevel -= lightDecay * Time.deltaTime;
            pointLight.pointLightOuterRadius = lightLevel;
            pointLight.pointLightInnerRadius = lightLevel / 5;
        }

        // Full Barrel

        if (Input.GetButton("Jump"))
        {
            while (gunStressLeft == 0)
            {
                audioSource.PlayOneShot(barrelFire);
                leftBarrel = 5;
                nextTimeToFireLeft = Time.time + gunBarrelCooldown;
                gunStressLeft = gunStressMax;
            }
            while (gunStressRight == 0)
            {
                audioSource.PlayOneShot(barrelFire);
                rightBarrel = 5;
                nextTimeToFireRight = Time.time + gunBarrelCooldown;
                gunStressRight = gunStressMax;
            }
        }
        if(nextTimeToFireLeftBarrel < Time.time && leftBarrel > 0)
        {
            ShootLeft(0f);
            ShootLeft(15f);
            ShootLeft(-15f);
            leftBarrel--;
            nextTimeToFireLeftBarrel = Time.time + 0.08f;
        }
        if (nextTimeToFireRightBarrel < Time.time && rightBarrel > 0)
        {
            ShootRight(0f);
            ShootRight(15f);
            ShootRight(-15f);
            rightBarrel--;
            nextTimeToFireRightBarrel = Time.time + 0.08f;
        }
        // Left gun

        if (Input.GetButton("Fire1") && Time.time > nextTimeToFireLeft)
        {
            nextTimeToFireLeft = Time.time + 1f / shotsPerSecond + gunStressLeft / 25f;
            audioSource.PlayOneShot(gunFire);
            ShootLeft(0f);
        }
        if (gunStressLeft <= gunHeatLevels.Length-1)
        {
            leftGunRenderer.sprite = gunHeatLevels[(int)((gunHeatLevels.Length - 1) * (gunStressLeft / gunStressMax))];
        }
        if (nextTimeToFireLeft + gunCooldownStart < Time.time && gunStressLeft > 0)
        {
            gunStressLeft -= gunCooldownPerSec * Time.deltaTime;
            if (gunStressLeft < 0)
            {
                gunStressLeft = 0;
            }
        }

        // Right gun

        if (Input.GetButton("Fire2") && Time.time > nextTimeToFireRight)
        {
            nextTimeToFireRight = Time.time + 1f / shotsPerSecond + gunStressRight / 25f;
            audioSource.PlayOneShot(gunFire);
            ShootRight(0f);
        }
        if (gunStressRight <= gunHeatLevels.Length - 1)
        {
            rightGunRenderer.sprite = gunHeatLevels[(int)((gunHeatLevels.Length-1)*(gunStressRight/gunStressMax))];
        }
        if (nextTimeToFireRight + gunCooldownStart < Time.time && gunStressRight > 0)
        {
            gunStressRight -= gunCooldownPerSec * Time.deltaTime;
            if (gunStressRight < 0)
            {
                gunStressRight = 0;
            }
        }
        // Debug.Log("Stress L=" + gunStressLeft + " R=" + gunStressRight);
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        
        playerEntity.MovePosition(playerEntity.position + panicMultiplier * movement * playerSpeed * Time.fixedDeltaTime);
        playerGFX.MovePosition(playerEntity.position + panicMultiplier * movement * playerSpeed * Time.fixedDeltaTime);
        

        Vector2 lookDir = mousePos - playerEntity.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        playerGFX.rotation = angle;
    }

    void ShootLeft(float offset)
    {
        Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation * Quaternion.Euler(0,0,90f + offset));
        gunStressLeft += gunStressPerShot;
        if (gunStressLeft > gunStressMax)
        {
            gunStressLeft = gunStressMax;
        }
    }
    void ShootRight(float offset)
    {
        Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation * Quaternion.Euler(0, 0, 90f + offset));
        gunStressRight += gunStressPerShot;
        if (gunStressRight > gunStressMax)
        {
            gunStressRight = gunStressMax;
        }
    }

    public void TakeDamage(float dmg)
    {
        if (isDead)
        {
            return;
        }
        lightLevel -= dmg;
    }
}
