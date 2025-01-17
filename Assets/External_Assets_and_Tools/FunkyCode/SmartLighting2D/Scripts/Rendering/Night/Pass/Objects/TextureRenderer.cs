﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rendering.Night {
        
    public class TextureRenderer  {
        
		public static void Draw(LightTexture2D id , Camera camera) {
			if (id.InCamera(camera) == false) {
				return;
			}

			Vector2 offset = -camera.transform.position;

			Material material;

			switch(id.shaderMode) {
				case LightTexture2D.ShaderMode.Additive:
					material = Lighting2D.materials.GetAdditive();
					material.mainTexture = id.texture;

					GLExtended.color = id.color;

					Universal.Texture.Quad.Draw(material, new Vector3(offset.x, offset.y) + id.transform.position, id.size, 0, 0);
					
					material.mainTexture = null;
				break;

				case LightTexture2D.ShaderMode.Multiply:
					material = Lighting2D.materials.GetMultiplyHDR();
					material.mainTexture = id.texture;

					GLExtended.color = id.color;

					Universal.Texture.Quad.Draw(material, new Vector3(offset.x, offset.y) + id.transform.position, id.size, 0, 0);
					
					material.mainTexture = null;

				break;
			}

		
		}
    }
}