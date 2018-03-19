/***********************************************
 * File dynamically created from IP: 03/15/2018 16:03:35
 * DO NOT MANUALLY EDIT
/***********************************************/

using System;
using PGL.IP.API;
using System.Collections.Generic;
using PGL.IP.UserProgDotNetInterface;

public partial class IPLink
{
    const int FIRST_AVAILABLE_LOG_RUN = -1;
    const int LAST_AVAILABLE_LOG_RUN = -2;
    float Depth(int index) { return (float)IPProxy.GetCurveData(1, index); }

    float GR(int index) { return (float)IPProxy.GetCurveData(inputCurves[0], 0, index, true); }
    float Array_GR(int index, int xVal, int yVal) { return (float)IPProxy.GetCurveData(inputCurves[0], 0, index, xVal, yVal, true); }
    double[] Array_GR_XValues(int index, int yVal) { return IPProxy.GetXValues(inputCurves[0], 0, index, yVal); }
    String GR_Text(int index) { return IPProxy.GetTextCurveValue(inputCurves[0], index); }
    String Array_GR_Text(int index, int xVal, int yVal) { return IPProxy.GetTextCurveValue(inputCurves[0], index, xVal, yVal); }
    String GR_Name { get { return IPProxy.GetCurveText(inputCurves[0], 1); } }
    String GR_Units { get { return IPProxy.GetCurveText(inputCurves[0], 2); } }
    String GR_Comments { get { return IPProxy.GetCurveText(inputCurves[0], 3); } }
    void Save_GR_Comments(string newValue) { IPProxy.SetCurveText(inputCurves[0], 3, newValue); }
    String Get_GR_Attribute(string attributeName) { return IPProxy.GetText(2, inputCurves[0], attributeName); }
    void Set_GR_Attribute(string attributeName, string newValue) { IPProxy.SetText(2, inputCurves[0], attributeName, newValue); }
    IUnit GR_SourceUnit { get { return IPProxy.GetUnit(inputCurves[0]); } }
    IUnit GR_ExpectedUnit { get { return IPProxy.GetExpectedUnit(0, true); } }
    Boolean GR_IsSelected { get { return IPProxy.GetText(3, 0, "") == "True"; } }
    void Save_GR(int index, float value) { IPProxy.SetCurveData(inputCurves[0],0, index, value, true); }
    void Save_Array_GR(int index, int xVal, int yVal, float value) { IPProxy.SetCurveData(inputCurves[0],0, index, value, xVal, yVal, true); }
    void Save_GR_Text(int index, String value) { IPProxy.SetTextCurveValue(inputCurves[0], index, value); }
    void Save_Array_GR_Text(int index, int xVal, int yVal, String value) { IPProxy.SetTextCurveValue(inputCurves[0], index, value, xVal, yVal); }
    int Array_GR_MaxX { get { return inArrayX[0]; } }
    int Array_GR_MaxY { get { return inArrayY[0]; } }
    IEnumerable<IUserAppCurve> GR_Curves
    {
        get
        {
             if (groupedInputCurves.ContainsKey(0))
                foreach (IGroupCurveInput groupCurveInput in groupedInputCurves[0])
                   if (groupCurveInput != null && groupCurveInput.CurveId > 0)
                       yield return new UserAppCurve(IPProxy, 0, groupCurveInput);
        }
    }
    float RDEEP(int index) { return (float)IPProxy.GetCurveData(inputCurves[1], 1, index, true); }
    float Array_RDEEP(int index, int xVal, int yVal) { return (float)IPProxy.GetCurveData(inputCurves[1], 1, index, xVal, yVal, true); }
    double[] Array_RDEEP_XValues(int index, int yVal) { return IPProxy.GetXValues(inputCurves[1], 1, index, yVal); }
    String RDEEP_Text(int index) { return IPProxy.GetTextCurveValue(inputCurves[1], index); }
    String Array_RDEEP_Text(int index, int xVal, int yVal) { return IPProxy.GetTextCurveValue(inputCurves[1], index, xVal, yVal); }
    String RDEEP_Name { get { return IPProxy.GetCurveText(inputCurves[1], 1); } }
    String RDEEP_Units { get { return IPProxy.GetCurveText(inputCurves[1], 2); } }
    String RDEEP_Comments { get { return IPProxy.GetCurveText(inputCurves[1], 3); } }
    void Save_RDEEP_Comments(string newValue) { IPProxy.SetCurveText(inputCurves[1], 3, newValue); }
    String Get_RDEEP_Attribute(string attributeName) { return IPProxy.GetText(2, inputCurves[1], attributeName); }
    void Set_RDEEP_Attribute(string attributeName, string newValue) { IPProxy.SetText(2, inputCurves[1], attributeName, newValue); }
    IUnit RDEEP_SourceUnit { get { return IPProxy.GetUnit(inputCurves[1]); } }
    IUnit RDEEP_ExpectedUnit { get { return IPProxy.GetExpectedUnit(1, true); } }
    Boolean RDEEP_IsSelected { get { return IPProxy.GetText(3, 1, "") == "True"; } }
    void Save_RDEEP(int index, float value) { IPProxy.SetCurveData(inputCurves[1],1, index, value, true); }
    void Save_Array_RDEEP(int index, int xVal, int yVal, float value) { IPProxy.SetCurveData(inputCurves[1],1, index, value, xVal, yVal, true); }
    void Save_RDEEP_Text(int index, String value) { IPProxy.SetTextCurveValue(inputCurves[1], index, value); }
    void Save_Array_RDEEP_Text(int index, int xVal, int yVal, String value) { IPProxy.SetTextCurveValue(inputCurves[1], index, value, xVal, yVal); }
    int Array_RDEEP_MaxX { get { return inArrayX[1]; } }
    int Array_RDEEP_MaxY { get { return inArrayY[1]; } }
    IEnumerable<IUserAppCurve> RDEEP_Curves
    {
        get
        {
             if (groupedInputCurves.ContainsKey(1))
                foreach (IGroupCurveInput groupCurveInput in groupedInputCurves[1])
                   if (groupCurveInput != null && groupCurveInput.CurveId > 0)
                       yield return new UserAppCurve(IPProxy, 1, groupCurveInput);
        }
    }
    float testin1(int index) { return (float)IPProxy.GetCurveData(inputCurves[2], 2, index, true); }
    float Array_testin1(int index, int xVal, int yVal) { return (float)IPProxy.GetCurveData(inputCurves[2], 2, index, xVal, yVal, true); }
    double[] Array_testin1_XValues(int index, int yVal) { return IPProxy.GetXValues(inputCurves[2], 2, index, yVal); }
    String testin1_Text(int index) { return IPProxy.GetTextCurveValue(inputCurves[2], index); }
    String Array_testin1_Text(int index, int xVal, int yVal) { return IPProxy.GetTextCurveValue(inputCurves[2], index, xVal, yVal); }
    String testin1_Name { get { return IPProxy.GetCurveText(inputCurves[2], 1); } }
    String testin1_Units { get { return IPProxy.GetCurveText(inputCurves[2], 2); } }
    String testin1_Comments { get { return IPProxy.GetCurveText(inputCurves[2], 3); } }
    void Save_testin1_Comments(string newValue) { IPProxy.SetCurveText(inputCurves[2], 3, newValue); }
    String Get_testin1_Attribute(string attributeName) { return IPProxy.GetText(2, inputCurves[2], attributeName); }
    void Set_testin1_Attribute(string attributeName, string newValue) { IPProxy.SetText(2, inputCurves[2], attributeName, newValue); }
    IUnit testin1_SourceUnit { get { return IPProxy.GetUnit(inputCurves[2]); } }
    IUnit testin1_ExpectedUnit { get { return IPProxy.GetExpectedUnit(2, true); } }
    Boolean testin1_IsSelected { get { return IPProxy.GetText(3, 2, "") == "True"; } }
    void Save_testin1(int index, float value) { IPProxy.SetCurveData(inputCurves[2],2, index, value, true); }
    void Save_Array_testin1(int index, int xVal, int yVal, float value) { IPProxy.SetCurveData(inputCurves[2],2, index, value, xVal, yVal, true); }
    void Save_testin1_Text(int index, String value) { IPProxy.SetTextCurveValue(inputCurves[2], index, value); }
    void Save_Array_testin1_Text(int index, int xVal, int yVal, String value) { IPProxy.SetTextCurveValue(inputCurves[2], index, value, xVal, yVal); }
    int Array_testin1_MaxX { get { return inArrayX[2]; } }
    int Array_testin1_MaxY { get { return inArrayY[2]; } }
    IEnumerable<IUserAppCurve> testin1_Curves
    {
        get
        {
             if (groupedInputCurves.ContainsKey(2))
                foreach (IGroupCurveInput groupCurveInput in groupedInputCurves[2])
                   if (groupCurveInput != null && groupCurveInput.CurveId > 0)
                       yield return new UserAppCurve(IPProxy, 2, groupCurveInput);
        }
    }
    float testin2(int index) { return (float)IPProxy.GetCurveData(inputCurves[3], 3, index, true); }
    float Array_testin2(int index, int xVal, int yVal) { return (float)IPProxy.GetCurveData(inputCurves[3], 3, index, xVal, yVal, true); }
    double[] Array_testin2_XValues(int index, int yVal) { return IPProxy.GetXValues(inputCurves[3], 3, index, yVal); }
    String testin2_Text(int index) { return IPProxy.GetTextCurveValue(inputCurves[3], index); }
    String Array_testin2_Text(int index, int xVal, int yVal) { return IPProxy.GetTextCurveValue(inputCurves[3], index, xVal, yVal); }
    String testin2_Name { get { return IPProxy.GetCurveText(inputCurves[3], 1); } }
    String testin2_Units { get { return IPProxy.GetCurveText(inputCurves[3], 2); } }
    String testin2_Comments { get { return IPProxy.GetCurveText(inputCurves[3], 3); } }
    void Save_testin2_Comments(string newValue) { IPProxy.SetCurveText(inputCurves[3], 3, newValue); }
    String Get_testin2_Attribute(string attributeName) { return IPProxy.GetText(2, inputCurves[3], attributeName); }
    void Set_testin2_Attribute(string attributeName, string newValue) { IPProxy.SetText(2, inputCurves[3], attributeName, newValue); }
    IUnit testin2_SourceUnit { get { return IPProxy.GetUnit(inputCurves[3]); } }
    IUnit testin2_ExpectedUnit { get { return IPProxy.GetExpectedUnit(3, true); } }
    Boolean testin2_IsSelected { get { return IPProxy.GetText(3, 3, "") == "True"; } }
    void Save_testin2(int index, float value) { IPProxy.SetCurveData(inputCurves[3],3, index, value, true); }
    void Save_Array_testin2(int index, int xVal, int yVal, float value) { IPProxy.SetCurveData(inputCurves[3],3, index, value, xVal, yVal, true); }
    void Save_testin2_Text(int index, String value) { IPProxy.SetTextCurveValue(inputCurves[3], index, value); }
    void Save_Array_testin2_Text(int index, int xVal, int yVal, String value) { IPProxy.SetTextCurveValue(inputCurves[3], index, value, xVal, yVal); }
    int Array_testin2_MaxX { get { return inArrayX[3]; } }
    int Array_testin2_MaxY { get { return inArrayY[3]; } }
    IEnumerable<IUserAppCurve> testin2_Curves
    {
        get
        {
             if (groupedInputCurves.ContainsKey(3))
                foreach (IGroupCurveInput groupCurveInput in groupedInputCurves[3])
                   if (groupCurveInput != null && groupCurveInput.CurveId > 0)
                       yield return new UserAppCurve(IPProxy, 3, groupCurveInput);
        }
    }


