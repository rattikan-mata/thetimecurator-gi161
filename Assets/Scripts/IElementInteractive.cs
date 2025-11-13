using UnityEngine;

public interface IElementInteractive
{
    bool HandleElementContact(ElementType type, PlayerUnit player);
}
