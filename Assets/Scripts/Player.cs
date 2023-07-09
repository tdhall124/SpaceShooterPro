using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private Vector3 _laserOffset;
    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
            Debug.LogError("The SpawnManager is NULL.");

        _laserOffset = new Vector3(0, 1.05f, 0);
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            FireLaser();
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        
        // same as above
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        
        // Cannot clamp this because we need to actually reposition to inverse of x
        if (transform.position.x > 9f)
        {
            transform.position = new Vector3(-9f, transform.position.y, 0);
        }
        else if (transform.position.x < -9f)
        {
            transform.position = new Vector3(9f, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
    }
    public void Damage()
    {
        _lives--;
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
