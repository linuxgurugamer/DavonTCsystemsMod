using UnityEngine;
using ToolbarControl_NS;


namespace DifferentialThrustMod
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class RegisterToolbar : MonoBehaviour
    {
        protected void Start()
        {
            ToolbarControl.RegisterMod(DifferentialThrustToolbar.MODID, DifferentialThrustToolbar.MODNAME);
        }
    }

}
