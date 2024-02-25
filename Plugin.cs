using System;
using System.IO;
using System.Reflection;
using BepInEx;
using Photon.Voice;
using Unity.Mathematics;
using UnityEngine;
using Utilla;
using Megahits;

namespace Megahits
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {


        public bool active;
        bool inRoom;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            AssetObj.SetActive(true);

            active = true;

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            AssetObj.SetActive(false);

            active = false;

            HarmonyPatches.RemoveHarmonyPatches();
        }
        public GameObject AssetObj;
        void OnGameInitialized(object sender, EventArgs e)
        {
            var assetBundle = LoadAssetBundle("MegahitsFrame.mega");
            GameObject Obj = assetBundle.LoadAsset<GameObject>("megahitz");

            AssetObj = Instantiate(Obj);
            AssetObj.transform.position = new Vector3(-68.8561f, 12.3656f, - 84.2387f);
            AssetObj.transform.rotation = Quaternion.Euler(0.2387f, 146.1173f, - 0.0005f);
            AssetObj.transform.localScale = new Vector3(2.0852f, - 50.8484f, - 44.6857f);
            AssetObj.layer = 8;
        }

        AssetBundle LoadAssetBundle(string path)
        {
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
                AssetBundle bundle = AssetBundle.LoadFromStream(stream);
                stream.Close();
                Debug.Log("[" + PluginInfo.GUID + "] Success loading asset bundle");
                return bundle;
            }
            catch (Exception e)
            {
                Debug.Log("[" + PluginInfo.Name + "] Error loading asset bundle: " + e.Message + " " + path);
                throw;
            }
        }
    }
}