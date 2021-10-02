using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum VoxMaterial {
    Snow,
    Asphalt,
    Metal,
    Wood,
    Grass,
    Ground,
    Sand,
    Water,
    Glass,
    Null
}

public class VoxMaterialManager : MonoBehaviour {

    [SerializeField] private List<Color> snowColors;
    [SerializeField] private List<Color> asphaltColors;
    [SerializeField] private List<Color> metalColors;
    [SerializeField] private List<Color> woodColors;
    [SerializeField] private List<Color> grassColors;
    [SerializeField] private List<Color> groundColors;
    [SerializeField] private List<Color> sandColors;
    [SerializeField] private List<Color> waterColors;
    [SerializeField] private List<Color> glassColors;
    

    [Header("Import")]
    [SerializeField] private Texture2D importPallete;
    [SerializeField] private int snowColorRows;
    [SerializeField] private int asphaltColorRows;
    [SerializeField] private int metalColorRows;
    [SerializeField] private int woodColorRows;
    [SerializeField] private int grassColorRows;
    [SerializeField] private int groundColorRows;
    [SerializeField] private int sandColorRows;
    [SerializeField] private int waterColorRows;
    [SerializeField] private int glassColorRows;
    [SerializeField] private Color emptyColor = new Color(0.2941177f, 0.2941177f, 0.2941177f);

    [SerializeField] private Color lastColor;
    [SerializeField] private Vector2 lastCoord;

    public VoxMaterial GetMaterial(RaycastHit hit) {
        Color color;
        var renderer = hit.collider.GetComponentInChildren<Renderer>();
        var texture2D = renderer.material.mainTexture as Texture2D;
        if (texture2D == null) {
            color = renderer.material.color;
        } else {
            Vector2 pCoord = hit.textureCoord;
            lastCoord = pCoord;
            pCoord.x *= texture2D.width;
            pCoord.y *= texture2D.height;
            Vector2 tiling = renderer.material.mainTextureScale;
            color = texture2D.GetPixel(Mathf.FloorToInt(pCoord.x * tiling.x), Mathf.FloorToInt(pCoord.y * tiling.y));
        }
        lastColor = color;
        if (snowColors.Contains(color)) return VoxMaterial.Snow;
        else if (asphaltColors.Contains(color)) return VoxMaterial.Asphalt;
        else if (metalColors.Contains(color)) return VoxMaterial.Metal;
        else if (woodColors.Contains(color)) return VoxMaterial.Wood;
        else if (grassColors.Contains(color)) return VoxMaterial.Grass;
        else if (groundColors.Contains(color)) return VoxMaterial.Ground;
        else if (sandColors.Contains(color)) return VoxMaterial.Sand;
        else if (waterColors.Contains(color)) return VoxMaterial.Water;
        else if (glassColors.Contains(color)) return VoxMaterial.Glass;
        return VoxMaterial.Null;
    }

    [ContextMenu("Import Pallete")]
    private void ImportPallete() {
        int rowStart = 0;
        ImportColors(ref rowStart, snowColorRows, ref snowColors);
        snowColors.RemoveAt(0);
        ImportColors(ref rowStart, asphaltColorRows, ref asphaltColors);
        ImportColors(ref rowStart, metalColorRows, ref metalColors);
        ImportColors(ref rowStart, woodColorRows, ref woodColors);
        ImportColors(ref rowStart, grassColorRows, ref grassColors);
        ImportColors(ref rowStart, groundColorRows, ref groundColors);
        ImportColors(ref rowStart, sandColorRows, ref sandColors);
        ImportColors(ref rowStart, waterColorRows, ref waterColors);
        ImportColors(ref rowStart, glassColorRows, ref glassColors);
    }

    private void ImportColors(ref int rowStart, int rowCount, ref List<Color> colorList) {
        colorList = new List<Color>();
        for (int row = rowStart; row < rowStart + rowCount; row++) {
            for (int col = 0; col < 8; col++) {
                var color = importPallete.GetPixel(255 - (row * 8 + col), 0);
                if (color != emptyColor) colorList.Add(color);
            }
        }
        rowStart += rowCount;
    }

}
