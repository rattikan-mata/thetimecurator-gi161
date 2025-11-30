using UnityEngine;

public class InputManager : MonoBehaviour
{

    // อ้างอิงถึง PlayerUnit เพื่อส่งคำสั่ง
    public PlayerUnit player;

    void Update()
    {
        if (player == null) return;

        // ตรวจสอบการสลับ Element (ใช้ปุ่ม E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.ShiftElement();
        }

        // ตรวจสอบการเคลื่อนที่
        // ในเกม Grid-Based เราจะใช้ปุ่มลูกศรเพื่อกำหนดทิศทางการเคลื่อนที่
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryMoveAndInteract(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TryMoveAndInteract(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TryMoveAndInteract(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TryMoveAndInteract(Vector3.right);
        }
    }

    private void TryMoveAndInteract(Vector3 direction)
    {
        // ตำแหน่งเป้าหมายคือ ตำแหน่งปัจจุบัน + 1 ช่อง ตามทิศทาง
        Vector3 targetPosition = player.transform.position + direction;

        // ในเกม Grid-Based เราต้องหาว่าในตำแหน่งเป้าหมายนั้น มีวัตถุอะไรอยู่บ้าง
        // *****************************************************************
        // 1. ตรวจสอบว่าตำแหน่งเป้าหมายมี IElementInteractive Object หรือไม่
        IElementInteractive targetObject = FindInteractiveObjectAtPosition(targetPosition);

        if (targetObject != null)
        {
            // 2. ถ้ามีวัตถุโต้ตอบได้ ให้เรียกเมธอด InteractWith() ของ Player
            // นี่คือการเรียกใช้ Polymorphism!
            player.InteractWith(targetObject);
        }
        else
        {
            // 3. ถ้าไม่มีวัตถุโต้ตอบ ให้เคลื่อนที่ตามปกติ
            player.MoveTo(targetPosition);
        }

        // ในเกม Turn-Based ต้องแจ้ง GameManager ว่าผู้เล่นทำ Action เสร็จแล้ว
        // GameManager.EndPlayerTurn(); 
        // *****************************************************************
    }

    // เมธอดสำหรับจำลองการค้นหาวัตถุใน Grid (ในโค้ดจริงจะซับซ้อนกว่านี้)
    private IElementInteractive FindInteractiveObjectAtPosition(Vector3 position)
    {
        // ในโปรเจกต์จริง: ค้นหา GameObject ที่มี Component IElementInteractive ในตำแหน่งนั้น
        // สำหรับการทดลองเบื้องต้น: เราสามารถใช้ Physics.OverlapBox หรือ Tag/Layer 

        Collider2D hit = Physics2D.OverlapBox(position, new Vector2(0.5f, 0.5f), 0);

        if (hit != null)
        {
            // พยายามดึง Interface IElementInteractive จากวัตถุที่ชน
            return hit.GetComponent<IElementInteractive>();
        }
        return null;
    }
}