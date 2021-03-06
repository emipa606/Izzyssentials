using System.Collections.Generic;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace Izzyssentials
{
    //Building, IFlickable => Iflickable changed to Comp
    public class Building_HRC_Switch : Building
    {
        private CompFlickable flickableComp; //A13

        private ColorInt LedState;

        private CompSensor sensorComp;
        //now in CompFlickable
        //private bool wantSwitchOn = true;
        //private bool switchOn = true;

        private bool switchOnOld = true;

        //now in CompFlickable
        //public bool SwitchOn 
        //{
        //    get
        //    {
        //        return this.switchOn;
        //    }

        //    set
        //    {
        //        if (value != this.switchOn)
        //        {
        //            this.switchOn = value;
        //            this.UpdatePowerGrid();
        //        }
        //    }
        //}

        public override bool TransmitsPowerNow => flickableComp.SwitchIsOn; //A13

        //return this.switchOn;
        public override Color DrawColor
        {
            get
            {
                if (def.MadeFromStuff)
                {
                    return base.DrawColor;
                }

                return DrawColorTwo;
            }
        }

        public override Color DrawColorTwo => Util.IntToColour(LedState);


        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            flickableComp = GetComp<CompFlickable>(); //A13
            sensorComp = GetComp<CompSensor>();
            LedState = ListOColours.Blackout;
        }


        public override void ExposeData()
        {
            base.ExposeData();
            //Scribe_Values.LookValue<bool>(ref this.switchOn, "switchOn", false, false);
            //Scribe_Values.LookValue<bool>(ref this.wantSwitchOn, "wantSwitchOn", false, false);
            //this.switchOnOld = !this.switchOn;
            //this.UpdatePowerGrid();
            if (Scribe.mode != LoadSaveMode.PostLoadInit)
            {
                return;
            }

            if (flickableComp == null)
            {
                flickableComp = GetComp<CompFlickable>();
            }

            switchOnOld = !flickableComp.SwitchIsOn;
            UpdatePowerGrid();
        }

        //moved to Comp
        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (var g in base.GetGizmos())
            {
                //Log.Message((typeof(Command_Toggle) != g.GetType()).ToString());
                if (typeof(Command_Toggle) != g.GetType())
                {
                    yield return g;
                }
                else
                {
                    if (!sensorComp.auto)
                    {
                        yield return g;
                    }
                }
            }
        }


        public override string GetInspectString() // fuck these comments to hell and back.
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());
            stringBuilder.AppendLine();
            stringBuilder.Append("PowerSwitch_Power".Translate() + ": ");
            stringBuilder.Append(flickableComp.SwitchIsOn ? "On".Translate() : "Off".Translate());

            return stringBuilder.ToString();
        }

        protected override void ReceiveCompSignal(string signal) //A13
        {
            if (signal == "FlickedOff" || signal == "FlickedOn")
            {
                UpdatePowerGrid();
            }
        }

        private void UpdatePowerGrid()
        {
            if (flickableComp.SwitchIsOn == switchOnOld)
            {
                return;
            }

            if (Spawned)
            {
                Map.powerNetManager.Notfiy_TransmitterTransmitsPowerNowChanged(PowerComp);
            }

            switchOnOld = flickableComp.SwitchIsOn;
        }

        public override void DrawExtraSelectionOverlays()
        {
            if (sensorComp == null)
            {
                return;
            }

            if (sensorComp.auto)
            {
                GenDraw.DrawRadiusRing(Position, sensorComp.setRadius);
            }
        }

        public override void Draw() // maybe cleanup this code? looks like a spider for some reason
        {
            base.Draw();
            if (flickableComp.SwitchIsOn)
            {
                if (PowerComp.PowerNet.CurrentEnergyGainRate() == 0)
                {
                    if (LedState != ListOColours.Red)
                    {
                        UpdateTex(ListOColours.Red);
                    }
                }
                else
                {
                    if (LedState != ListOColours.Green)
                    {
                        UpdateTex(ListOColours.Green);
                    }
                }
            }
            else
            {
                if (LedState != ListOColours.Blackout)
                {
                    UpdateTex(ListOColours.Blackout);
                }
            }
        }

        private void UpdateTex(ColorInt col)
        {
            LedState = col;
            Notify_ColorChanged();
            Util.updateMap(Position, Map);
        }
    }
}