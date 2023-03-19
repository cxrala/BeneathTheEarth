using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityDisplay {
    public void SetEntity(Entity entity);

    public void InitialiseHealthDisplay();

    public void UpdateHealthDisplay();

    public void UpdateStatusDisplay();

    public void PerformSpriteAnimation(string animation);

    public void SetName(string name);

    public void OtherAnimation();
}
