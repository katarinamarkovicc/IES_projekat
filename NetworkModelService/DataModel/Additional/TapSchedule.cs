using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Additional
{
    public class TapSchedule : SeasonDayTypeSchedule
    {
        private long tapChanger;

        public TapSchedule(long globalId) : base(globalId)
        { 
        }

        public long TapChanger { get => tapChanger; set => tapChanger = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				TapSchedule r = obj as TapSchedule;

				return this.tapChanger == r.tapChanger;
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
				case ModelCode.TAPSCHEDULE_TAPCHANGER:
					return true;
				default:
					return base.HasProperty(property);

			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.TAPSCHEDULE_TAPCHANGER:
					prop.SetValue(this.tapChanger);
					break;
				default:
					base.GetProperty(prop);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.TAPSCHEDULE_TAPCHANGER:
					tapChanger = property.AsLong();
					break;
				default:
					base.SetProperty(property);
					break;
			}

		}

		#endregion IAccess implementation

		#region IReference

		public override bool IsReferenced => tapChanger > 0 || base.IsReferenced;

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (tapChanger != 0 && (refType == TypeOfReference.Target && refType == TypeOfReference.Both))
			{
				references[ModelCode.TAPSCHEDULE_TAPCHANGER] = new List<long> { tapChanger };
			}
			base.GetReferences(references, refType);
		}

		#endregion
	}
}
