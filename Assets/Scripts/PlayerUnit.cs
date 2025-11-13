using UnityEngine;

public class PlayerUnit : AbstractUnit
{
    public void InteractWith(IElementInteractive targetObject)
    {
        bool success = targetObject.HandleElementContact(_stats.CurrentElement, this);

        if (!success)
        {
            TakeDamage(1);
            Debug.Log("Interaction Failed! Took 1 Damage.");
        }
        else
        {
            Debug.Log("Interaction Success!");
        }
    }

    public void ShiftElement()
    {
        ElementType newElement = _stats.CurrentElement == ElementType.Present ? ElementType.Past : ElementType.Present;
        _stats.ChangeElement(newElement);
    }

    public override void HandleElementContact(ElementType element, IElementInteractive sourceObject)
    {
        Debug.Log($"Player received element signal: {element}");
    }

    protected override void Die()
    {
        Debug.Log("Player Died. Game Over.");
    }
}