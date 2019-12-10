using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takens {
    /// <summary>
    /// Main class for picking my level
    /// </summary>
    public class Zone : Pattison.Zone {


        /// <summary>
        /// My levels information
        /// </summary>
        new static public ZoneInfo info = new ZoneInfo() {
            zoneName = "Space Ship Shooter",
            creator = "Takens",
            level = "TakensScene"
        };
    }
}