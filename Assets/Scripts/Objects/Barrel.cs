using UnityEngine;

public class Barrel : MonoBehaviour, IDamageable
{
    //[SerializeField] private GameObject particles;
    
    public void TakeDamage(int amount)
    {
        //Particles
        //Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
