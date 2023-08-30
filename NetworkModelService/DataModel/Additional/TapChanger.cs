using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Additional
{
    public class TapChanger : PowerSystemResource
    {
        private List<long> tapSchedules = new List<long>();

        public TapChanger(long globalId) : base(globalId)
        { 
        }

        public List<long> TapSchedules { get => tapSchedules; set => tapSchedules = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				TapChanger r = obj as TapChanger;

				return CompareHelper.CompareLists(this.tapSchedules, r.tapSchedules);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#region IAccess implementation

		public override bool HasProperty(ModelCode property)
		{
			switch (property)
			{
				case ModelCode.TAPCHANGER_TAPSCHEDULES:
					return true;
				default:
					return base.HasProperty(property);

			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.TAPCHANGER_TAPSCHEDULES:
					prop.SetValue(this.tapSchedules);
					break;
				default:
					base.GetProperty(prop);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			base.SetProperty(property);
		}

        #endregion IAccess implementation

        #region IReference Implementation

        public override bool IsReferenced
        {
            get
            {
                return tapSchedules.Count > 0 || base.IsReferenced;
            }

        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (tapSchedules != null && tapSchedules.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TAPCHANGER_TAPSCHEDULES] = tapSchedules.GetRange(0, tapSchedules.Count);
            }
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TAPSCHEDULE_TAPCHANGER:
                    tapSchedules.Add(globalId);
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
                case ModelCode.TAPSCHEDULE_TAPCHANGER:
                    if (tapSchedules.Contains(globalId))
                    {
                        tapSchedules.Remove(globalId);
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
