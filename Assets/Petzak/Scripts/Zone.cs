using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak {
    public class Zone : Pattison.Zone {

        new static public ZoneInfo info = new ZoneInfo() {
            zoneName = "Big Spider",
            creator = "Alec Petzak",
            level = "PetzakScene"
        };

        /// <summary>
        /// Global hud
        /// </summary>
        public HUD hud;
    }
}