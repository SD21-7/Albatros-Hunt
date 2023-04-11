using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public static class GunDict
    {
        public static readonly IDictionary<string, Gun> Guns = new Dictionary<string, Gun> {
            {"HuntRifle", new Gun("Hunting Rifle", 8, 8, 1, 0.1f, 100, false, "8bitShot", null)},
            { "M4", new Gun("M4", 30, 30, 0.15f, 0.1f, 75, true, "8bitShot", null)},
            {"Admin", new Gun("Admin Gun", 9999, 9999, 0.01f, 0.1f, 100000, true, "8bitShot", null)}
        };
    
    }
}

