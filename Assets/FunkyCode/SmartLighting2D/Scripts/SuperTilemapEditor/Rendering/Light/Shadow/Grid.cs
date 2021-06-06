﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightTilemapCollider;

#if (SUPER_TILEMAP_EDITOR)

    namespace SuperTilemapEditorSupport.Light.Shadow {
        
        public class Grid {

            static public void Draw(Light2D light, LightTilemapCollider2D id) {
                Vector2 lightPosition = -light.transform.position;
                LightTilemapCollider.Base tilemapCollider = id.GetCurrentTilemap();

                int count = tilemapCollider.chunkManager.GetTiles(light.GetWorldRect());

                for(int i = 0; i < count; i++) {
                    LightTile tile = tilemapCollider.chunkManager.display[i];

                    List<Polygon2> polygons = tile.GetWorldPolygons(tilemapCollider);
                    Vector2 tilePosition = tile.GetWorldPosition(tilemapCollider);

                    if (tile.NotInRange(tilePosition + lightPosition, light.size)) {
                        continue;
                    }

                    Rendering.Light.ShadowEngine.Draw(polygons, 0, 0);
                }
            }
        }
    }

#else 

    namespace SuperTilemapEditorSupport.Light.Shadow {
        public class Grid { 
            static public void Draw(Light2D light, LightTilemapCollider2D id) {}
        }
    }

#endif