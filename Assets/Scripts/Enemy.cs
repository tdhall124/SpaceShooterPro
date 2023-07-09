using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4f;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Player player = other.transform.GetComponent<Player>();
            Player player2 = other.GetComponent<Player>();
            
            if (player2 != null)
                player2.Damage();
            Destroy(this.gameObject);
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
