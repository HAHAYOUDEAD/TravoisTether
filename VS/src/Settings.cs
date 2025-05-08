using ModSettings;

namespace TravoisTether
{
    internal static class Settings
    {
        public static void OnLoad()
        {
            Settings.options = new TravoisTetherSettings();
            Settings.options.AddToModSettings("Travois Tether");
        }

        public static TravoisTetherSettings options;
    }

    internal class TravoisTetherSettings : JsonModSettings
    {
        [Name("Reposition")]
        [Description("When game fails to validate travois position - attempt to reposition\n\nNot recommended unless your travois is stuck somewhere")]
        public bool reposition = false;

        [Name("Debug Log")]
        [Description("Log debug messages to console")]
        public bool debugLog = false;


        protected override void OnConfirm()
        {
            base.OnConfirm();
        }
    }
}
