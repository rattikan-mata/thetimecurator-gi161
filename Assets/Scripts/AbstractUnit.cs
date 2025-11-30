using UnityEngine;

// 1. Abstract Class: ฐานของยูนิตทั้งหมด
public abstract class AbstractUnit : MonoBehaviour
{

    // Composition: รวม UnitStats เข้ามา (ใช้ Encapsulation)
    [SerializeField] protected UnitStats _stats = new UnitStats();

    // Public Getter: ให้คลาสภายนอกเข้าถึงอ็อบเจกต์ UnitStats ได้อย่างถูกกฎหมาย
    public UnitStats Stats => _stats;

    // Public Getter สำหรับอ่านค่าสถานะ (ใช้ Encapsulation)
    public int Health => _stats.Health;
    public ElementType CurrentElement => _stats.CurrentElement;

    // 1. Abstract Method: บังคับให้คลาสลูกต้องมี Logic การโต้ตอบนี้ (Polymorphism)
    public abstract void HandleElementContact(ElementType element, IElementInteractive sourceObject);

    // 2. Virtual Method: อนุญาตให้คลาสลูกเขียนทับได้
    public virtual void MoveTo(Vector3 targetPosition)
    {
        transform.position = targetPosition;
        Debug.Log($"Unit {gameObject.name} Moved to {targetPosition}");
    }

    public void TakeDamage(int amount)
    {
        _stats.ChangeHealth(-amount);
        if (_stats.Health <= 0)
        {
            Die();
        }
    }

    // 1. Abstract Method: บังคับให้คลาสลูกต้องมี Logic การตายที่เหมาะสม
    protected abstract void Die();

    // เมธอดสำหรับสลับธาตุ
    public void ShiftElement(ElementType newElement)
    {
        // เรียกใช้เมธอด ChangeElement จาก UnitStats (Encapsulation)
        _stats.ChangeElement(newElement);
    }
}
