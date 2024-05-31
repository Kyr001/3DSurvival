using System;
using UnityEngine;

public interface IDamagable
{
    void TakePhisicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health;  } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;
    public float movingStaminaDecay;


    public event Action onTakeDamage; // 대미지 받았을때 발생하는 이벤트

    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if(hunger.curValue <= 0.0f)
        {
            health.Subtract(noHungerHealthDecay*Time.deltaTime);
        }

        if (CharacterManager.Instance.Player.controller.isMoving)
        {
            //Debug.Log("stamina declining");
            stamina.Subtract(movingStaminaDecay * Time.deltaTime);
        }

        if (health.curValue <= 0.0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    private void Die()
    {
        //Debug.Log("죽었다");
    }

    public void TakePhisicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
}
