using System;
using System.Collections.Generic;
using System.Text;
using PGL.IP.UserProgDotNetInterface;
using System.Runtime.Remoting;

[Serializable]
public partial class IPLink : IUserProgramExV2
{
    private IIntPetEx IPProxy;
    private List<int> inputCurves;
    private Dictionary<int, List<IGroupCurveInput>> groupedInputCurves;
    private List<int> outputCurves;
    private List<Single> inputParameters;
    private List<string> textInputParameters;
    private List<Boolean> flagInputParameters;
    private List<int> inArrayX;
    private List<int> inArrayY;
    private List<int> outArrayX; 
    private List<int> outArrayY;
    private List<int> parCnIn;
    private int topIndex;
    private int bottomIndex;
    private int totalZones;
    private int zoneNumber;
    
    
    public IPLink()
    {
    }
 

    #region IUserProgram implementation methods

    public void SetupParameters(int[] CnIn, int[] CnOut, Single[] ParIn,
                    string[] TxtParIn, Boolean[] FlagIn, int StIndex, int SpIndex, int ZoneNum, int TotZones,
                        int[] AXIn, int[] AYIn, int[] AXOut, int[] AYOut, int[] ParamCrvIn)
    {
        this.inputCurves = new List<int>(CnIn);
        this.outputCurves = new List<int>(CnOut);
        this.inputParameters = new List<Single>(ParIn);
        this.textInputParameters = new List<string>(TxtParIn);
        this.flagInputParameters = new List<Boolean>(FlagIn);
        this.topIndex = StIndex;
        this.bottomIndex = SpIndex;
        this.totalZones = TotZones;
        this.zoneNumber = ZoneNum;
        this.inArrayX = new List<int>(AXIn);
        this.inArrayY = new List<int>(AYIn);
        this.outArrayX = new List<int>(AXOut);
        this.outArrayY = new List<int>(AYOut);
        this.parCnIn = new List<int>(ParamCrvIn);
    }

    public void SetupParameters(int[] CnIn, Dictionary<int, List<IGroupCurveInput>> GrpCnIn, int[] CnOut, Single[] ParIn,
                    string[] TxtParIn, Boolean[] FlagIn, int StIndex, int SpIndex, int ZoneNum, int TotZones,
                    int[] AXIn, int[] AYIn, int[] AXOut, int[] AYOut, int[] ParamCrvIn)
    {
        SetupParameters(CnIn, CnOut, ParIn, TxtParIn, FlagIn, StIndex, SpIndex, ZoneNum, TotZones, AXIn, AYIn, AXOut, AYOut, ParamCrvIn);

        this.groupedInputCurves = GrpCnIn;
    }

    public void ResetZoneParameters(Single[] ParIn, string[] TxtParIn, Boolean[] FlagIn,
                     int[] ParamCrvIn, int StIndex, int SpIndex, int ZoneNum)
    {
        this.inputParameters = new List<Single>(ParIn);
        this.textInputParameters = new List<string>(TxtParIn);
        this.flagInputParameters = new List<Boolean>(FlagIn);
        this.parCnIn = new List<int>(ParamCrvIn);
        this.topIndex = StIndex;
        this.bottomIndex = SpIndex;
        this.zoneNumber = ZoneNum;
    }

    public void SetupIpProxy(IIntPetEx ip)
    {
        IPProxy = ip;
    }

    public void Run(){
        UserCode();
    }
    #endregion

    int TopDepth { get { return topIndex; } }
    int BottomDepth { get { return bottomIndex; } }
    int TotalZones { get { return totalZones; } }
    int ZoneNumber { get { return zoneNumber; } }

    public void SetZone(int zoneNum) { IPProxy.SetZone(zoneNum); }

    #region Well Text Methods

    string Well_Name { get { return Read_Well_Attribute("WellName"); } }
    string Well_Company { get { return Read_Well_Attribute("Company"); } }
    string Well_Field { get { return Read_Well_Attribute("Field"); } }

    string Read_Well_Attribute(string attributeName) { return IPProxy.GetWellText(attributeName); }
    void Write_Well_Attribute(string attributeName, string value) { IPProxy.SetWellText(attributeName, value); }

    string Read_Log_Attribute(string attributeName, int logRunNum) { return IPProxy.GetLogText(attributeName, logRunNum); }
    void Write_Log_Attribute(string attributeName, string value, int logRunNum) { IPProxy.SetLogText(attributeName, value, logRunNum); }
        
    #endregion

    #region Curve Text Methods
    
    string Read_Curve_Attribute(int curveNumber, string attributeName) { return IPProxy.GetText(2, curveNumber, attributeName); }
    void Write_Curve_Attribute(int curveNumber, string attributeName, string value) { IPProxy.SetText(2, curveNumber, attributeName, value); }
    
    #endregion

    #region General Attribute Methods
    
    // flags: 0 = Well, 1 = Log, 2 = Curve...
    string Read_Text(int flags, int index, string attributeName) { return IPProxy.GetText(flags, index, attributeName); }
    void Write_Text(int flags, int index, string attributeName, string value) { IPProxy.SetText(flags, index, attributeName, value); }
    
    #endregion
}

