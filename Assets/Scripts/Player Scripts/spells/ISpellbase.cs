using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpellbase
{
    void OnDisable();
    void Start();

    void CastSpell();
}
