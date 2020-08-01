//Written by Flip van Toly for KSP community
//License GPL v2.0 (GNU General Public License)
// Namespace Declaration 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;
using ToolbarControl_NS;
using ClickThroughFix;


using KSP.UI;
using KSP.UI.Screens;


namespace DifferentialThrustMod
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class DifferentialThrustToolbar : MonoBehaviour
    {

        private double lastUpdateToolBarTime = 0.0f;

        internal const string MODID = "DavonTCsystemsMod";
        internal const string MODNAME = "Davon Throttle Control systems";

        static ToolbarControl toolbarControl;

        void Start()
        {
            addToolbarButton();
        }
        public void OnGUI()
        {
            print("DifferentialThrustToolbar.OnGUI");
            OnDraw();

        }

        private void OnDraw()
        {
            updateToolBar();
        }

        private void updateToolBar()
        {
            if (toolbarControl == null && hasDifferentialThrustModule()) { addToolbarButton(); }
            if (toolbarControl != null && !hasDifferentialThrustModule()) { removeToolbarButton(); }

            lastUpdateToolBarTime = Planetarium.GetUniversalTime();
        }

        private void addToolbarButton()
        {
            if (toolbarControl == null)
            {
                toolbarControl = this.gameObject.AddComponent<ToolbarControl>();
                toolbarControl.AddToAllToolbars(
                    onToggleOn, onToggleOff,
                    ApplicationLauncher.AppScenes.FLIGHT,
                    MODID,
                    "DavonButton",
                    "DavonTCsystemsMod/PluginData/Textures/TCbutton-38",
                    "DavonTCsystemsMod/PluginData/Textures/TCbutton-24",
                    MODNAME
                    );
            }
        }

        private void removeToolbarButton()
        {
            if (toolbarControl != null)
            {
                toolbarControl.OnDestroy();
                Destroy(toolbarControl);
            }
        }

        void onToggleOn()
        {
            if (!toggleOnPrimary())
            {
                makePrimary();
                toggleOnPrimary();
            }
        }

        void onToggleOff()
        {
            toggleOff();
        }

        private bool hasDifferentialThrustModule()
        {
            foreach (Part p in FlightGlobals.ActiveVessel.parts)
            {
                foreach (PartModule pm in p.Modules)
                {
                    if (pm.ClassName == "DifferentialThrust")
                    {
                        return (true);
                    }
                }
            }
            return (false);
        }

        private bool toggleOnPrimary()
        {
            foreach (Part p in FlightGlobals.ActiveVessel.parts)
            {
                foreach (PartModule pm in p.Modules)
                {
                    if (pm.ClassName == "DifferentialThrust")
                    {
                        DifferentialThrust aDifferentialThrust;
                        aDifferentialThrust = p.Modules.OfType<DifferentialThrust>().FirstOrDefault();
                        if (aDifferentialThrust.isPrimary) { aDifferentialThrust.toggleModuleOn(); return true; }
                    }
                }
            }
            return (false);
        }

        private void makePrimary()
        {
            foreach (Part p in FlightGlobals.ActiveVessel.parts)
            {
                foreach (PartModule pm in p.Modules)
                {
                    if (pm.ClassName == "DifferentialThrust")
                    {
                        DifferentialThrust aDifferentialThrust;
                        aDifferentialThrust = p.Modules.OfType<DifferentialThrust>().FirstOrDefault();
                        aDifferentialThrust.isPrimary = true;
                        return;
                    }
                }
            }
        }

        private bool toggleOff()
        {
            foreach (Part p in FlightGlobals.ActiveVessel.parts)
            {
                foreach (PartModule pm in p.Modules)
                {
                    if (pm.ClassName == "DifferentialThrust")
                    {
                        DifferentialThrust aDifferentialThrust;
                        aDifferentialThrust = p.Modules.OfType<DifferentialThrust>().FirstOrDefault();
                        aDifferentialThrust.toggleModuleOff();
                    }
                }
            }
            return (false);
        }
    }
}
