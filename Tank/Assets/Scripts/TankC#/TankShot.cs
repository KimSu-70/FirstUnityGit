using UnityEngine;

public class TankShot : MonoBehaviour
{
    public GameObject[] BulletType;
    public Transform Pos;
    public int Number;
    [SerializeField] float speed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Number = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Number = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (BulletType.Length > 0 && Number >= 0 && Number < BulletType.Length)
        {
            GameObject bulletPrefab = BulletType[Number];
            GameObject bullet = Instantiate(bulletPrefab, Pos.position, Pos.rotation);

            
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                if (Number == 0) 
                {
                    bulletRb.velocity = Pos.forward * speed; 
                }
                else if (Number == 1) 
                {
                    bulletRb.velocity = (Pos.forward + Vector3.up * 0.5f) * speed; 
                }
            }

            
            TankBullet bulletScript = bullet.GetComponent<TankBullet>();
            if (bulletScript != null)
            {
                if (Number == 0)
                {
                    bulletScript.attackPower = 1;
                }
                else if(Number == 1)
                {
                    bulletScript.attackPower = 2;
                }
            }
        }
    }
}
