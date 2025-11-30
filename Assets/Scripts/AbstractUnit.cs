using UnityEngine;

// 1. Abstract Class: ฐานของยูนิตทั้งหมด
// ทุกยูนิตต้องสืบทอดจากคลาสนี้
public abstract class AbstractUnit : MonoBehaviour
{

    // Composition: รวม UnitStats เข้ามา (ใช้ Encapsulation)
    // Protected เพื่อให้คลาสลูกเข้าถึงได้ และ SerializeField เพื่อให้ปรับค่าใน Inspector ได้
    [SerializeField] protected UnitStats _stats = new UnitStats();

    // Public Getter สำหรับอ่านค่าสถานะ (ใช้ Encapsulation)
    public int Health => _stats.Health;
    public ElementType CurrentElement => _stats.CurrentElement;

    // 1. Abstract Method: บังคับให้คลาสลูก (Player/Enemy) ต้องมี Logic การโต้ตอบนี้
    // นี่คือส่วนที่สำคัญของ Polymorphism สำหรับยูนิต
    public abstract void HandleElementContact(ElementType element, IElementInteractive sourceObject);

    // 2. Virtual Method: เมธอดสำหรับการเคลื่อนที่
    // ใช้ virtual เพื่อให้คลาสลูก (เช่น EnemyUnit) สามารถ Override (เขียนทับ) พฤติกรรมการเคลื่อนที่ได้
    public virtual void MoveTo(Vector3 targetPosition)
    {
        // Logic การเคลื่อนที่พื้นฐาน
        transform.position = targetPosition;
        Debug.Log($"Unit {gameObject.name} Moved to {targetPosition}");
    }

    // เมธอดสำหรับรับความเสียหาย
    public void TakeDamage(int amount)
    {
        _stats.ChangeHealth(-amount);
        if (_stats.Health <= 0)
        {
            Die();
        }
    }

    // 1. Abstract Method: บังคับให้คลาสลูกต้องมี Logic การตายที่เหมาะสม
    // PlayerUnit จะมี Logic Game Over, EnemyUnit จะมี Logic หายไปจากฉาก
    protected abstract void Die();

    // เมธอดสำหรับสลับธาตุ
    public void ShiftElement(ElementType newElement)
    {
        // เรียกใช้เมธอด ChangeElement จาก UnitStats (Encapsulation)
        _stats.ChangeElement(newElement);
    }
}