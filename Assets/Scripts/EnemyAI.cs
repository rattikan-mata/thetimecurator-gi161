using UnityEngine;

// Enum สำหรับสถานะ AI (Finite State Machine - FSM)
public enum EnemyState
{
    Idle,       // รอคอย
    Chasing,    // ไล่ตามผู้เล่น
    Blocking,   // ดักทาง/ขัดขวางปริศนา
    Attacking   // โจมตีผู้เล่น
}

public class EnemyAI : MonoBehaviour
{

    // อ้างอิงถึงยูนิตของศัตรูและผู้เล่น (กำหนดใน Inspector)
    public EnemyUnit enemyUnit;
    public PlayerUnit player;
    public EnemyState currentState = EnemyState.Idle;

    // ระยะการโจมตีของศัตรู
    [SerializeField] private float attackRange = 1f;

    // เมธอด Behavior ที่ถูกเรียกใช้จาก GameManager ในทุก EndPlayerTurn()
    public void Behavior()
    {
        if (enemyUnit == null || player == null) return;

        // FSM: ตรวจสอบสถานะและเปลี่ยนพฤติกรรม
        switch (currentState)
        {
            case EnemyState.Idle:
                // AI Logic: ตรวจสอบระยะผู้เล่น
                if (Vector3.Distance(enemyUnit.transform.position, player.transform.position) < 5)
                {
                    currentState = EnemyState.Chasing;
                }
                break;

            case EnemyState.Chasing:
                HandleChasingState();
                break;

            case EnemyState.Blocking:
                HandleBlockingState();
                break;

            case EnemyState.Attacking:
                HandleAttackingState();
                break;
        }
    }

    private void HandleChasingState()
    {
        // กลไก AI: วิเคราะห์ Element Form ของผู้เล่น
        if (player.CurrentElement == enemyUnit.CurrentElement)
        {
            // ถ้าผู้เล่นอยู่ใน Form ที่ศัตรูได้เปรียบ (Form เดียวกัน) ให้เปลี่ยนไปโจมตี
            currentState = EnemyState.Attacking;
        }
        else
        {
            // ถ้าผู้เล่นอยู่ใน Form ที่ศัตรูเสียเปรียบ (Form ตรงข้าม) ให้เปลี่ยนไปดักทาง/หลบหลีก
            currentState = EnemyState.Blocking;
        }
    }

    private void HandleAttackingState()
    {
        if (Vector3.Distance(enemyUnit.transform.position, player.transform.position) <= attackRange)
        {
            // โจมตีและจบเทิร์น
            enemyUnit.Attack(player);
            currentState = EnemyState.Idle; // จบเทิร์น
        }
        else
        {
            // ไล่ตาม (ในเกม Grid-Based ต้องใช้ Logic Pathfinding ที่ซับซ้อนกว่านี้)
            Vector3 direction = (player.transform.position - enemyUnit.transform.position).normalized;
            // enemyUnit.MoveTo(enemyUnit.transform.position + direction); // ตัวอย่างการเคลื่อนที่
            Debug.Log("AI: Moving to attack range.");
            currentState = EnemyState.Idle; // จบเทิร์น
        }
    }

    private void HandleBlockingState()
    {
        // AI Logic: การดักทาง (Obstacle Blocking)

        // 1. ตรวจสอบพื้นที่อันตราย (Past Form Zone)
        if (player.CurrentElement == ElementType.Past)
        {
            Debug.Log("AI: Retreating from Past Form Zone.");
            // enemyUnit.MoveTo(safe_position); // Logic ถอยหนี
        }
        else
        {
            // 2. ดักทาง: พยายามเคลื่อนที่ไปยังช่องที่ผู้เล่นต้องใช้ในการสลับ Form
            Debug.Log("AI: Blocking vital path or waiting for player's form change.");
        }

        currentState = EnemyState.Idle; // จบเทิร์น
    }
}
