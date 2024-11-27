using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    TankSystem tank;

    public Animator dog;

    public float movementSpeed, turnSpeed;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        // Reference tank control system component
        tank = GetComponent<TankSystem>();
        tank.dog = dog;
    }

    // Update is called once per frame
    void Update()
    {
        // If tank fall out of the map
        if (transform.position.y < -10f)
        {
            // die
            tank.CommitSudoku();
        }

        tank.Move(Input.GetAxis(tank.movementAxisName) * movementSpeed);
        if (Input.GetAxis(tank.movementAxisName) != 0)
            dog.SetTrigger("Run");

        tank.Turn(Input.GetAxis(tank.rotateAxisName) * turnSpeed);

        if (Input.GetKeyDown(tank.shootButton))
        {
            tank.GetReadyToShoot();
        }
        else if (Input.GetKey(tank.shootButton))
        {
            tank.ChargePower();
        }
        else if (Input.GetKeyUp(tank.shootButton))
        {
            dog.SetTrigger("Attack");
            tank.Shoot();
        }

        healthBar.value = tank.currentHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item/Medkit"))
        {
            Destroy(other.gameObject);
            tank.GiveEffect(TankSystem.Effect.Heal, 0.5f);
        }
        // else if (other.CompareTag("Barrier"))
        // {
        //     Destroy(other.gameObject);
        //     tank.GiveEffect(TankSystem.Effect.Barrier, 10);
        // }
        // else if (other.CompareTag("Speed"))
        // {
        //     Destroy(other.gameObject);
        //     tank.GiveEffect(TankSystem.Effect.Speed, 10);
        // }
        // else if (other.CompareTag("Bomb"))
        // {
        //     Destroy(other.gameObject);
        //     tank.GiveEffect(TankSystem.Effect.Bomb, 10);
        // }
    }
}