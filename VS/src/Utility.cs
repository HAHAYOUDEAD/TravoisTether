global using CC = System.ConsoleColor;
global using System;
global using MelonLoader;
global using HarmonyLib;
global using UnityEngine;
global using System.Reflection;
global using System.Collections;
global using System.Collections.Generic;
global using Il2Cpp;

namespace TravoisTether
{
    internal class Utility
    {
        public const string modVersion = "1.0";
        public const string modName = "TravoisTether";
        public const string modAuthor = "Waltz";
        

        public static bool IsScenePlayable()
        {
            return !(string.IsNullOrEmpty(GameManager.m_ActiveScene) || GameManager.m_ActiveScene.Contains("MainMenu") || GameManager.m_ActiveScene == "Boot" || GameManager.m_ActiveScene == "Empty");
        }

        public static bool IsScenePlayable(string scene)
        {
            return !(string.IsNullOrEmpty(scene) || scene.Contains("MainMenu") || scene == "Boot" || scene == "Empty");
        }

        public static bool IsMainMenu(string scene)
        {
            return !string.IsNullOrEmpty(scene) && scene.Contains("MainMenu");
        }

        public static void Log(ConsoleColor color, string message)
        {
            if (Settings.options.debugLog)
            {
                Melon<Main>.Logger.Msg(color, message);
            }
        }

        public static void AlignToSurface(Transform transform)
        {
            BoxCollider meshCollider = transform.GetComponent<BoxCollider>();
            if (meshCollider == null)
            {
                return;
            }

            Bounds bounds = meshCollider.bounds;

            float raycastHeight = 1.5f;

            int groundMask = 0;
            groundMask |= 1 << vp_Layer.TerrainObject;
            groundMask |= 1 << vp_Layer.Ground;
            groundMask |= 1 << vp_Layer.GroundNoNavmesh;

            // Define the 4 corner points of the rectangle in world space
            Vector3[] corners = new Vector3[4];
            corners[0] = new Vector3(bounds.min.x, bounds.center.y, bounds.min.z); // bottom-left
            corners[1] = new Vector3(bounds.max.x, bounds.center.y, bounds.min.z); // bottom-right
            corners[2] = new Vector3(bounds.min.x, bounds.center.y, bounds.max.z); // top-left
            corners[3] = new Vector3(bounds.max.x, bounds.center.y, bounds.max.z); // top-right

            Vector3 averagePosition = Vector3.zero;
            Vector3 averageNormal = Vector3.zero;
            int hits = 0;

            foreach (var corner in corners)
            {
                Vector3 origin = corner + Vector3.up * raycastHeight;

                if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, raycastHeight * 2f, groundMask))
                {
                    averagePosition += hit.point;
                    averageNormal += hit.normal;
                    hits++;
                }
            }

            if (hits == 0)
            {
                Log(CC.Gray, "No surface detected under any corner while attempting to reposition travois");
                return;
            }

            averagePosition /= hits;
            averageNormal.Normalize();

            // Move object to average hit position
            transform.position = averagePosition + Vector3.up * 0.1f;

            // Rotate to align with surface normal
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, averageNormal) * transform.rotation;
            transform.rotation = targetRotation;
        }

    }
}
