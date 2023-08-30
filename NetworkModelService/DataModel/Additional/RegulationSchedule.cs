using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Additional
{
   public class RegulationSchedule : SeasonDayTypeSchedule
    {
        private long regulatingControl;

        public RegulationSchedule(long globalId) : base(globalId)
        { 
        }

        public long RegulatingControl { get => regulatingControl; set => regulatingControl = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				RegulationSchedule r = obj as RegulationSchedule;

				return this.regulatingControl == r.regulatingControl;
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
				case ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL:
					return true;
				default:
					return base.HasProperty(property);

			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL:
					prop.SetValue(this.regulatingControl);
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
				case ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL:
					regulatingControl = property.AsLong();
					break;
				default:
					base.SetProperty(property);
					break;
			}

		}

		#endregion IAccess implementation

		#region IReference

		public override bool IsReferenced => regulatingControl > 0 || base.IsReferenced;

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (regulatingControl != 0 && (refType == TypeOfReference.Target && refType == TypeOfReference.Both))
			{
				references[ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL] = new List<long> { regulatingControl };
			}
			base.GetReferences(references, refType);
		}

		#endregion
	}
}
