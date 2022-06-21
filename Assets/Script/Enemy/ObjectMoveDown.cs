using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum type
{
    Gold,
    Fuel,
    Bullet,
    Heart,
    Brick,
    Box,
    Bonus
}

public class ObjectMoveDown : MonoBehaviour
{
    public type typeEnemy;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    
    void Update()
    {
        transform.position += new Vector3(0,-4, 0) * (Time.deltaTime * gameManager.speedEnemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (typeEnemy == type.Gold)
            {
                gameManager.goldGet();
                Lean.Pool.LeanPool.Despawn(this.gameObject);
            }

            if (typeEnemy == type.Fuel)
            {
                gameManager.fuelGet();
                Lean.Pool.LeanPool.Despawn(this.gameObject);
            }

            if (typeEnemy == type.Bullet)
            {
                gameManager.bulletGet();
                Lean.Pool.LeanPool.Despawn(this.gameObject);
            }

            if (typeEnemy == type.Heart)
            {
                gameManager.heartGet();
                Lean.Pool.LeanPool.Despawn(this.gameObject);
            }
            
            if (typeEnemy == type.Brick)
            {
                gameManager.brickGet();
            }

            if (typeEnemy == type.Box)
            {
                gameManager.boxGet();
                Lean.Pool.LeanPool.Despawn(this.gameObject);
            }

            if (typeEnemy == type.Bonus)
            {
                gameManager.bonusGet();
                Lean.Pool.LeanPool.Despawn(this.gameObject);
            }
        }
    }
}
