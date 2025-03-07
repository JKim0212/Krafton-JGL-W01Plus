using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    private GameManager gm;

    [Header("Components")]
    private Collider2D coll;
    private SpriteRenderer rend;

    [Header("Player Movement")]
    private Rigidbody2D playerRb;
    public float moveSpeed;
    private float curSpeed;
    private Camera mainCam;
    public GameObject shooter;


    [Header("Basic Projectile")]
    public GameObject projectile;
    public float fireRate;
    private float coolDown;
    public float damage = 5;
    public float projectileSpeed = 7;
    public Color projectileColor;

    [Header("Missile")]
    public GameObject missilePrefab;
    public float spawnDistance;
    private GameObject _currentProjectile;
    private float rotationSpeed = 100f;
    private float _rotateAngle = 90f;
    private int _rotateDirection = 0;
    public float launchSpeed = 5f;

    [Header("Skills")]
    [HideInInspector] public float skillPower = 1;
    public float ChargeTime = 1.5f;
    public GameObject gravityShot;
    [SerializeField] bool isChargeing;
    public bool isShielded;

    public GameObject shield;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameManager.instance;
        coolDown = 0;
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        isChargeing = false;
        curSpeed = moveSpeed;
        coll = GetComponent<Collider2D>();
        rend = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (gm.isPlaying)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = mainCam.WorldToScreenPoint(transform.localPosition);
            Vector2 offset = new Vector2(mousePos.x - worldPos.x, mousePos.y - worldPos.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            shooter.transform.rotation = Quaternion.Euler(0, 0, angle);

            Vector2 moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.Translate(moveDir.normalized * Mathf.Lerp(0, curSpeed, 0.5f) * Time.deltaTime, Space.World);
            if (coolDown > 0)
            {
                coolDown -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Shoot();

                }
            }

            if (Input.GetMouseButton(1))
            {
                if (_currentProjectile != null)
                {
                    RotateProjectile();
                }
            }

            if (Input.GetMouseButtonDown(1) && _currentProjectile == null)
            {
                if (gm.missileAmount > 0)
                {
                    _rotateDirection = -1;
                    SpawnProjectile();
                }

            }

            if (Input.GetMouseButtonUp(1))
            {
                if (_currentProjectile != null)
                {
                    // Launch current projectile
                    LaunchProjectile();

                }
            }

        
        } else {
            playerRb.linearVelocity = Vector2.zero;
        }


        // if (Input.GetKeyDown(KeyCode.Alpha2) && !isChargeing)
        // {
        //     Charge();

        // }

        // if (Input.GetKeyDown(KeyCode.Alpha3) && !isShielded)
        // {
        //     Shield();

        // }
    }

    void Shoot()
    {
        Instantiate(projectile, transform.position, shooter.transform.rotation);
        coolDown = fireRate;
    }

    private void SpawnProjectile()
    {
        if (_currentProjectile == null)
        {
            gm.missileAmount -= 1;
            UIManager.instance.UpdateMissile();
            Vector3 spawnPos = transform.position + Vector3.up * spawnDistance;

            _currentProjectile = Instantiate(missilePrefab, spawnPos, Quaternion.identity);
            _currentProjectile.tag = "Untagged";
        }
    }

    private void RotateProjectile()
    {
        _rotateAngle += rotationSpeed * _rotateDirection * Time.deltaTime;
        var radian = _rotateAngle * Mathf.Deg2Rad;
        var newPos = transform.position + new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * spawnDistance;
        _currentProjectile.transform.position = newPos;
        //_currentProjectile.transform.rotation = Quaternion.Euler(0, 0, - _rotateAngle);
    }

    private void LaunchProjectile()
    {
        _currentProjectile.tag = "Missile Projectile";

        var direction = (_currentProjectile.transform.position - transform.position).normalized;
        var rb = _currentProjectile.GetComponent<Rigidbody2D>();
        _currentProjectile.transform.rotation = Quaternion.Euler(direction);

        rb.linearVelocity = direction * launchSpeed;


        // Init current projectile
        _currentProjectile = null;
        _rotateAngle = 90;
        _rotateDirection = 0;
    }

    public void GravityShot()
    {
        Debug.Log("shot gravity shot");
        GameObject gravShot = Instantiate(gravityShot, transform.position, shooter.transform.rotation);
        gravShot.transform.localScale *= skillPower;
    }

    public void Charge()
    {
        if (!isChargeing)
        {
            isChargeing = true;
            curSpeed *= 4;
            StartCoroutine("ChargeCo");
        }

    }
    IEnumerator ChargeCo()
    {
        yield return new WaitForSeconds(1f * skillPower);
        isChargeing = false;
        curSpeed = moveSpeed;
    }

    public void Shield()
    {
        if (!isShielded)
        {
            isShielded = true;
            shield.SetActive(true);
            StartCoroutine("ShieldCo");
        }

    }
    IEnumerator ShieldCo()
    {
        yield return new WaitForSeconds(1f * skillPower);
        isShielded = false;
        shield.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isChargeing && collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().Damage(10f);
        }
    }

}
