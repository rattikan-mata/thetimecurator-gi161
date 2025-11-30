using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerUnit player;
    public EnemyAI enemyAI; // อ้างอิงถึง EnemyAI เพื่อเรียกใช้ Behavior
    private bool isPlayerTurn = true; // สถานะปัจจุบันว่าเป็นเทิร์นของผู้เล่นหรือไม่

    void Start()
    {
        // ตรวจสอบความถูกต้องของการเชื่อมต่อ
        if (player == null || enemyAI == null)
        {
            Debug.LogError("GameManager: Player or Enemy AI not assigned! Please ensure both have been dragged onto the GameManager object in the Inspector.");
            enabled = false;
        }
        // เริ่มต้นด้วยการรีเซ็ต AP เพื่อให้แน่ใจว่า Player เริ่มต้นด้วย AP เต็ม (ใช้ Encapsulation)
        if (player != null)
        {
            player.Stats.ResetActionPoints();
        }
        Debug.Log("Game Started. Player Turn.");
    }

    // เมธอดที่ถูกเรียกจาก InputManager เมื่อผู้เล่นทำ Action เสร็จแล้ว
    public void EndPlayerTurn()
    {
        if (!isPlayerTurn) return;

        isPlayerTurn = false;
        Debug.Log("--- Player Turn Ended. Starting Enemy Turn... ---");

        // 1. ให้ศัตรูกระทำ (AI Behavior)
        if (enemyAI != null)
        {
            enemyAI.Behavior();
        }

        // 2. ตรวจสอบเงื่อนไขการแพ้/ชนะ หลังจากการกระทำของผู้เล่นและศัตรู
        CheckWinCondition();

        // 3. เริ่มต้นเทิร์นผู้เล่นใหม่
        isPlayerTurn = true;

        // คืน Action Point ให้ผู้เล่นเมื่อเริ่มเทิร์นใหม่ (ใช้ Encapsulation)
        if (player != null)
        {
            player.Stats.ResetActionPoints();
        }
        Debug.Log("--- Enemy Turn Ended. Player Turn. ---");
    }

    private void CheckWinCondition()
    {
        // ตรวจสอบการแพ้ (Health น้อยกว่าหรือเท่ากับ 0)
        if (player != null && player.Health <= 0)
        {
            Debug.Log("PLAYER DIED. GAME OVER.");
            Time.timeScale = 0; // หยุดเกม (หรือแสดง Game Over UI)
            return;
        }

        // ตรวจสอบการชนะ (เมื่อ Temporal Anchor ถูกเปิดใช้งาน)
        // ใช้ FindAnyObjectByType แทน FindObjectOfType เพื่อแก้ Warning/Deprecation
        TemporalAnchor anchor = FindAnyObjectByType<TemporalAnchor>();

        // ใช้ IsActivated ที่เป็น Public Getter จาก TemporalAnchor (Encapsulation)
        if (anchor != null && anchor.IsActivated)
        {
            Debug.Log("TEMPORAL ANCHOR ACTIVATED. YOU WIN!");
            Time.timeScale = 0; // หยุดเกม (หรือแสดง Win UI)
        }
    }
}