    void Save_originalflag(int index, float value) { IPProxy.SetCurveData(outputCurves[0], index, value); }
    void Save_Array_originalflag(int index, int xVal, int yVal, float value) { IPProxy.SetCurveData(outputCurves[0], index, value, xVal, yVal); }
    void Save_originalflag_Text(int index, String value) { IPProxy.SetTextCurveValue(outputCurves[0], index, value); }
    void Save_Array_originalflag_Text(int index, int xVal, int yVal, String value) { IPProxy.SetTextCurveValue(outputCurves[0], index, value, xVal, yVal); }
    float originalflag(int index) { return (float)IPProxy.GetCurveData(outputCurves[0], 0, index); }
    float Array_originalflag(int index, int xVal, int yVal) { return (float)IPProxy.GetCurveData(outputCurves[0], 0, index, xVal, yVal); }
    String originalflag_Text(int index) { return IPProxy.GetTextCurveValue(outputCurves[0], index); }
    String Array_originalflag_Text(int index, int xVal, int yVal) { return IPProxy.GetTextCurveValue(outputCurves[0], index, xVal, yVal); }
    String originalflag_Name { get { return IPProxy.GetCurveText(outputCurves[0], 1); } }
    String originalflag_Units { get { return IPProxy.GetCurveText(outputCurves[0], 2); } }
    String originalflag_Comments { get { return IPProxy.GetCurveText(outputCurves[0], 3); } }
    void Save_originalflag_Comments(string newValue) { IPProxy.SetCurveText(outputCurves[0], 3, newValue); }
    int Array_originalflag_MaxX { get { return outArrayX[0]; } }
    int Array_originalflag_MaxY { get { return outArrayY[0]; } }
    String Get_originalflag_Attribute(string attributeName) { return IPProxy.GetText(2, outputCurves[0], attributeName); }
    void Set_originalflag_Attribute(string attributeName, string newValue) { IPProxy.SetText(2, outputCurves[0], attributeName, newValue); }

