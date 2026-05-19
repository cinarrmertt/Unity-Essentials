using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float rotateSpeed;
    public GameObject particleEffect;
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(particleEffect, transform.position, transform.rotation);
        }
       
    }
}
