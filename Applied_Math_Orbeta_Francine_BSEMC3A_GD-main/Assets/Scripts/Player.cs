using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    

    [SerializeField] private Animator animator;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.Playing)
        {
            return;
        }
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0f, v).normalized;

        if (direction != Vector3.zero)
        {
    
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            
            transform.position += direction * moveSpeed * Time.deltaTime;

            
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.killedEnemyCount++;
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.OnDied();
        }
    }
}
