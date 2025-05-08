global using static TravoisTether.Utility;

namespace TravoisTether
{
    public class Main : MelonMod
    {
        public bool isLoaded = false;

        public static string modsPath;

        public override void OnInitializeMelon()
        {
            modsPath = Path.GetFullPath(typeof(MelonMod).Assembly.Location + "/../../../Mods/");

            Settings.OnLoad();
        }
    }
}




