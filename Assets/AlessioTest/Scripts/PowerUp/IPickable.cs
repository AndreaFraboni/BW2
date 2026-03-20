using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface IPickable
{
    public bool IsPicked { get; set; } 
    
    void Pick();
}
