﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightShape;

[System.Serializable]
public class DayLightColliderShape {
	public DayLightCollider2D.ShadowType shadowType = DayLightCollider2D.ShadowType.SpritePhysicsShape;
    
	public DayLightCollider2D.MaskType maskType = DayLightCollider2D.MaskType.Sprite;

	public Transform transform;
   
    public DayLightingColliderTransform transform2D = new DayLightingColliderTransform();

    public SpriteShape spriteShape = new SpriteShape();
    public SpritePhysicsShape spritePhysicsShape = new SpritePhysicsShape();
	public Collider2DShape colliderShape = new Collider2DShape();

    public float height = 1;
	public float thickness = 1;

	public bool isStatic = false;

    public void SetTransform(Transform t) {
        transform = t;

        transform2D.SetShape(this);

        spriteShape.SetTransform(t);
        spritePhysicsShape.SetTransform(t);
		
		colliderShape.SetTransform(t);
    }

    public void ResetLocal() {
		spriteShape.ResetLocal();
		spritePhysicsShape.ResetLocal();

		colliderShape.ResetLocal();
	}

    public void ResetWorld() {
		spritePhysicsShape.ResetWorld();

		colliderShape.ResetWorld();
	}

	public List<MeshObject> GetMeshes() {
		switch(shadowType) {

			case DayLightCollider2D.ShadowType.FillCollider2D:
				return(colliderShape.GetMeshes());
		}

		return(null);
	}

	public List<Polygon2> GetPolygonsLocal() {
		switch(shadowType) {
			case DayLightCollider2D.ShadowType.SpritePhysicsShape:
				return(spritePhysicsShape.GetPolygonsLocal());

			case DayLightCollider2D.ShadowType.Collider2D:
			case DayLightCollider2D.ShadowType.FillCollider2D:
				return(colliderShape.GetPolygonsLocal());
		}

		return(null);
	}

    public List<Polygon2> GetPolygonsWorld() {
		switch(shadowType) {
			case DayLightCollider2D.ShadowType.SpritePhysicsShape:
			case DayLightCollider2D.ShadowType.SpriteProjectionShape:
				return(spritePhysicsShape.GetPolygonsWorld());
				
			case DayLightCollider2D.ShadowType.Collider2D:
			case DayLightCollider2D.ShadowType.SpriteProjectionCollider:
				return(colliderShape.GetPolygonsWorld());
		}

		return(null);
	}


	public Rect GetShadowBounds() {
		List<Polygon2> polygons = GetPolygonsWorld();

		if (polygons != null) {
	
			return( Polygon2Helper.GetDayRect(polygons, height) );
			
		}

		return(new Rect());
	}
}
