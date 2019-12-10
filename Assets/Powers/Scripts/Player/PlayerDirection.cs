using UnityEngine;

namespace Powers
{
    public class PlayerDirection : MonoBehaviour
    {
        public PlayerLook playerLook;

        public Material playerMaterial;
        public Texture up;
        public Texture diagonalUp;
        public Texture side;
        public Texture diagonalDown;
        public Texture down;

        // Update is called once per frame
        void Update()
        {
            //depending on the look direction of the player, set the player sprite to look in a certain direction
            if (playerLook.lookDirection.y > -22.5 && playerLook.lookDirection.y <= 22.5) playerMaterial.SetTexture("_MainTex", up);
            else if (playerLook.lookDirection.y < -22.5 && playerLook.lookDirection.y >= -67.5 || playerLook.lookDirection.y > 22.5 && playerLook.lookDirection.y <= 67.5) playerMaterial.SetTexture("_MainTex", diagonalUp);
            else if (playerLook.lookDirection.y > 247.5 && playerLook.lookDirection.y <= 270 || playerLook.lookDirection.y < -67.5 && playerLook.lookDirection.y >= -90 || playerLook.lookDirection.y > 67.5 && playerLook.lookDirection.y <= 112.5) playerMaterial.SetTexture("_MainTex", side);
            else if (playerLook.lookDirection.y > 202.5 && playerLook.lookDirection.y <= 247.5 || playerLook.lookDirection.y > 112.5 && playerLook.lookDirection.y <= 157.5) playerMaterial.SetTexture("_MainTex", diagonalDown);
            else if (playerLook.lookDirection.y > 157.5 && playerLook.lookDirection.y <= 202.5) playerMaterial.SetTexture("_MainTex", down);

            //depending on the look direction of the player, flip the player sprite
            if (playerLook.lookDirection.y >= 202.5 || playerLook.lookDirection.y <= -22.5) gameObject.transform.localScale = new Vector3(-1, 1, 1);
            else gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
