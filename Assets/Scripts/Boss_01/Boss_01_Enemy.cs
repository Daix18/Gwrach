using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_01_Enemy : Entity 
{
    public Animator animator; // Referencia al Animator del enemigo
    public Canvas pauseCanvas; // Canvas que pausa el juego
    public KeyCode resumeKey = KeyCode.W; // Tecla para reanudar el juego
    public float minCountdown = 3f; // Tiempo mínimo de la cuenta regresiva
    public float maxCountdown = 10f; // Tiempo máximo de la cuenta regresiva

    private bool isPlayerInRange = false; // Si el jugador está en el rango de colisión
    private bool isPerformingAction = false; // Si el enemigo está realizando una acción
    private float attack1Countdown; // Cuenta regresiva para el ataque 1
    private float attack2Countdown; // Cuenta regresiva para el ataque 2
    private int attack1Count = 0; // Contador de ataques 1S

    private void Start()
    {

        ResetAttackCountdowns();
        pauseCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPerformingAction) return;

        if (isPlayerInRange)
        {
            PerformFirstAttack();
        }
        else
        {
            UpdateCountdowns();
        }
    }

    private void ResetAttackCountdowns()
    {
        attack1Countdown = Random.Range(minCountdown, maxCountdown);
        attack2Countdown = Random.Range(minCountdown, maxCountdown);
    }

    private void UpdateCountdowns()
    {
        attack1Countdown -= Time.deltaTime;
        attack2Countdown -= Time.deltaTime;

        if (attack1Countdown <= 0 && attack2Countdown <= 0)
        {
            PerformAttack(attack2Countdown <= attack1Countdown ? 2 : 1);
        }
        else if (attack1Countdown <= 0)
        {
            PerformAttack(1);
        }
        else if (attack2Countdown <= 0)
        {
            PerformAttack(2);
        }
    }

    private void PerformFirstAttack()
    {
        isPerformingAction = true;
        animator.SetTrigger("Attack1"); // Activa la animación "FirstAttack"
        attack1Count++;

        // Mostrar el Canvas y pausar el tiempo
        pauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;

        // Esperar entrada para continuar
        StartCoroutine(WaitForResume());
    }

    private IEnumerator WaitForResume()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(resumeKey));

        // Ocultar el Canvas y reanudar el tiempo
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;

        isPerformingAction = false;

        // Si ya realizó el primer ataque tres veces, inicia el ataque 2
        if (attack1Count >= 3)
        {
            ResetAttackCountdowns();
        }
    }

    private void PerformAttack(int attackType)
    {
        isPerformingAction = true;

        if (attackType == 1)
        {
            animator.SetTrigger("Attack1"); // Activa la animación "FirstAttack"
            attack1Count++;
        }
        else if (attackType == 2)
        {
            animator.SetTrigger("Attack2"); // Activa la animación "SecondAttack"
        }

        // Esperar hasta que termine la animación antes de reanudar
        StartCoroutine(WaitForAnimation(attackType));
    }

    private IEnumerator WaitForAnimation(int attackType)
    {
        AnimatorStateInfo stateInfo;
        do
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        } while (!stateInfo.IsName(attackType == 1 ? "FirstAttack" : "SecondAttack") || stateInfo.normalizedTime < 1.0f);

        isPerformingAction = false;

        if (attackType == 1 && attack1Count >= 3)
        {
            ResetAttackCountdowns();
        }
        else
        {
            attack1Countdown = Random.Range(minCountdown, maxCountdown);
            attack2Countdown = Random.Range(minCountdown, maxCountdown);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
