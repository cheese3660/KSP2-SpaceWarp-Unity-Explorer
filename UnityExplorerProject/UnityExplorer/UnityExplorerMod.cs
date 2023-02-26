using SpaceWarp.API.Mods;
using System.Reflection;

namespace UnityExplorer
{
    [MainMod]
     public class UnityExplorerMod : Mod
    {
        public static string[] load_order = new string[] {
            "UniverseLib.Mono.dll"
        };

        public static string UnityExplorerDLL = "UnityExplorer.STANDALONE.Mono.dll";
        public override void OnInitialized()
        {
            Logger.Info("Unity Explorer Initialization");
            string lib_folder = SpaceWarp.API.SpaceWarpManager.MODS_FULL_PATH + "/" + Info.mod_id + "/libs/";
            foreach(string dll in load_order)
            {
                Logger.Info($"Loading library {lib_folder}{dll}");
                Assembly.LoadFrom(lib_folder + dll);
            }

            Logger.Info($"Loading {lib_folder}{UnityExplorerDLL}");
            var explorer = Assembly.LoadFrom(lib_folder + UnityExplorerDLL);
            Type explorerType = explorer.GetType("UnityExplorer.ExplorerStandalone");
            MethodInfo createMethod = explorerType.GetMethod("CreateInstance", new Type [] { });
            createMethod.Invoke(null,null);
            Logger.Info("Unity Explorer Has Been Initialized");
        }
    }
}