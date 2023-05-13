using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject projectilePrefab; //Variabel untuk menyimpan prefab proyektil
    [SerializeField] float shootingDistance; //Jarak maksimal turret bisa menembak
    [SerializeField] float shootingCooldown; //Waktu tunggu antara setiap tembakan
    [SerializeField] float lastShootTime; //Waktu terakhir turret menembak
    [SerializeField] Transform spawnProjectileLeft; //Titik spawn proyektil
    [SerializeField] Transform spawnProjectileRight; //Titik spawn proyektil

    void Update()
    {
        if (player != null)
        {
            transform.LookAt(player);
        }

        //Jika player dalam jarak tembak turret dan sudah waktunya menembak, turret menembak
        if (Vector3.Distance(transform.position, player.position) <= shootingDistance && Time.time > lastShootTime + shootingCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        //Membuat proyektil dari prefab yang sudah ditentukan
        GameObject projectileLeft = Instantiate(projectilePrefab, spawnProjectileLeft.position, Quaternion.identity);
        projectileLeft.GetComponent<Projectile>().target = player; //Mengatur target proyektil
        GameObject projectileRight = Instantiate(projectilePrefab, spawnProjectileRight.position, Quaternion.identity);
        projectileRight.GetComponent<Projectile>().target = player; //Mengatur target proyektil
    }
}
