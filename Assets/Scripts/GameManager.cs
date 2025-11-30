using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerUnit player;
    public EnemyAI enemyAI;
    private bool isPlayerTurn = true;

    void Start()
    {
        // ตรวจสอบความถูกต้องของการเชื่อมต่อ
        if (player == null || enemyAI == null)
        {
            Debug.LogError("GameManager: Player or Enemy AI not assigned!");
            enabled = false;
        }
        Debug.Log("Game Started. Player Turn.");
    }

    public void EndPlayerTurn()
    {
        if (!isPlayerTurn) return;

        isPlayerTurn = false;
        Debug.Log("--- Player Turn Ended. Starting Enemy Turn... ---");

        // 1. ให้ศัตรูกระทำ
        enemyAI.Behavior();

        // 2. ตรวจสอบเงื่อนไขการแพ้/ชนะ
        CheckWinCondition();

        // 3. เริ่มต้นเทิร์นผู้เล่นใหม่
        isPlayerTurn = true;
        Debug.Log("--- Enemy Turn Ended. Player Turn. ---");
    }

    private void CheckWinCondition()
    {
        // ตรวจสอบการแพ้
        if (player.Health <= 0)
        {
            Debug.Log("PLAYER DIED. GAME OVER.");
            Time.timeScale = 0; // หยุดเกม
        }

        // ตรวจสอบการชนะ (เมื่อ Temporal Anchor ถูกเปิดใช้งาน)
        TemporalAnchor anchor = FindObjectOfType<TemporalAnchor>();
        if (anchor != null && anchor.isActivated)
        { // ต้องแก้ให้ Anchor มี public bool isActivated
            Debug.Log("TEMPORAL ANCHOR ACTIVATED. YOU WIN!");
            Time.timeScale = 0; // หยุดเกม
        }
    }
}