    void Save_classflag(int index, float value) { IPProxy.SetCurveData(outputCurves[1], index, value); }
    void Save_Array_classflag(int index, int xVal, int yVal, float value) { IPProxy.SetCurveData(outputCurves[1], index, value, xVal, yVal); }
    void Save_classflag_Text(int index, String value) { IPProxy.SetTextCurveValue(outputCurves[1], index, value); }
    void Save_Array_classflag_Text(int index, int xVal, int yVal, String value) { IPProxy.SetTextCurveValue(outputCurves[1], index, value, xVal, yVal); }
    float classflag(int index) { return (float)IPProxy.GetCurveData(outputCurves[1], 1, index); }
    float Array_classflag(int index, int xVal, int yVal) { return (float)IPProxy.GetCurveData(outputCurves[1], 1, index, xVal, yVal); }
    String classflag_Text(int index) { return IPProxy.GetTextCurveValue(outputCurves[1], index); }
    String Array_classflag_Text(int index, int xVal, int yVal) { return IPProxy.GetTextCurveValue(outputCurves[1], index, xVal, yVal); }
    String classflag_Name { get { return IPProxy.GetCurveText(outputCurves[1], 1); } }
    String classflag_Units { get { return IPProxy.GetCurveText(outputCurves[1], 2); } }
    String classflag_Comments { get { return IPProxy.GetCurveText(outputCurves[1], 3); } }
    void Save_classflag_Comments(string newValue) { IPProxy.SetCurveText(outputCurves[1], 3, newValue); }
    int Array_classflag_MaxX { get { return outArrayX[1]; } }
    int Array_classflag_MaxY { get { return outArrayY[1]; } }
    String Get_classflag_Attribute(string attributeName) { return IPProxy.GetText(2, outputCurves[1], attributeName); }
    void Set_classflag_Attribute(string attributeName, string newValue) { IPProxy.SetText(2, outputCurves[1], attributeName, newValue); }

