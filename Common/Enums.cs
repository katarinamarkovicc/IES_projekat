using System;

namespace FTN.Common
{	
	public enum PhaseCode : short
	{
		Unknown = 0x0,
		N = 0x1,
		C = 0x2,
		CN = 0x3,
		B = 0x4,
		BN = 0x5,
		BC = 0x6,
		BCN = 0x7,
		A = 0x8,
		AN = 0x9,
		AC = 0xA,
		ACN = 0xB,
		AB = 0xC,
		ABN = 0xD,
		ABC = 0xE,
		ABCN = 0xF
	}

	public enum UnitMultiplier : short
	{ 
		G = 0x1,
		M = 0x2,
		T = 0x3,
		c = 0x4, 
		d = 0x5,
		k = 0x6,
		m = 0x7,
		micro = 0x8,
		n = 0x9,
		none = 0xA,
		p = 0xB
	}

	public enum UnitSymbol : short
	{ 
		A = 0x1,
		F = 0x2,
		H = 0x3,
		Hz = 0x4,
		J = 0x5,
		N = 0x6,
		Pa = 0x7,
		S = 0x8,
		V = 0x9,
		VA = 0xA,
		VAh = 0xB,
		VAr = 0xC,
		VArh = 0xD,
		W = 0xE,
		Wh = 0xF
	}

	public enum RegulatingControlModeKind : short
	{ 
		activePower = 1,
		admittance = 2,
		currentFlow = 3,
		fix = 4,	//proveriti
		powerFactor = 5,
		reactivePower = 6,
		temperature = 7,
		timeScheduled = 8,
		voltage = 9
	}
}
