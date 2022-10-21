using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

///  EnemyBehaviour.cs, 
///  Lucas Gurney, 
///  101313633
///  October 21 2022 5:08 PM, 
///  Makes the enemy in a ping pong pattern and slowly creeps towards the player while shooting him 
///  Made it change from going down and bouncing left and right now going left and bouncing up and down towards the player.
public class EnemyBehaviour : MonoBehaviour
{
    public Boundary horizontalBoundary;
    public Boundary verticalBoundary;
    public Boundary screenBounds;
    public float horizontalSpeed;
    public float verticalSpeed;
    public Color randomColor;

    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public float fireRate = 0.2f;
    
    
    private BulletManager bulletManager;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletManager = FindObjectOfType<BulletManager>();
        ResetEnemy();
        InvokeRepeating("FireBullets", 0.3f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    public void Move()
    {
        var verticalLength = verticalBoundary.max - verticalBoundary.min;
        //transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, verticalLength) - horizontalBoundary.max,
        //    transform.position.y - verticalSpeed * Time.deltaTime, transform.position.z);
        transform.position = new Vector3(transform.position.x - horizontalSpeed * Time.deltaTime,
            Mathf.PingPong(Time.time * verticalSpeed, verticalLength) - verticalBoundary.max, transform.position.z);
    }

    public void CheckBounds()
    {
        if (transform.position.x < screenBounds.min)
        {
            ResetEnemy();
        }
    }

    public void ResetEnemy()
    {
        var RandomXPosition = Random.Range(horizontalBoundary.min, horizontalBoundary.max);
        var RandomYPosition = Random.Range(verticalBoundary.min, verticalBoundary.max);
        horizontalSpeed = Random.Range(1.0f, 6.0f);
        verticalSpeed = Random.Range(1.0f, 3.0f);
        transform.position = new Vector3(RandomXPosition, RandomYPosition, 0.0f);

        List<Color> colorList = new List<Color>() {Color.red, Color.yellow, Color.magenta, Color.cyan, Color.white, Color.white};

        randomColor = colorList[Random.Range(0, 6)];
        spriteRenderer.material.SetColor("_Color", randomColor);
    }

    void FireBullets()
    {
        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.ENEMY);
    }
}
