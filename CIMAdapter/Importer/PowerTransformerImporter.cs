using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;



namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)
			ImportSwitch();
			ImportRegulatingControle();

			ImportDayType();
			ImportRegulationSchedule();
			ImportTapChanger();
			ImportTapSchedule();
			ImportRegularTimePoint();

			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

		#region Import

		//------------------------------------IMPORT------------------------------------------------------------

		private void ImportRegularTimePoint()
		{
			SortedDictionary<string, object> cimRegularTimePoint = concreteModel.GetAllObjectsOfType("FTN.RegularTimePoint");
			if (cimRegularTimePoint != null)
			{
				foreach (KeyValuePair<string, object> cimRegularTimePointPair in cimRegularTimePoint)
				{
					RegularTimePoint rtp = cimRegularTimePointPair.Value as RegularTimePoint;

					ResourceDescription rd = CreateRegularTimePointResourceDescription(rtp);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("RegularTimePoint ID = ").Append(rtp.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("RegularTimePoint ID = ").Append(rtp.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private void ImportDayType()
		{
			SortedDictionary<string, object> cimDayType = concreteModel.GetAllObjectsOfType("FTN.DayType");
			if (cimDayType != null)
			{
				foreach (KeyValuePair<string, object> cimDayTypePair in cimDayType)
				{
					DayType dt = cimDayTypePair.Value as DayType;

					ResourceDescription rd = CreateDayTypeResourceDescription(dt);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("DayType ID = ").Append(dt.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("DayType ID = ").Append(dt.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private void ImportSwitch()
		{
			SortedDictionary<string, object> cimSwitch = concreteModel.GetAllObjectsOfType("FTN.Switch");
			if (cimSwitch != null)
			{
				foreach (KeyValuePair<string, object> cimSwitchPair in cimSwitch)
				{
					Switch s = cimSwitchPair.Value as Switch;

					ResourceDescription rd = CreateSwitchResourceDescription(s);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Switch ID = ").Append(s.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("Switch ID = ").Append(s.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private void ImportTapChanger()
		{
			SortedDictionary<string, object> cimTapChanger = concreteModel.GetAllObjectsOfType("FTN.TapChanger");
			if (cimTapChanger != null)
			{
				foreach (KeyValuePair<string, object> cimTapChangerPair in cimTapChanger)
				{
					TapChanger tc = cimTapChangerPair.Value as TapChanger;

					ResourceDescription rd = CreateTapChangerResourceDescription(tc);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("TapChanger ID = ").Append(tc.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("TapChanger ID = ").Append(tc.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private void ImportTapSchedule()
		{
			SortedDictionary<string, object> cimTapSchedule = concreteModel.GetAllObjectsOfType("FTN.TapSchedule");
			if (cimTapSchedule != null)
			{
				foreach (KeyValuePair<string, object> cimTapSchedulePair in cimTapSchedule)
				{
					TapSchedule ts = cimTapSchedulePair.Value as TapSchedule;

					ResourceDescription rd = CreateTapScheduleResourceDescription(ts);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("TapSchedule ID = ").Append(ts.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("TapSchedule ID = ").Append(ts.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private void ImportRegulatingControle()
		{
			SortedDictionary<string, object> cimRegulatingControl = concreteModel.GetAllObjectsOfType("FTN.RegulatingControl");
			if (cimRegulatingControl != null)
			{
				foreach (KeyValuePair<string, object> cimRegulatingControlPair in cimRegulatingControl)
				{
					RegulatingControl rc = cimRegulatingControlPair.Value as RegulatingControl;

					ResourceDescription rd = CreateRegulatingControlResourceDescription(rc);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("RegulatingControl ID = ").Append(rc.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("RegulatingControl ID = ").Append(rc.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private void ImportRegulationSchedule()
		{
			SortedDictionary<string, object> cimRegulationSchedule = concreteModel.GetAllObjectsOfType("FTN.RegulationSchedule");
			if (cimRegulationSchedule != null)
			{
				foreach (KeyValuePair<string, object> cimRegulationSchedulePair in cimRegulationSchedule)
				{
					RegulationSchedule rs = cimRegulationSchedulePair.Value as RegulationSchedule;

					ResourceDescription rd = CreateRegulationScheduleResourceDescription(rs);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("RegulationSchedule ID = ").Append(rs.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("RegulationSchedule ID = ").Append(rs.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		//------------------------------------CREATE------------------------------------------------------------

		private ResourceDescription CreateRegularTimePointResourceDescription(RegularTimePoint cimRegularTimePoint)
		{
			ResourceDescription rd = null;
			if (cimRegularTimePoint != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULARTIMEPOINT, importHelper.CheckOutIndexForDMSType(DMSType.REGULARTIMEPOINT));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimRegularTimePoint.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateRegularTimePointProperties(cimRegularTimePoint, rd, importHelper, report);
			}
			return rd;
		}

		private ResourceDescription CreateDayTypeResourceDescription(DayType cimDayType)
		{
			ResourceDescription rd = null;
			if (cimDayType != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DAYTYPE, importHelper.CheckOutIndexForDMSType(DMSType.DAYTYPE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimDayType.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateDayTypeProperties(cimDayType, rd, importHelper, report);
			}
			return rd;
		}
	
		private ResourceDescription CreateSwitchResourceDescription(Switch cimSwitch)
		{
			ResourceDescription rd = null;
			if (cimSwitch != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SWITCH, importHelper.CheckOutIndexForDMSType(DMSType.SWITCH));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimSwitch.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateSwitchProperties(cimSwitch, rd, importHelper, report);
			}
			return rd;
		}

		private ResourceDescription CreateTapChangerResourceDescription(TapChanger cimTapChanger)
		{
			ResourceDescription rd = null;
			if (cimTapChanger != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TAPCHANGER, importHelper.CheckOutIndexForDMSType(DMSType.TAPCHANGER));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimTapChanger.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateTapChangerProperties(cimTapChanger, rd, importHelper, report);
			}
			return rd;
		}

		private ResourceDescription CreateTapScheduleResourceDescription(TapSchedule cimTapSchedule)
		{
			ResourceDescription rd = null;
			if (cimTapSchedule != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TAPSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.TAPSCHEDULE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimTapSchedule.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateTapScheduleProperties(cimTapSchedule, rd, importHelper, report);
			}
			return rd;
		}

		private ResourceDescription CreateRegulatingControlResourceDescription(RegulatingControl cimRegulatingControl)
		{
			ResourceDescription rd = null;
			if (cimRegulatingControl != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULATINGCONTROL, importHelper.CheckOutIndexForDMSType(DMSType.REGULATINGCONTROL));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimRegulatingControl.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateRegulatingControlProperties(cimRegulatingControl, rd, importHelper, report);
			}
			return rd;
		}

		private ResourceDescription CreateRegulationScheduleResourceDescription(RegulationSchedule cimRegulationSchedule)
		{
			ResourceDescription rd = null;
			if (cimRegulationSchedule != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULATIONSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.REGULATIONSCHEDULE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimRegulationSchedule.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateRegulationScheduleProperties(cimRegulationSchedule, rd, importHelper, report);
			}
			return rd;
		}

		#endregion Import
	}
}

