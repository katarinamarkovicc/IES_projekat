using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Additional
{
    public  class RegulatingControl :  PowerSystemResource
    {
        private bool discrete;
        private RegulatingControlModeKind mode;
        private PhaseCode monitoredPhase;
        private float targetRange;
        private float targetValue;
        private List<long> regulationSchedule = new List<long>();

        public RegulatingControl(long globalId) : base(globalId)
        { 
        }

        public bool Discrete { get => discrete; set => discrete = value; }
        public RegulatingControlModeKind Mode { get => mode; set => mode = value; }
        public PhaseCode MonitoredPhase { get => monitoredPhase; set => monitoredPhase = value; }
        public float TargetRange { get => targetRange; set => targetRange = value; }
        public float TargetValue { get => targetValue; set => targetValue = value; }
        public List<long> RegulationSchedule { get => regulationSchedule; set => regulationSchedule = value; }

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                RegulatingControl b = x as RegulatingControl;

                return this.discrete == b.discrete && this.mode == b.mode
                    && this.monitoredPhase == b.monitoredPhase && this.targetRange == b.targetRange
                    && this.targetValue == b.targetValue && CompareHelper.CompareLists(this.regulationSchedule, b.regulationSchedule);
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
                case ModelCode.REGULATINGCONTROL_DISCRETE:
                case ModelCode.REGULATINGCONTROL_MODE:
                case ModelCode.REGULATINGCONTROL_MONITOREDPHASE:
                case ModelCode.REGULATINGCONTROL_TARGETRANGE:
                case ModelCode.REGULATINGCONTROL_TARGETVALUE:
                case ModelCode.REGULATINGCONTROLE_REGULATIONSCHEDULE:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULATINGCONTROL_DISCRETE:
                    property.SetValue(discrete);
                    break;
                case ModelCode.REGULATINGCONTROL_MODE:
                    property.SetValue((short)mode);
                    break;
                case ModelCode.REGULATINGCONTROL_MONITOREDPHASE:
                    property.SetValue((short)monitoredPhase);
                    break;
                case ModelCode.REGULATINGCONTROL_TARGETRANGE:
                    property.SetValue(targetRange);
                    break;
                case ModelCode.REGULATINGCONTROL_TARGETVALUE:
                    property.SetValue(targetValue);
                    break;
                case ModelCode.REGULATINGCONTROLE_REGULATIONSCHEDULE:
                    property.SetValue(regulationSchedule);
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
                case ModelCode.REGULATINGCONTROL_DISCRETE:
                    discrete = property.AsBool();
                    break;
                case ModelCode.REGULATINGCONTROL_MODE:
                    mode = (RegulatingControlModeKind)property.AsEnum();
                    break;
                case ModelCode.REGULATINGCONTROL_MONITOREDPHASE:
                    monitoredPhase = (PhaseCode)property.AsEnum();
                    break;
                case ModelCode.REGULATINGCONTROL_TARGETRANGE:
                    targetRange = property.AsFloat();
                    break;
                case ModelCode.REGULATINGCONTROL_TARGETVALUE:
                    targetValue = property.AsFloat();
                    break;                 
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion

        #region IReference Implementation

        public override bool IsReferenced
        {
            get
            {
                return regulationSchedule.Count > 0 || base.IsReferenced;
            }

        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (regulationSchedule != null && regulationSchedule.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULATINGCONTROLE_REGULATIONSCHEDULE] = regulationSchedule.GetRange(0, regulationSchedule.Count);
            }
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL:
                    regulationSchedule.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL:
                    if (regulationSchedule.Contains(globalId))
                    {
                        regulationSchedule.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }
                    break;
                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion
    }
}
