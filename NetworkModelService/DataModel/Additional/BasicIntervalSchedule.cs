using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Additional
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        private DateTime startTime;
        private UnitMultiplier value1Multiplier;
        private UnitSymbol value1Unit;
        private UnitMultiplier value2Multiplier;
        private UnitSymbol value2Unit;

        public BasicIntervalSchedule(long globalId) : base(globalId)
        { 
        }

        public DateTime StartTime { get => startTime; set => startTime = value; }
        public UnitMultiplier Value1Multiplier { get => value1Multiplier; set => value1Multiplier = value; }
        public UnitSymbol Value1Unit { get => value1Unit; set => value1Unit = value; }
        public UnitMultiplier Value2Multiplier { get => value2Multiplier; set => value2Multiplier = value; }
        public UnitSymbol Value2Unit { get => value2Unit; set => value2Unit = value; }

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                BasicIntervalSchedule b = x as BasicIntervalSchedule;

                return this.StartTime == b.StartTime && this.Value1Multiplier == b.Value1Multiplier
                    && this.Value1Unit == b.Value1Unit && this.Value2Multiplier == b.Value2Multiplier
                    && this.Value2Unit == b.Value2Unit;
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
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                case ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTIPLIER:
                case ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT:
                case ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTIPLIER:
                case ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                    property.SetValue(startTime);
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTIPLIER:
                    property.SetValue((short)value1Multiplier);
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT:
                    property.SetValue((short)value1Unit);
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTIPLIER:
                    property.SetValue((short)value2Multiplier);
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT:
                    property.SetValue((short)value2Multiplier);
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
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                    startTime = property.AsDateTime();
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTIPLIER:
                    value1Multiplier = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT:
                    value1Unit = (UnitSymbol)property.AsEnum();
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTIPLIER:
                    value2Multiplier = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT:
                    value2Unit = (UnitSymbol)property.AsEnum();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion
    }
}
