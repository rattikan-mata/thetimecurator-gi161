using UnityEngine;

public enum ElementType
{
    Present, 
    Past     
}

[System.Serializable]
public class UnitStats
{
    [SerializeField] private int _health = 10;
    [SerializeField] private ElementType _currentElement = ElementType.Present;

    public int Health
    {
        get { return _health; }
        private set { _health = value; }
    }

    public ElementType CurrentElement
    {
        get { return _currentElement; }
        private set { _currentElement = value; }
    }

    public void ChangeHealth(int amount)
    {
        this.Health += amount;
        if (this.Health < 0) this.Health = 0;
        Debug.Log($"Health changed by: {amount}");
    }

    public void ChangeElement(ElementType newElement)
    {
        this.CurrentElement = newElement;
        Debug.Log($"Element shifted to: {newElement}");
    }
}