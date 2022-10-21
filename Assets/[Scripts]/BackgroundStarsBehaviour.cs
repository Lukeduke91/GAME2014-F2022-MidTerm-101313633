using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///  BackgroundStarBehaviour.cs
///  Lucas Gurney
///  101313633
///  October 21 2022 1:17 PM
///  Makes the background loop endlessly
///  Changed the background to loop to the left

public class BackgroundStarsBehaviour : MonoBehaviour
{
    public float verticalSpeed;
    public Boundary boundary;
    

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    public void Move()
    {
        transform.position -= new Vector3(verticalSpeed * Time.deltaTime, 0.0f);
    }

    public void CheckBounds()
    {
        if (transform.position.x < boundary.min)
        {
            ResetStars();
        }
    }

    public void ResetStars()
    {
        transform.position = new Vector2(boundary.max, 0.0f);
    }
}
