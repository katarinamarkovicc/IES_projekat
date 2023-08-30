using FTN.Services.NetworkModelService.DataModel.Additional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Switch  :  ConductingEquipment
    {
        private bool normalOpen;
        private float ratedCurrent;
        private bool retained;
        private int switchOnCount;
        private DateTime switchOnDate;

        public Switch(long globalId) : base(globalId)
        { 
        }

        public bool NormalOpen { get => normalOpen; set => normalOpen = value; }
        public float RatedCurrent { get => ratedCurrent; set => ratedCurrent = value; }
        public bool Retained { get => retained; set => retained = value; }
        public int SwitchOnCount { get => switchOnCount; set => switchOnCount = value; }
        public DateTime SwitchOnDate { get => switchOnDate; set => switchOnDate = value; }

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                Switch b = x as Switch;

                return this.normalOpen == b.normalOpen && this.ratedCurrent == b.ratedCurrent
                    && this.retained == b.retained && this.switchOnCount == b.switchOnCount
                    && this.switchOnDate == b.switchOnDate;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess Implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.SWICTH_NORMALOPEN:
                case ModelCode.SWICTH_RATEDCURRENT:
                case ModelCode.SWICTH_RETAINED:
                case ModelCode.SWITCH_SWITCHONCOUNT:
                case ModelCode.SWITCH_SWITCHONDATE:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SWICTH_NORMALOPEN:
                    property.SetValue(normalOpen);
                    break;
                case ModelCode.SWICTH_RATEDCURRENT:
                    property.SetValue(ratedCurrent);
                    break;
                case ModelCode.SWICTH_RETAINED:
                    property.SetValue(retained);
                    break;
                case ModelCode.SWITCH_SWITCHONCOUNT:
                    property.SetValue(switchOnCount);
                    break;
                case ModelCode.SWITCH_SWITCHONDATE:
                    property.SetValue(switchOnDate);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SWICTH_NORMALOPEN:
                    Aggregate = property.AsBool();
                    break;
                case ModelCode.SWICTH_RATEDCURRENT:
                    ratedCurrent = property.AsFloat();
                    break;
                case ModelCode.SWICTH_RETAINED:
                    retained = property.AsBool();
                    break;
                case ModelCode.SWITCH_SWITCHONCOUNT:
                    switchOnCount = property.AsInt();
                    break;
                case ModelCode.SWITCH_SWITCHONDATE:
                    switchOnDate = property.AsDateTime();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion
    }
}
