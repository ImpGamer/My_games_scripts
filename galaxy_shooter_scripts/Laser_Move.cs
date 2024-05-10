using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser_Move : MonoBehaviour
{
    // Start is called before the first frame update
    public byte speed = 10;
    [SerializeField]
    private float disappearPos = 5.9f;
    public bool tripleShoot = false;
   

    // Update is called once per frame
    void Update()
    {
        move();
        if(transform.position.y >= disappearPos)
        {
            Destroy(this.gameObject);
        }
    }
    void move()
    {
        transform.Translate(Vector3.up*speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (!tripleShoot)
            {
                enemy.life--;
            }
            else
            {
                enemy.life -= 3;
            }
            Destroy(this.gameObject);
            other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 0.5f);
        }
    }

}
