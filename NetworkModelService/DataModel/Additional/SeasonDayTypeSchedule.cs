using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Additional
{
    public class SeasonDayTypeSchedule : RegularIntervalSchedule
    {
        private long dayType;

        public SeasonDayTypeSchedule(long globalId) : base(globalId)
        { 
        }

        public long DayType { get => dayType; set => dayType = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				SeasonDayTypeSchedule r = obj as SeasonDayTypeSchedule;

				return this.dayType  == r.dayType;
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
				case ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE:
					return true;
				default:
					return base.HasProperty(property);

			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE:
					prop.SetValue(this.dayType);
					break;
				default:
					base.GetProperty(prop);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch(property.Id)
			{
				case ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE:
					dayType = property.AsLong();
					break;
				default:
					base.SetProperty(property);
					break;
			}
			
		}

		#endregion IAccess implementation

		#region IReference

		public override bool IsReferenced => dayType > 0 || base.IsReferenced;

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (dayType != 0 && (refType == TypeOfReference.Target && refType == TypeOfReference.Both))
			{
				references[ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE] = new List<long> { dayType };
			}
			base.GetReferences(references, refType);
		}

		#endregion
	}
}
