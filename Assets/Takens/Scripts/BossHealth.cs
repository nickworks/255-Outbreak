using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Takens
{
    public class BossHealth : MonoBehaviour
    {
        public GameObject healthBar;
        private RectTransform t;
        public float health = 100;
        // Start is called before the first frame update
        void Start()
        {
            t = healthBar.GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health > 100) health = 100;
            if (health < 0) health = 0;
            t.localScale = new Vector3((health / 100),1,1);
        }


        private void OnTriggerEnter(Collider other)
        {
            Damage d = other.GetComponent<Damage>();
            if (d == null) return;

           if (d.friendly)
                health -= d.damage;

            Debug.Log("ow");
        }
    }
}