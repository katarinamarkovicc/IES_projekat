using FTN.Common;
namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	//using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIAS, cimIdentifiedObject.AliasName));
				}
			}
		}

		public static void PopulateBasicIntervalScheduleProperties(BasicIntervalSchedule cimBasicIntervalSchedule, ResourceDescription rd,ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimBasicIntervalSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);

				if (cimBasicIntervalSchedule.StartTimeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_STARTTIME, cimBasicIntervalSchedule.StartTime));
				}
				if (cimBasicIntervalSchedule.Value1MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTIPLIER, (short)GetDMSUnitMultiplier(cimBasicIntervalSchedule.Value1Multiplier)));	//DODATI METODU
				}
				if (cimBasicIntervalSchedule.Value1UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value1Unit)));	//DODATI METODU
				}
				if (cimBasicIntervalSchedule.Value2MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTIPLIER, (short)GetDMSUnitMultiplier(cimBasicIntervalSchedule.Value2Multiplier)));
				}
				if (cimBasicIntervalSchedule.Value2UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value2Unit))); 
				}
			}
		}

		public static void PopulateRegularTimePointProperties(RegularTimePoint cimRegularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimRegularTimePoint != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimRegularTimePoint, rd);
				if (cimRegularTimePoint.SequenceNumberHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_SEQUENCENUMBER, cimRegularTimePoint.SequenceNumber));
				}
				if (cimRegularTimePoint.Value1HasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VALUE1, cimRegularTimePoint.Value1));
				}
				if (cimRegularTimePoint.Value2HasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VALUE2, cimRegularTimePoint.Value2));
				}
				if (cimRegularTimePoint.IntervalScheduleHasValue)
				{
					//rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VALUE2, cimRegularTimePoint.Value2));

					long gid = importHelper.GetMappedGID(cimRegularTimePoint.IntervalSchedule.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimRegularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(cimRegularTimePoint.ID);
						report.Report.Append("\" - Failed to set reference to RegularIntervalSchedule: rdfID \"").Append(cimRegularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE, gid));
				}
			}
		}

		public static void PopulatePowerSystemResourceProperties(PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);

			}
		}

		public static void PopulateDayTypeProperties(DayType cimDayType, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimDayType != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimDayType, rd);

			}
		}

		public static void PopulateRegularIntervalScheduleProperties(RegularIntervalSchedule cimRegularIntervalSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimRegularIntervalSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateBasicIntervalScheduleProperties(cimRegularIntervalSchedule, rd, importHelper, report);
			}
		}

		public static void PopulateSeasonDayTypeScheduleProperties(SeasonDayTypeSchedule cimSeasonDayTypeSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSeasonDayTypeSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateRegularIntervalScheduleProperties(cimSeasonDayTypeSchedule, rd, importHelper, report);

				if (cimSeasonDayTypeSchedule.DayTypeHasValue)
				{
					long gid = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.DayType.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
						report.Report.Append("\" - Failed to set reference to DayType: rdfID \"").Append(cimSeasonDayTypeSchedule.DayType.ID).AppendLine(" \" is not mapped to GID!");

					}
					rd.AddProperty(new Property(ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE, gid));
				}				
			}
		}

		public static void PopulateEquipmentProperties(Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);

				if (cimEquipment.AggregateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
				}

				if (cimEquipment.NormallyInServiceHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYINSERVICE, cimEquipment.NormallyInService));
				}
			}
		}

		public static void PopulateConductingEquipmentProperties(ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);

			}
		}

		public static void PopulateSwitchProperties(Switch cimSwitch, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSwitch != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSwitch, rd, importHelper, report);

				if (cimSwitch.NormalOpenHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWICTH_NORMALOPEN, cimSwitch.NormalOpen));
				}
				if (cimSwitch.RatedCurrentHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWICTH_RATEDCURRENT, cimSwitch.RatedCurrent));
				}
				if (cimSwitch.Retained)
				{
					rd.AddProperty(new Property(ModelCode.SWICTH_RETAINED, cimSwitch.Retained));
				}
				if (cimSwitch.SwitchOnCountHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONCOUNT, cimSwitch.SwitchOnCount));
				}
				if (cimSwitch.SwitchOnDateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONDATE, cimSwitch.SwitchOnDate));
				}
			}
		}

		public static void PopulateTapChangerProperties(TapChanger cimTapChanger, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimTapChanger != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimTapChanger, rd, importHelper, report);


			}
		}

		public static void PopulateTapScheduleProperties(TapSchedule cimTapSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimTapSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateSeasonDayTypeScheduleProperties(cimTapSchedule, rd, importHelper, report);

				if (cimTapSchedule.TapChangerHasValue)
				{
					long gid = importHelper.GetMappedGID(cimTapSchedule.TapChanger.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimTapSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimTapSchedule.ID);
						report.Report.Append("\" - Failed to set reference to TapChanger: rdfID \"").Append(cimTapSchedule.TapChanger.ID).AppendLine(" \" is not mapped to GID!");

					}
					rd.AddProperty(new Property(ModelCode.TAPSCHEDULE_TAPCHANGER, gid));
				}
			}
		}
		
		public static void PopulateRegulatingControlProperties(RegulatingControl cimRegulatingControl, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimRegulatingControl != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimRegulatingControl, rd, importHelper, report);

				if (cimRegulatingControl.DiscreteHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_DISCRETE, cimRegulatingControl.Discrete));
				}
				if (cimRegulatingControl.ModeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_MODE, (short)GetDMSRegulatingControlModeKind(cimRegulatingControl.Mode))); //DODATI METODU
				}
				if (cimRegulatingControl.MonitoredPhaseHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_MONITOREDPHASE, (short)GetDMSPhaseCode(cimRegulatingControl.MonitoredPhase))); 
				}
				if (cimRegulatingControl.TargetRangeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TARGETRANGE, cimRegulatingControl.TargetRange));
				}
				if (cimRegulatingControl.TargetValueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TARGETVALUE, cimRegulatingControl.TargetValue));
				}
			}
		}

		public static void PopulateRegulationScheduleProperties(RegulationSchedule cimRegulationSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimRegulationSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateSeasonDayTypeScheduleProperties(cimRegulationSchedule, rd, importHelper, report);

				if (cimRegulationSchedule.RegulatingControlHasValue)
				{
					long gid = importHelper.GetMappedGID(cimRegulationSchedule.RegulatingControl.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimRegulationSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimRegulationSchedule.ID);
						report.Report.Append("\" - Failed to set reference to RegulatingControl: rdfID \"").Append(cimRegulationSchedule.RegulatingControl.ID).AppendLine(" \" is not mapped to GID!");

					}
					rd.AddProperty(new Property(ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL, gid));
				}
			}
		}

		#endregion Populate ResourceDescription
		

		#region Enums convert
		public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
		{
			switch (phases)
			{
				case FTN.PhaseCode.A:
					return PhaseCode.A;
				case FTN.PhaseCode.AB:
					return PhaseCode.AB;
				case FTN.PhaseCode.ABC:
					return PhaseCode.ABC;
				case FTN.PhaseCode.ABCN:
					return PhaseCode.ABCN;
				case FTN.PhaseCode.ABN:
					return PhaseCode.ABN;
				case FTN.PhaseCode.AC:
					return PhaseCode.AC;
				case FTN.PhaseCode.ACN:
					return PhaseCode.ACN;
				case FTN.PhaseCode.AN:
					return PhaseCode.AN;
				case FTN.PhaseCode.B:
					return PhaseCode.B;
				case FTN.PhaseCode.BC:
					return PhaseCode.BC;
				case FTN.PhaseCode.BCN:
					return PhaseCode.BCN;
				case FTN.PhaseCode.BN:
					return PhaseCode.BN;
				case FTN.PhaseCode.C:
					return PhaseCode.C;
				case FTN.PhaseCode.CN:
					return PhaseCode.CN;
				case FTN.PhaseCode.N:
					return PhaseCode.N;
				case FTN.PhaseCode.s12N:
					return PhaseCode.ABN;
				case FTN.PhaseCode.s1N:
					return PhaseCode.AN;
				case FTN.PhaseCode.s2N:
					return PhaseCode.BN;
				default: return PhaseCode.BN;
			}
		}

		public static UnitMultiplier GetDMSUnitMultiplier(UnitMultiplier unitMultiplier)
		{
			switch (unitMultiplier)				
			{
				case FTN.UnitMultiplier.G:
					return UnitMultiplier.G;
				case FTN.UnitMultiplier.M:
					return UnitMultiplier.M;
				case FTN.UnitMultiplier.T:
					return UnitMultiplier.T;
				case FTN.UnitMultiplier.c:
					return UnitMultiplier.c;
				case FTN.UnitMultiplier.d:
					return UnitMultiplier.d;
				case FTN.UnitMultiplier.k:
					return UnitMultiplier.k;
				case FTN.UnitMultiplier.m:
					return UnitMultiplier.m;
				case FTN.UnitMultiplier.micro:
					return UnitMultiplier.micro;
				case FTN.UnitMultiplier.n:
					return UnitMultiplier.n;
				case FTN.UnitMultiplier.none:
					return UnitMultiplier.none;
				case FTN.UnitMultiplier.p:
					return UnitMultiplier.p;
				default:
					return UnitMultiplier.d;
			}
		}

		public static UnitSymbol GetDMSUnitSymbol(UnitSymbol unitSymbol)
		{
			switch (unitSymbol)            
			{
				case FTN.UnitSymbol.A:
					return UnitSymbol.A;
				case FTN.UnitSymbol.F:
					return UnitSymbol.F;
				case FTN.UnitSymbol.H:
					return UnitSymbol.H;
				case FTN.UnitSymbol.Hz:
					return UnitSymbol.Hz;
				case FTN.UnitSymbol.J:
					return UnitSymbol.J;
				case FTN.UnitSymbol.N:
					return UnitSymbol.N;
				case FTN.UnitSymbol.Pa:
					return UnitSymbol.Pa;
				case FTN.UnitSymbol.S:
					return UnitSymbol.S;
				case FTN.UnitSymbol.V:
					return UnitSymbol.V;
				case FTN.UnitSymbol.VA:
					return UnitSymbol.VA;
				case FTN.UnitSymbol.VAh:
					return UnitSymbol.VAh;
				case FTN.UnitSymbol.VAr:
					return UnitSymbol.VAr;
				case FTN.UnitSymbol.VArh:
					return UnitSymbol.VArh;
				case FTN.UnitSymbol.W:
					return UnitSymbol.W;
				case FTN.UnitSymbol.Wh:
					return UnitSymbol.Wh;
				default:
					return UnitSymbol.J;
			}
		}

		public static RegulatingControlModeKind GetDMSRegulatingControlModeKind(RegulatingControlModeKind regulatingControlModeKind)
		{
			switch (regulatingControlModeKind)             
			{
				case FTN.RegulatingControlModeKind.activePower:
					return RegulatingControlModeKind.activePower;
				case FTN.RegulatingControlModeKind.admittance:
					return RegulatingControlModeKind.admittance;
				case FTN.RegulatingControlModeKind.currentFlow:
					return RegulatingControlModeKind.currentFlow;
				case FTN.RegulatingControlModeKind.powerFactor:
					return RegulatingControlModeKind.powerFactor;
				case FTN.RegulatingControlModeKind.reactivePower:
					return RegulatingControlModeKind.reactivePower;			
				case FTN.RegulatingControlModeKind.temperature:
					return RegulatingControlModeKind.temperature;
				case FTN.RegulatingControlModeKind.timeScheduled:
					return RegulatingControlModeKind.timeScheduled;
				case FTN.RegulatingControlModeKind.voltage:
					return RegulatingControlModeKind.voltage;
				default:
					return RegulatingControlModeKind.temperature;
			}
		}

		#endregion Enums convert
	}
}
