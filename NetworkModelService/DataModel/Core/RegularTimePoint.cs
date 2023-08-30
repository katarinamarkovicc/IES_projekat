using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Additional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class RegularTimePoint : IdentifiedObject
    {
        private int sequenceNumber;
        private float value1;
        private float value2;
        private long intervalSchedule;

        public RegularTimePoint(long globalId) : base(globalId)
        { 
        }

        public int SequenceNumber { get => sequenceNumber; set => sequenceNumber = value; }
        public float Value1 { get => value1; set => value1 = value; }
        public float Value2 { get => value2; set => value2 = value; }
        public long IntervalSchedule { get => intervalSchedule; set => intervalSchedule = value; }

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                RegularTimePoint b = x as RegularTimePoint;

                return this.sequenceNumber == b.sequenceNumber && this.value1 == b.value1
                    && this.value2 == b.value2 && this.IntervalSchedule == b.IntervalSchedule;
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
                case ModelCode.REGULARTIMEPOINT_SEQUENCENUMBER:
                case ModelCode.REGULARTIMEPOINT_VALUE1:
                case ModelCode.REGULARTIMEPOINT_VALUE2:
                case ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULARTIMEPOINT_SEQUENCENUMBER:
                    property.SetValue(sequenceNumber);
                    break;
                case ModelCode.REGULARTIMEPOINT_VALUE1:
                    property.SetValue(value1);
                    break;
                case ModelCode.REGULARTIMEPOINT_VALUE2:
                    property.SetValue(value2);
                    break;
                case ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE:
                    property.SetValue(IntervalSchedule);
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
                case ModelCode.REGULARTIMEPOINT_SEQUENCENUMBER:
                    sequenceNumber = property.AsInt();
                    break;
                case ModelCode.REGULARTIMEPOINT_VALUE1:
                    value1 = property.AsFloat();
                    break;
                case ModelCode.REGULARTIMEPOINT_VALUE2:
                    value2 = property.AsFloat();
                    break;
                case ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE:
                    intervalSchedule = property.AsLong();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion

        #region IReference

        public override bool IsReferenced => intervalSchedule > 0 || base.IsReferenced;

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (intervalSchedule != 0 && (refType == TypeOfReference.Target && refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE] = new List<long> { intervalSchedule };
            }
            base.GetReferences(references, refType);
        }


        #endregion
    }
}
