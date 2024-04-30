using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace PartyHardCHS
{
    public static class FontAssets
    {
        public static string dll_Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string fontFilePath = Path.Combine(dll_Location, "bitfont.fnt");
        public static string fontTexturePath = Path.Combine(dll_Location, "bitfont.png");
        //public static UIFont myNGUIfont;
        public static tk2dFontData mytk2dFontData = null;

        public static tk2dFontData CheckMyFontData()
        {
            if (mytk2dFontData == null)
            {
                // tk2dFontData 不能单独创建，必须依附于一个 GameObject
                // 基于： https://www.2dtoolkit.com/forum/index.php?topic=904.0
                GameObject fontDataObj = new GameObject();
                fontDataObj.name = "Label";
                tk2dFontData fontData = fontDataObj.AddComponent<tk2dFontData>();

                var fontInfo = Builder.ParseBMFont(FontAssets.fontFilePath);
                Builder.BuildFont(fontInfo, fontData, 1, 0, false, false, null, 0);
                Material fontMaterial = new Material(Shader.Find("tk2d/BlendVertexColor"));
                Texture2D fontTexture = new Texture2D(2, 2);
                fontTexture.LoadImage(File.ReadAllBytes(FontAssets.fontTexturePath));
                fontTexture.wrapMode = TextureWrapMode.Clamp;
                fontTexture.filterMode = FilterMode.Point;
                fontTexture.anisoLevel = 16;
                fontTexture.Apply();
                fontMaterial.mainTexture = fontTexture;

                fontData.material = fontMaterial;
                mytk2dFontData = fontData;
                return mytk2dFontData;
            }
            else
            {
                return mytk2dFontData;
            }
        }
    }
}
