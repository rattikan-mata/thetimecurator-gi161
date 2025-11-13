using UnityEngine;

public abstract class AbstractUnit : MonoBehaviour
{
    [SerializeField] protected UnitStats _stats = new UnitStats();

    public int Health => _stats.Health;
    public ElementType CurrentElement => _stats.CurrentElement;

    public abstract void HandleElementContact(ElementType element, IElementInteractive sourceObject);

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

    protected abstract void Die();

    public void ShiftElement(ElementType newElement)
    {
        _stats.ChangeElement(newElement);
        Debug.Log($"Unit {gameObject.name} successfully shifted to {newElement}");
    }
}
