using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaFactory : Factory<CajaPowerUp>
{
    public CajaFactory(CajaPowerUp caja)
    {
        prefab = caja;
    }
}
