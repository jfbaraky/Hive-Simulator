using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.IO;
using System;

public class Hive_UserInterface : MonoBehaviour
{
    public string mapFilePath;
    public GameObject map;

    public void Load_NewMap()
    {
        Destroy(map);
        map = new GameObject("World Map");
        SpriteRenderer mapRender = map.AddComponent<SpriteRenderer>();
        mapFilePath = EditorUtility.OpenFilePanel("Mapa", "../", "");
        if (mapFilePath != string.Empty)
        {
            byte[] data = File.ReadAllBytes(mapFilePath);
            
            Texture2D texture = new Texture2D(10, 10, TextureFormat.ARGB32, false);
            texture.LoadImage(data);
            texture.filterMode = FilterMode.Point;
            
            texture.name = Path.GetFileNameWithoutExtension(mapFilePath);
            mapRender.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));


            BoxCollider2D[,] collisionMap = new BoxCollider2D[texture.width,texture.height];
            

            float scale = (float)(256 / texture.width);

            Debug.Log(scale);

            map.transform.localScale = new Vector3(scale,scale);
            map.transform.position = new Vector3(0, 0, 100);
            GameObject walls = new GameObject("Walls");
            walls.transform.parent = map.transform;
            // walls.transform.localScale = new Vector3(1,1);

            int centerX, centerY;

            if (texture.width % 2 == 0)
            {
                centerX = texture.width / 2;
                
            }

            else
            {
                centerX = (texture.width + 1) / 2;
            }
                

            if (texture.height % 2 == 0)
            {
                centerY = texture.height / 2;
            }

            else
            {
                centerY = (texture.height + 1) / 2;
            }
                

            for (int posX = 0; posX < texture.width; posX++)
            {
                for (int posY = 0; posY < texture.height; posY++)
                {
                    if (texture.GetPixel(posX, posY) == Color.black)
                    {                        
                        collisionMap[posX,posY] = walls.AddComponent<BoxCollider2D>();
                        collisionMap[posX, posY].offset = new Vector2((float)(posX - centerX), (float)(posY - centerY));
                        collisionMap[posX, posY].size = new Vector2(1,1);
                    }
                }
            }

            walls.transform.localScale = new Vector3(0.01f, 0.01f);
            walls.transform.position = new Vector3(scale*0.005f, scale*0.005f);

        }

    }//public void Load_NewMap()

}//public class Hive_UserInterface : MonoBehaviour