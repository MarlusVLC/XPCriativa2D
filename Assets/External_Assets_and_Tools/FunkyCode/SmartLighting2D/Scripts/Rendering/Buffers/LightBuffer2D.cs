﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using LightingSettings;

[ExecuteInEditMode]
public class LightBuffer2D {
	public string name = "unknown";

	private Light2D light;

	public Light2D Light {
		get => light;
		set {
			light = value;

			Rendering.LightBuffer.UpdateName(this);
		}
	}

	public bool Free {
		get => light == null;
	}

	public LightTexture renderTexture;

	public LightTexture collisionTexture;

	public bool updateNeeded = false;

	public static List<LightBuffer2D> List = new List<LightBuffer2D>();

	public LightBuffer2D() {
		List.Add(this);
	}

	public static void Clear() {
		foreach(LightBuffer2D buffer in new List<LightBuffer2D>(List)) {
			if (buffer.light) {
				buffer.light.Buffer = null;
			}

			buffer.DestroySelf();
		}

		List.Clear();
	}

	public void DestroySelf() {
		if (renderTexture != null) {
			if (renderTexture.renderTexture != null) {
				
				if (Application.isPlaying) {
					UnityEngine.Object.Destroy (renderTexture.renderTexture);
				} else {
					UnityEngine.Object.DestroyImmediate (renderTexture.renderTexture);
				}
				
			}
		}

		List.Remove(this);
	}

	public void Initiate (Vector2Int textureSize) {
		Rendering.LightBuffer.InitializeRenderTexture(this, textureSize);
	}

	public void Render() {
		if (renderTexture.renderTexture == null) {
			return;
		}

		if (updateNeeded == false) {
			return;
		}
		
		updateNeeded = false;

		if (light == null) {
			return;
		}

		if (light.maskTranslucency > 0) {
			if (collisionTexture == null) {
				Vector2Int effectTextureSize = LightingRender2D.GetTextureSize(Lighting2D.Profile.qualitySettings.lightEffectTextureSize);
				Rendering.LightBuffer.InitializeCollisionTexture(this, effectTextureSize);
			}
			
			if (collisionTexture != null) {
				RenderTexture previous2 = RenderTexture.active;

				RenderTexture.active = collisionTexture.renderTexture;

				// Clear
				if (light.litMode == Light2D.LitMode.Everything) {
					GL.Clear(true, true, Color.white);
				} else {
					GL.Clear(true, true, Color.black);
				}

				Rendering.LightBuffer.RenderCollisions(light);

				RenderTexture.active = previous2;
			}
		}
	

		RenderTexture previous = RenderTexture.active;

		RenderTexture.active = renderTexture.renderTexture;

		// Clear
		if (light.litMode == Light2D.LitMode.Everything) {
			GL.Clear(true, true, Color.white);
		} else {
			GL.Clear(true, true, Color.black);
		}

		Rendering.LightBuffer.Render(light);

		RenderTexture.active = previous;

		
	}
}