    void Save_testflag(int index, float value) { IPProxy.SetCurveData(outputCurves[2], index, value); }
    void Save_Array_testflag(int index, int xVal, int yVal, float value) { IPProxy.SetCurveData(outputCurves[2], index, value, xVal, yVal); }
    void Save_testflag_Text(int index, String value) { IPProxy.SetTextCurveValue(outputCurves[2], index, value); }
    void Save_Array_testflag_Text(int index, int xVal, int yVal, String value) { IPProxy.SetTextCurveValue(outputCurves[2], index, value, xVal, yVal); }
    float testflag(int index) { return (float)IPProxy.GetCurveData(outputCurves[2], 2, index); }
    float Array_testflag(int index, int xVal, int yVal) { return (float)IPProxy.GetCurveData(outputCurves[2], 2, index, xVal, yVal); }
    String testflag_Text(int index) { return IPProxy.GetTextCurveValue(outputCurves[2], index); }
    String Array_testflag_Text(int index, int xVal, int yVal) { return IPProxy.GetTextCurveValue(outputCurves[2], index, xVal, yVal); }
    String testflag_Name { get { return IPProxy.GetCurveText(outputCurves[2], 1); } }
    String testflag_Units { get { return IPProxy.GetCurveText(outputCurves[2], 2); } }
    String testflag_Comments { get { return IPProxy.GetCurveText(outputCurves[2], 3); } }
    void Save_testflag_Comments(string newValue) { IPProxy.SetCurveText(outputCurves[2], 3, newValue); }
    int Array_testflag_MaxX { get { return outArrayX[2]; } }
    int Array_testflag_MaxY { get { return outArrayY[2]; } }
    String Get_testflag_Attribute(string attributeName) { return IPProxy.GetText(2, outputCurves[2], attributeName); }
    void Set_testflag_Attribute(string attributeName, string newValue) { IPProxy.SetText(2, outputCurves[2], attributeName, newValue); }





}
