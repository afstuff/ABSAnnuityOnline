Imports System.Data.OleDb
Imports System.Data

Partial Class Annuity_PRG_ANNUITY_POLY_PREM
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    'Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    'Protected BufferStr As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strF_ID As String
    Protected strQ_ID As String
    Protected strP_ID As String

    Protected strP_TYPE As String
    Protected strP_DESC As String

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strTmp_Value As String = ""
    Dim myarrData() As String

    Dim strErrMsg As String

    ' additional premium variable
    Dim dblAdd_Prem_Amt As Double = 0
    Dim dblAdd_Prem_Rate As Double = 0
    Dim dblAdd_Rate_Per As Integer = 0
    Dim dblAdd_Prem_SA_LC As Double
    Dim dblAdd_Prem_SA_FC As Double

    Dim dblTotal_Add_Prem_LC As Double = 0
    Dim dblTotal_Add_Prem_FC As Double = 0

    Dim dblTotal_Prem_LC As Double = 0
    Dim dblTotal_Prem_FC As Double = 0

    Dim dblAnnual_Basic_Prem_LC As Double = 0
    Dim dblAnnual_Basic_Prem_FC As Double = 0

    Dim dblTmp_Amt As Double = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strTableName = "TBIL_ANN_POLICY_PREM_INFO"

        STRMENU_TITLE = "Proposal Screen"
        'STRMENU_TITLE = "Investment Plus Proposal"

        Try
            strF_ID = CType(Request.QueryString("optfileid"), String)
        Catch ex As Exception
            strF_ID = ""
        End Try

        Try
            strQ_ID = CType(Request.QueryString("optquotid"), String)
        Catch ex As Exception
            strQ_ID = ""
        End Try

        Try
            strP_ID = CType(Request.QueryString("optpolid"), String)
        Catch ex As Exception
            strP_ID = ""
        End Try


        If Not (Page.IsPostBack) Then
            Me.lblMsg.Text = "Status:"

            'Me.optPrem_Applied_SA.Checked = True
            'Me.optPrem_Applied_Prem.Checked = False
            'Me.txtPrem_Rate_Applied_On.Text = "S"
            'Me.txtPrem_Is_SA_From_PremNum.Text = "N"
            'Call gnProc_DDL_Get(Me.cboPrem_Is_SA_From_Prem, RTrim(Me.txtPrem_Is_SA_From_PremNum.Text))

            Me.cmdPrev.Enabled = True
            Me.cmdNext.Enabled = False
            Call gnProc_Populate_Box("IL_CODE_LIST", "017", Me.cboPrem_SA_Currency)
            Me.cboPrem_MOP_Type.Items.Clear()
            'Call gnProc_Populate_Box("IL_MOP_FACTOR_LIST", "IND", Me.cboPrem_MOP_Type)

            If Trim(strF_ID) <> "" Then
                Me.txtFileNum.Text = RTrim(strF_ID)
                '      Me.txtQuote_Num.Text = RTrim(strQ_ID)
                Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_ANNUITY_BY_FILE_NO", RTrim(strF_ID), RTrim(""), RTrim(""))
                If oAL.Item(0) = "TRUE" Then
                    '    'Retrieve the record
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
                    Me.txtQuote_Num.Text = oAL.Item(3)
                    Me.txtPolNum.Text = oAL.Item(4)
                    Me.txtProductClass.Text = oAL.Item(5)
                    Me.txtProduct_Num.Text = oAL.Item(6)
                    Me.txtPlan_Num.Text = oAL.Item(7)
                    Me.txtCover_Num.Text = oAL.Item(8)
                    Me.txtDOB.Text = oAL.Item(9)
                    Me.txtDOB_ANB.Text = oAL.Item(10)
                    Me.cmdNext.Enabled = True
                    LoadModeofPayment(txtProduct_Num.Text)
                    If UCase(oAL.Item(18).ToString) = "A" Then
                        Me.cmdNew_ASP.Visible = False
                        'Me.cmdSave_ASP.Visible = False
                        Me.cmdDelete_ASP.Visible = False
                        Me.cmdPrint_ASP.Visible = False
                    End If
                Else
                    '    'Destroy i.e remove the array list object from memory
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    Me.lblMsg.Text = "Status: " & oAL.Item(1)
                End If
                oAL = Nothing
            End If

            Call gnProc_Populate_Box("AN_RATE_TYPE_CODE_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPrem_Rate_Code)

            If Trim(strF_ID) <> "" Then
                strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
            End If
            'If Trim(strQ_ID) <> "" Then
            '    Me.txtQuote_Num.Text = RTrim(strQ_ID)
            'End If
            'If Trim(strP_ID) <> "" Then
            '    Me.txtPolNum.Text = RTrim(strP_ID)
            'End If

            'If Trim(Me.txtProduct_Num.Text) = "E001" Then
            '    Me.lblPrem_School_Term.Enabled = True
            '    Me.txtPrem_School_Term.Enabled = True
            '    Me.txtPrem_School_Term_Name.Enabled = True
            '    Me.lblPrem_Sch_Fee_Prd.Enabled = True
            '    Me.txtPrem_Sch_Fee_Prd.Enabled = True
            'Else
            '    Me.lblPrem_School_Term.Enabled = False
            '    Me.txtPrem_School_Term.Enabled = False
            '    Me.txtPrem_School_Term_Name.Enabled = False
            '    Me.lblPrem_Sch_Fee_Prd.Enabled = False
            '    Me.txtPrem_Sch_Fee_Prd.Enabled = False
            'End If
        End If

        If Me.txtAction.Text = "Save" Then
            'Call Proc_DoSave()
            Me.txtAction.Text = ""

        End If

        If Me.txtAction.Text = "Delete" Then
            'Call Proc_DoDelete()
            Me.txtAction.Text = ""
        End If

    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        'Call Proc_DoSave()
        Call Proc_DoSave1()
        Me.txtAction.Text = ""

    End Sub

    Private Sub DoGet_SelectedItem(ByVal pvDDL_Control As DropDownList, ByVal pvCtr_Value As TextBox, ByVal pvCtr_Text As TextBox, Optional ByVal pvCtr_Label As Label = Nothing)
        Try
            If pvDDL_Control.SelectedIndex = -1 Or pvDDL_Control.SelectedIndex = 0 Or _
            pvDDL_Control.SelectedItem.Value = "" Or pvDDL_Control.SelectedItem.Value = "*" Then
                pvCtr_Value.Text = ""
                pvCtr_Text.Text = ""
            Else
                pvCtr_Value.Text = pvDDL_Control.SelectedItem.Value
                pvCtr_Text.Text = pvDDL_Control.SelectedItem.Text
            End If
        Catch ex As Exception
            If pvCtr_Label IsNot Nothing Then
                If TypeOf pvCtr_Label Is System.Web.UI.WebControls.Label Then
                    pvCtr_Label.Text = "Error. Reason: " & ex.Message.ToString
                End If
            End If
        End Try
    End Sub

    Protected Sub DoProc_Currency_Code_Change()
        Me.lblMsg.Text = ""
        Me.txtPrem_Start_Date.Text = Trim(Me.txtPrem_Start_Date.Text)
        If RTrim(Me.txtPrem_Start_Date.Text) = "" Or Len(Trim(Me.txtPrem_Start_Date.Text)) <> 10 Then
            Me.cboPrem_SA_Currency.SelectedIndex = -1
            Me.lblMsg.Text = "Sorry. You must enter the TRANSACTION START DATE before the " & Me.lblPrem_SA_Currency.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim strMyYear As String = ""
        Dim strMyMth As String = ""
        Dim strMyDay As String = ""

        Dim strMyDte As String = ""
        Dim strMyDteX As String = ""

        Dim dteStart As Date = Now

        Dim mydteX As String = ""
        Dim mydte As Date = Now

        'Validate date
        myarrData = Split(Me.txtPrem_Start_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.cboPrem_SA_Currency.SelectedIndex = -1
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblPrem_Start_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            Me.cboPrem_SA_Currency.SelectedIndex = -1
            Me.lblMsg.Text = "Please enter valid date..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Exit Sub
        End If
        Me.txtPrem_Start_Date.Text = RTrim(strMyDte)
        'mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteStart = Format(mydte, "MM/dd/yyyy")


        Call DoGet_SelectedItem(Me.cboPrem_SA_Currency, Me.txtPrem_SA_CurrencyCode, Me.txtPrem_SA_CurrencyName, Me.lblMsg)
        If Trim(Me.txtPrem_SA_CurrencyCode.Text) = "" Then
            Me.txtPrem_Exchange_Rate.Text = "0.00"
        Else
            Dim myRetValue As String = "0"
            myRetValue = MOD_GEN.gnGET_RATE("GET_IL_EXCHANGE_RATE", "IND", Me.txtPrem_SA_CurrencyCode.Text, Format(dteStart, "MM/dd/yyyy"), RTrim(""), RTrim(""), Me.lblMsg)
            If Left(LTrim(myRetValue), 3) = "ERR" Then
                'Me.cboPrem_SA_Currency.SelectedIndex = -1
                Me.txtPrem_Exchange_Rate.Text = "0.00"
            Else
                Me.txtPrem_Exchange_Rate.Text = myRetValue.ToString
            End If
        End If


    End Sub


    Protected Sub DoProc_Premium_Code_Change()
        Call DoGet_SelectedItem(Me.cboPrem_Rate_Code, Me.txtPrem_Rate_Code, Me.txtPrem_Rate_CodeName, Me.lblMsg)
        If Trim(Me.txtPrem_Rate_Code.Text) = "" Then
            Me.txtPrem_Rate.Text = "0.00"
            Me.txtPrem_Rate_Per.Text = "0"
        Else
            Dim myRetValue As String = "0"
            Dim myTerm As String = ""
            myTerm = Me.txtPrem_Period_Yr.Text
            Select Case UCase(Me.txtProduct_Num.Text)
                Case "A001"
                    myTerm = "2"
                Case "F001", "F002"
                    myTerm = "1"
            End Select

            'Response.Write("<BR/>Rate Code: " & Me.txtPrem_Rate_Code.Text)
            'Response.Write("<BR/>Product Code: " & Me.txtProduct_Num.Text)
            'Response.Write("<BR/>Period: " & myTerm)
            'Response.Write("<BR/>Age: " & Me.txtDOB_ANB.Text)

            myRetValue = MOD_GEN.gnGET_RATE("GET_AN_PREMIUM_RATE", "A", Me.txtPrem_Rate_Code.Text, Me.txtProduct_Num.Text, myTerm, Me.txtDOB_ANB.Text, Me.lblMsg, Me.txtPrem_Rate_Per)
            If Left(LTrim(myRetValue), 3) = "ERR" Then
                Me.cboPrem_Rate_Code.SelectedIndex = -1
                Me.txtPrem_Rate.Text = "0.00"
                Me.txtPrem_Rate_Per.Text = "0"
            Else
                Me.txtPrem_Rate.Text = myRetValue.ToString
            End If
        End If

    End Sub

    Protected Sub DoProc_Rate_Type_Change()
        'Me.lblPrem_Rate_Applied_On.Enabled = False
        'Me.optPrem_Applied_SA.Enabled = False
        'Me.optPrem_Applied_SA.Checked = False
        'Me.optPrem_Applied_Prem.Enabled = False
        'Me.optPrem_Applied_Prem.Checked = False

        Call DoGet_SelectedItem(Me.cboPrem_Rate_Type, Me.txtPrem_Rate_TypeNum, Me.txtPrem_Rate_TypeName, Me.lblMsg)
        If Trim(Me.txtPrem_Rate_TypeNum.Text) = "F" Then
            Me.lblPrem_Fixed_Rate.Enabled = True
            Me.txtPrem_Fixed_Rate.Enabled = True
            Me.lblPrem_Fixed_Rate_Per.Enabled = True
            Me.cboPrem_Fixed_Rate_Per.Enabled = True
            Me.lblPrem_Rate_Code.Enabled = False
            Me.cboPrem_Rate_Code.Enabled = False
            'Me.txtPrem_Rate_Code.Enabled = False
            'Me.lblPrem_Rate.Enabled = False
            'Me.txtPrem_Rate.Enabled = False
            'Me.txtPrem_Rate_Per.Enabled = False
        ElseIf Trim(Me.txtPrem_Rate_TypeNum.Text) = "N" Then
            Me.lblPrem_Fixed_Rate.Enabled = False
            Me.txtPrem_Fixed_Rate.Enabled = False
            Me.lblPrem_Fixed_Rate_Per.Enabled = False
            Me.cboPrem_Fixed_Rate_Per.Enabled = False
            Me.lblPrem_Rate_Code.Enabled = False
            Me.cboPrem_Rate_Code.Enabled = False
            'Me.txtPrem_Rate_Code.Enabled = False
            'Me.lblPrem_Rate.Enabled = False
            'Me.txtPrem_Rate.Enabled = False
            'Me.txtPrem_Rate_Per.Enabled = False
        ElseIf Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" Then
            Me.lblPrem_Fixed_Rate.Enabled = False
            Me.txtPrem_Fixed_Rate.Enabled = False
            Me.lblPrem_Fixed_Rate_Per.Enabled = False
            Me.cboPrem_Fixed_Rate_Per.Enabled = False
            Me.lblPrem_Rate_Code.Enabled = True
            Me.cboPrem_Rate_Code.Enabled = True
            'Me.txtPrem_Rate_Code.Enabled = True
            'Me.lblPrem_Rate.Enabled = True
            'Me.txtPrem_Rate.Enabled = True
            'Me.txtPrem_Rate_Per.Enabled = True
            'Me.lblPrem_Rate_Applied_On.Enabled = True
            'Me.optPrem_Applied_SA.Enabled = True
            'Me.optPrem_Applied_SA.Checked = True
            'Me.optPrem_Applied_Prem.Enabled = True
            'Me.optPrem_Applied_Prem.Checked = True

        End If

    End Sub

    Protected Sub DoProc_MOP_Change()
        Call DoGet_SelectedItem(Me.cboPrem_MOP_Type, Me.txtPrem_MOP_Type, Me.txtPrem_MOP_TypeName, Me.lblMsg)
        If Trim(Me.txtPrem_MOP_Type.Text) = "" Then
            Me.txtPrem_MOP_Rate.Text = "0.00"
            Me.txtPrem_MOP_TypeName.Text = ""
            Me.lblMsg.Text = "Missing " & Me.lblPrem_MOP_Type.Text
        Else
            Dim myRetValue As String = "0"
            myRetValue = MOD_GEN.gnGET_RATE("GET_IL_MOP_FACTOR", "IND", Me.txtPrem_MOP_Type.Text, "", "", "", Me.lblMsg, Me.txtPrem_MOP_TypeName)
            If Left(LTrim(myRetValue), 3) = "ERR" Then
                Me.cboPrem_MOP_Type.SelectedIndex = -1
                Me.txtPrem_MOP_Rate.Text = "0.00"
                Me.txtPrem_MOP_TypeName.Text = ""
            Else
                Me.txtPrem_MOP_Rate.Text = myRetValue.ToString
            End If
        End If

    End Sub

    Private Sub Proc_DoSave1()
        Dim strMyYear As String = ""
        Dim strMyMth As String = ""
        Dim strMyDay As String = ""

        Dim strMyDte As String = ""

        Dim dteStart As Date = Now
        Dim dteEnd As Date = Now

        Dim mydteX As String = ""
        Dim mydte As Date = Now



        Dim myTmp_RecStatus = CType(Session("myTmp_RecStatus"), String)

        Me.lblMsg.Text = ""

        If Me.txtFileNum.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtQuote_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'added by femi
        If txtTBIL_ANN_POL_PRM_ANNUAL_PCENT_PYMT_INCREASE.Text <> "" Then
            If Not IsNumeric(txtTBIL_ANN_POL_PRM_ANNUAL_PCENT_PYMT_INCREASE.Text) Then
                Me.lblMsg.Text = "Missing or invalid " & Me.lblPrem_MOP_Type0.Text
                FirstMsg = "Javascript:alert('" & Me.lblPrem_MOP_Type0.Text & "')"
                Exit Sub
            Else

            End If
        Else
            Me.lblMsg.Text = "Missing " & Me.lblPrem_MOP_Type0.Text
            FirstMsg = "Javascript:alert('" & Me.lblPrem_MOP_Type0.Text & "')"
        End If

        'added by femi
        If txtTBIL_ANN_POL_PRM_DEATH_BENEFIT_ANNUAL_FACTOR.Text <> "" Then
            If Not IsNumeric(txtTBIL_ANN_POL_PRM_DEATH_BENEFIT_ANNUAL_FACTOR.Text) Then
                Me.lblMsg.Text = "Missing or invalid " & Me.lblPrem_End_Date0.Text
                FirstMsg = "Javascript:alert('" & Me.lblPrem_End_Date0.Text & "')"
                Exit Sub
            Else

            End If
        Else
            Me.lblMsg.Text = "Missing " & Me.lblPrem_End_Date0.Text
            FirstMsg = "Javascript:alert('" & Me.lblPrem_End_Date0.Text & "')"
        End If

        If Me.txtAnnualAnnuityLC.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPrem_Ann_Contrib_LC.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtProductClass.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblProduct.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtProduct_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblProduct.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


        Me.txtPrem_Period_Yr.Text = Trim(Me.txtPrem_Period_Yr.Text)
        If Me.txtPrem_Period_Yr.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPrem_Period_Yr.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Not IsNumeric(Me.txtPrem_Period_Yr.Text) Then
            Me.lblMsg.Text = "Invalid " & Me.lblPrem_Period_Yr.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtPrem_Start_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblPrem_Start_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)
        Me.txtPrem_Start_Date.Text = Trim(strMyDte)

        If RTrim(Me.txtPrem_Start_Date.Text) = "" Or Len(Trim(Me.txtPrem_Start_Date.Text)) <> 10 Then
            Me.lblMsg.Text = "Missing or Invalid date - " & Me.lblPrem_Start_Date.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtPrem_Start_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblPrem_Start_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            Me.lblMsg.Text = "Please enter valid date..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Exit Sub
        End If
        Me.txtPrem_Start_Date.Text = RTrim(strMyDte)
        'mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteStart = Format(mydte, "MM/dd/yyyy")

        dteEnd = DateAdd(DateInterval.Year, Val(Me.txtPrem_Period_Yr.Text), dteStart)
        Me.txtPrem_End_Date.Text = Format(dteEnd, "dd/MM/yyyy")

        'VALIDATION for DATA NOT OLD START - first pass
        If myTmp_RecStatus <> "OLD" Then
            'Call gnGET_SelectedItem(Me.cboPrem_MOP_Type, Me.txtPrem_MOP_Type, Me.txtPrem_MOP_TypeName, Me.lblMsg)
            Call DoProc_MOP_Change()
            If Me.txtPrem_MOP_Type.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblPrem_MOP_Type.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            If Trim(Me.txtPrem_Start_Date.Text) <> "" Then
                Call DoProc_Currency_Code_Change()
            End If


            Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_MOP_Rate)
            Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Exchange_Rate)



            'Select Case UCase(Me.txtProduct_Num.Text)
            '    Case "F001", "F002"
            '        If Val(Me.txtPrem_Enrollee_Num.Text) = 0 Then
            '            Me.lblMsg.Text = "Missing " & Me.lblPrem_Enrollee_Num.Text
            '            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            '            Exit Sub
            '        End If
            '    Case Else
            '        Me.txtPrem_Enrollee_Num.Text = "0"
            'End Select

            'Call gnGET_SelectedItem(Me.cboPrem_Allocation_YN, Me.txtPrem_Allocation_YN, Me.txtPrem_Allocation_YN_Name, Me.lblMsg)
            'If Me.txtPrem_Allocation_YN.Text = "" Then
            '    'txtPrem_Allocation_YN.Text = "N"
            '    Me.lblMsg.Text = "Missing " & Me.lblPrem_Allocation_YN.Text
            '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            '    Exit Sub
            'End If


            'Call gnGET_SelectedItem(Me.cboPrem_Bonus_YN, Me.txtPrem_Bonus_YN, Me.txtPrem_Bonus_YN_Name, Me.lblMsg)
            'If Me.txtPrem_Bonus_YN.Text = "" Then
            '    'txtPrem_Bonus_YN.Text = "N"
            '    Me.lblMsg.Text = "Missing " & Me.lblPrem_Bonus_YN.Text
            '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            '    Exit Sub
            'End If

            Call gnGET_SelectedItem(Me.cboPrem_Rate_Type, Me.txtPrem_Rate_TypeNum, Me.txtPrem_Rate_TypeName, Me.lblMsg)
            If Me.txtPrem_Rate_TypeNum.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblPrem_Rate_Type.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Fixed_Rate)
            If Trim(Me.txtPrem_Rate_TypeNum.Text) = "F" And Val(Me.txtPrem_Fixed_Rate.Text) = 0 Then
                Me.lblMsg.Text = "Missing Fixed Rate. Please enter valid fixed rate..."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            Call gnGET_SelectedItem(Me.cboPrem_Fixed_Rate_Per, Me.txtPrem_Fixed_Rate_PerNum, Me.txtPrem_Fixed_Rate_PerName, Me.lblMsg)
            If Trim(Me.txtPrem_Rate_TypeNum.Text) = "F" And Val(Me.txtPrem_Fixed_Rate_PerNum.Text) = 0 Then
                Me.lblMsg.Text = "Missing " & Me.lblPrem_Fixed_Rate_Per.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

        End If '        'VALIDATION for DATA NOT OLD END- first pass

        'Call gnGET_SelectedItem(Me.cboPrem_Life_Cover, Me.txtPrem_Life_CoverNum, Me.txtPrem_Life_CoverName, Me.lblMsg)
        'If Me.txtPrem_Life_CoverNum.Text = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblPrem_Life_Cover.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If



        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try


        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try


        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_ANN_POL_PRM_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
        'If Val(LTrim(RTrim(Me.txtRecNo.Text))) <> 0 Then
        '    strSQL = strSQL & " AND TBIL_ANN_POL_PRM_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
        'End If


        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        'or
        'objDA.SelectCommand = New System.Data.OleDb.OleDbCommand(strSQL, objOleConn)

        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        'Dim m_rwContact As System.Data.DataRow
        Dim intC As Integer = 0


        ' Try

        objDA.Fill(obj_DT)

        If obj_DT.Rows.Count = 0 Then
            '   Creating a new record

            Dim drNewRow As System.Data.DataRow
            drNewRow = obj_DT.NewRow()

            drNewRow("TBIL_ANN_POL_PRM_MDLE") = RTrim("A")
            drNewRow("TBIL_ANN_POL_PRM_FILE_NO") = RTrim(Me.txtFileNum.Text)
            drNewRow("TBIL_ANN_POL_PRM_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
            drNewRow("TBIL_ANN_POL_PRM_POLY_NO") = RTrim(Me.txtPolNum.Text)
            drNewRow("TBIL_ANN_POL_PRM_PRDCT_CD") = RTrim(Me.txtProduct_Num.Text)
            drNewRow("TBIL_ANN_POL_PRM_PLAN_CD") = RTrim(Me.txtPlan_Num.Text)
            drNewRow("TBIL_ANN_POLY_COVER_CD") = RTrim(Me.txtProduct_Num.Text)
            drNewRow("TBIL_ANN_PRDCT_DTL_CAT") = RTrim(Me.txtProductClass.Text)
            drNewRow("TBIL_ANN_POL_PRM_PERIOD_YRS") = Val(Trim(Me.txtPrem_Period_Yr.Text))
            'drNewRow("TBIL_ANN_POL_PRM_RATE") = RTrim(Me.txtPrem_Rate.Text)

            If Trim(Me.txtPrem_Start_Date.Text) <> "" Then
                drNewRow("TBIL_ANN_POL_PRM_FROM") = dteStart
            End If
            If Trim(Me.txtPrem_End_Date.Text) <> "" Then
                drNewRow("TBIL_ANN_POL_PRM_TO") = dteEnd
            End If

            drNewRow("TBIL_ANN_POL_PRM_MODE_PAYT") = RTrim(Me.txtPrem_MOP_Type.Text)
            'drNewRow("TBIL_ANN_POL_PRM_MOP_RATE") = RTrim(Me.txtPrem_MOP_Rate.Text)
            drNewRow("TBIL_ANN_POL_PRM_CURRCY") = Trim(Me.txtPrem_SA_CurrencyCode.Text)
            drNewRow("TBIL_ANN_POL_PRM_EXCHG_RATE") = RTrim(Me.txtPrem_Exchange_Rate.Text)

            'new fields
            'drNewRow("TBIL_ANN_POL_PURCHASE_LC") = Trim(txtPurchaseAnnuityLC.Text)
            'drNewRow("TBIL_ANN_POL_PURCHASE_FC") = Trim(txtPurchaseAnnuityFC.Text)

            If IsNumeric(txtPurchaseAnnuityLC.Text) Then
                drNewRow("TBIL_ANN_POL_PURCHASE_LC") = CType(Trim(txtPurchaseAnnuityLC.Text), Double)
            End If
            If IsNumeric(txtPurchaseAnnuityFC.Text) Then
                drNewRow("TBIL_ANN_POL_PURCHASE_FC") = CType(Trim(txtPurchaseAnnuityFC.Text), Double)
            End If
            If IsNumeric(txtPremiumWithLumpSumLC.Text) Then
                drNewRow("TBIL_ANN_POL_PREM_WITH_LS_LC") = CType(Trim(txtPremiumWithLumpSumLC.Text), Double)
            End If
            If IsNumeric(txtPremiumWithLumpSumFC.Text) Then
                drNewRow("TBIL_ANN_POL_PREM_WITH_LS_FC") = CType(Trim(txtPremiumWithLumpSumFC.Text), Double)
            End If
            If IsNumeric(txtPremiumWithoutLumpSumLC.Text) Then
                drNewRow("TBIL_ANN_POL_PREM_WITHOUT_LS_LC") = CType(Trim(txtPremiumWithoutLumpSumLC.Text), Double)
            End If
            If IsNumeric(txtPremiumWithoutLumpSumFC.Text) Then
                drNewRow("TBIL_ANN_POL_PREM_WITHOUT_LS_FC") = CType(Trim(txtPremiumWithoutLumpSumFC.Text), Double)
            End If
            If IsNumeric(txtAnnualAnnuityLC.Text) Then
                drNewRow("TBIL_ANN_ANNUAL_LC") = CType(Trim(txtAnnualAnnuityLC.Text), Double)
            End If
            If IsNumeric(txtAnnualAnnuityFC.Text) Then
                drNewRow("TBIL_ANN_ANNUAL_FC") = CType(Trim(txtAnnualAnnuityFC.Text), Double)
            End If
            If IsNumeric(txtMonthlyAnnuityLC.Text) Then
                drNewRow("TBIL_ANN_MONTHLY_LC") = CType(Trim(txtMonthlyAnnuityLC.Text), Double)
            End If
            If IsNumeric(txtMonthlyAnnuityFC.Text) Then
                drNewRow("TBIL_ANN_MONTHLY_FC") = CType(Trim(txtMonthlyAnnuityFC.Text), Double)
            End If

            drNewRow("TBIL_ANN_POL_PRM_RT_TAB_FIX") = Trim(Me.txtPrem_Rate_TypeNum.Text)
            drNewRow("TBIL_ANN_POL_PRM_RT_FIXED") = Trim(Me.txtPrem_Fixed_Rate.Text)
            If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" Then
                drNewRow("TBIL_ANN_POL_PRM_RT_FIX_PER") = 0.0
            Else
                drNewRow("TBIL_ANN_POL_PRM_RT_FIX_PER") = CType(Trim(Me.txtPrem_Fixed_Rate_PerNum.Text), Double)
            End If


            'condition added by femi reason b'cos the value is empty but it expects double/numeric value
            If RTrim(Me.txtPrem_Rate_Per.Text) <> "" Then
                drNewRow("TBIL_ANN_POL_PRM_RATE_PER") = RTrim(Me.txtPrem_Rate_Per.Text)
            Else
                drNewRow("TBIL_ANN_POL_PRM_RATE_PER") = 0.0
            End If

            drNewRow("TBIL_ANN_POL_PRM_ANNUAL_PCENT_PYMT_INCREASE") = txtTBIL_ANN_POL_PRM_ANNUAL_PCENT_PYMT_INCREASE.Text
            drNewRow("TBIL_ANN_POL_PRM_DEATH_BENEFIT_ANNUAL_FACTOR") = txtTBIL_ANN_POL_PRM_DEATH_BENEFIT_ANNUAL_FACTOR.Text


            drNewRow("TBIL_ANN_POL_PRM_FLAG") = "A"
            drNewRow("TBIL_ANN_POL_PRM_OPERID") = CType(myUserIDX, String)
            drNewRow("TBIL_ANN_POL_PRM_KEYDTE") = Now

            obj_DT.Rows.Add(drNewRow)
            'obj_DT.AcceptChanges()
            intC = objDA.Update(obj_DT)

            drNewRow = Nothing

            Me.lblMsg.Text = "New Record Saved to Database Successfully."

        Else
            '   Update existing record
            With obj_DT
                '.Rows(0)("TBIL_ANN_POL_PRM_FILE_NO") = RTrim(Me.txtFileNum.Text)


                .Rows(0)("TBIL_ANN_POL_PRM_MDLE") = RTrim("A")
                .Rows(0)("TBIL_ANN_POL_PRM_FILE_NO") = RTrim(Me.txtFileNum.Text)
                .Rows(0)("TBIL_ANN_POL_PRM_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                .Rows(0)("TBIL_ANN_POL_PRM_POLY_NO") = RTrim(Me.txtPolNum.Text)
                .Rows(0)("TBIL_ANN_POL_PRM_PRDCT_CD") = RTrim(Me.txtProduct_Num.Text)
                .Rows(0)("TBIL_ANN_POL_PRM_PLAN_CD") = RTrim(Me.txtPlan_Num.Text)
                .Rows(0)("TBIL_ANN_POLY_COVER_CD") = RTrim(Me.txtProduct_Num.Text)
                .Rows(0)("TBIL_ANN_PRDCT_DTL_CAT") = RTrim(Me.txtProductClass.Text)
                .Rows(0)("TBIL_ANN_POL_PRM_PERIOD_YRS") = Val(Trim(Me.txtPrem_Period_Yr.Text))
                '.Rows(0)("TBIL_ANN_POL_PRM_RATE") = RTrim(Me.txtPrem_Rate.Text)

                If Trim(Me.txtPrem_Start_Date.Text) <> "" Then
                    .Rows(0)("TBIL_ANN_POL_PRM_FROM") = dteStart
                End If
                If Trim(Me.txtPrem_End_Date.Text) <> "" Then
                    .Rows(0)("TBIL_ANN_POL_PRM_TO") = dteEnd
                End If

                .Rows(0)("TBIL_ANN_POL_PRM_MODE_PAYT") = RTrim(Me.txtPrem_MOP_Type.Text)
                '.Rows(0)("TBIL_ANN_POL_PRM_MOP_RATE") = RTrim(Me.txtPrem_MOP_Rate.Text)
                .Rows(0)("TBIL_ANN_POL_PRM_CURRCY") = Trim(Me.txtPrem_SA_CurrencyCode.Text)
                .Rows(0)("TBIL_ANN_POL_PRM_EXCHG_RATE") = RTrim(Me.txtPrem_Exchange_Rate.Text)

                'new fields
                '.Rows(0)("TBIL_ANN_POL_PURCHASE_LC") = Trim(txtPurchaseAnnuityLC.Text)
                '.Rows(0)("TBIL_ANN_POL_PURCHASE_FC") = Trim(txtPurchaseAnnuityFC.Text)

                If IsNumeric(txtPurchaseAnnuityLC.Text) Then
                    .Rows(0)("TBIL_ANN_POL_PURCHASE_LC") = CType(Trim(txtPurchaseAnnuityLC.Text), Double)
                End If
                If IsNumeric(txtPurchaseAnnuityFC.Text) Then
                    .Rows(0)("TBIL_ANN_POL_PURCHASE_FC") = CType(Trim(txtPurchaseAnnuityFC.Text), Double)
                End If
                If IsNumeric(txtPremiumWithLumpSumLC.Text) Then
                    .Rows(0)("TBIL_ANN_POL_PREM_WITH_LS_LC") = CType(Trim(txtPremiumWithLumpSumLC.Text), Double)
                End If
                If IsNumeric(txtPremiumWithLumpSumFC.Text) Then
                    .Rows(0)("TBIL_ANN_POL_PREM_WITH_LS_FC") = CType(Trim(txtPremiumWithLumpSumFC.Text), Double)
                End If
                If IsNumeric(txtPremiumWithoutLumpSumLC.Text) Then
                    .Rows(0)("TBIL_ANN_POL_PREM_WITHOUT_LS_LC") = CType(Trim(txtPremiumWithoutLumpSumLC.Text), Double)
                End If
                If IsNumeric(txtPremiumWithoutLumpSumFC.Text) Then
                    .Rows(0)("TBIL_ANN_POL_PREM_WITHOUT_LS_FC") = CType(Trim(txtPremiumWithoutLumpSumFC.Text), Double)
                End If
                If IsNumeric(txtAnnualAnnuityLC.Text) Then
                    .Rows(0)("TBIL_ANN_ANNUAL_LC") = CType(Trim(txtAnnualAnnuityLC.Text), Double)
                End If
                If IsNumeric(txtAnnualAnnuityFC.Text) Then
                    .Rows(0)("TBIL_ANN_ANNUAL_FC") = CType(Trim(txtAnnualAnnuityFC.Text), Double)
                End If
                If IsNumeric(txtMonthlyAnnuityLC.Text) Then
                    .Rows(0)("TBIL_ANN_MONTHLY_LC") = CType(Trim(txtMonthlyAnnuityLC.Text), Double)
                End If
                If IsNumeric(txtMonthlyAnnuityFC.Text) Then
                    .Rows(0)("TBIL_ANN_MONTHLY_FC") = CType(Trim(txtMonthlyAnnuityFC.Text), Double)
                End If

                .Rows(0)("TBIL_ANN_POL_PRM_RT_TAB_FIX") = Trim(Me.txtPrem_Rate_TypeNum.Text)
                .Rows(0)("TBIL_ANN_POL_PRM_RT_FIXED") = Trim(Me.txtPrem_Fixed_Rate.Text)
                If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" Then
                    .Rows(0)("TBIL_ANN_POL_PRM_RT_FIX_PER") = 0.0
                Else
                    .Rows(0)("TBIL_ANN_POL_PRM_RT_FIX_PER") = CType(Trim(Me.txtPrem_Fixed_Rate_PerNum.Text), Double)
                End If


                'condition added by femi reason b'cos the value is empty but it expects double/numeric value
                If RTrim(Me.txtPrem_Rate_Per.Text) <> "" Then
                    .Rows(0)("TBIL_ANN_POL_PRM_RATE_PER") = RTrim(Me.txtPrem_Rate_Per.Text)
                Else
                    .Rows(0)("TBIL_ANN_POL_PRM_RATE_PER") = 0.0
                End If

                .Rows(0)("TBIL_ANN_POL_PRM_ANNUAL_PCENT_PYMT_INCREASE") = txtTBIL_ANN_POL_PRM_ANNUAL_PCENT_PYMT_INCREASE.Text
                .Rows(0)("TBIL_ANN_POL_PRM_DEATH_BENEFIT_ANNUAL_FACTOR") = txtTBIL_ANN_POL_PRM_DEATH_BENEFIT_ANNUAL_FACTOR.Text


                .Rows(0)("TBIL_ANN_POL_PRM_FLAG") = "C"
                '.Rows(0)("TBIL_ANN_POL_PRM_OPERID") = CType(myUserIDX, String)
                '.Rows(0)("TBIL_ANN_POL_PRM_KEYDTE") = Now

            End With

            'obj_DT.AcceptChanges()
            intC = objDA.Update(obj_DT)

            Me.lblMsg.Text = "Record Saved to Database Successfully."

        End If

        'Catch ex As Exception
        '    Me.lblMsg.Text = ex.Message.ToString
        '    Exit Sub
        'End Try

        obj_DT.Dispose()
        obj_DT = Nothing

        m_cbCommandBuilder.Dispose()
        m_cbCommandBuilder = Nothing

        If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
            objDA.SelectCommand.Connection.Close()
        End If
        objDA.Dispose()
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing



        Me.cmdNext.Enabled = True

        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"





    End Sub


    Private Sub Proc_DoCalc_Annuity()

        '******************************************************
        ' START CODES - CALCULATE SUM ASSURED FROM PREMIUM
        '******************************************************
        Dim annualAnnuity As Double = Nothing
        Dim monthlyAnnuity As Double = Nothing
        Dim purchaseAmount As Double = Nothing
        Dim rate As Double = Nothing
        Dim ratePer As Double = Nothing

        'Dim mystrCONN As String = CType(Session("connstr"), String)
        'Dim objOLEConn As New OleDbConnection(mystrCONN)

        'Try
        '    'open connection to database
        '    objOLEConn.Open()
        'Catch ex As Exception
        '    Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
        '    objOLEConn = Nothing
        '    Exit Sub
        'End Try


        '******************************************************
        ' START CODES - Annuity
        '******************************************************
        Select Case Trim(Me.txtPlan_Num.Text)
            Case "A001"

                dblTmp_Amt = 0

                Select Case UCase(Trim(txtPrem_Rate_TypeNum.Text))
                    Case "N"
                        'this is to call a NO_RATE method on ANNUITY

                    Case "F"
                        'this is to do a fixed_RATE method on ANNUITY

                        If txtPrem_Fixed_Rate.Text <> "" Then
                            If IsNumeric(Trim(txtPrem_Fixed_Rate.Text)) Then
                                rate = CType(Trim(Me.txtPrem_Fixed_Rate.Text), Double)
                            End If
                        Else
                            Me.lblMsg.Text = "Missing Fixed Rate"
                            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                            Exit Sub
                        End If
                        If txtPurchaseAnnuityLC.Text <> "" Then
                            If IsNumeric(Trim(txtPurchaseAnnuityLC.Text)) Then
                                purchaseAmount = CType(Trim(txtPurchaseAnnuityLC.Text), Double)
                            End If
                        Else
                            Me.lblMsg.Text = "Missing Purchase Ammount"
                            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                            Exit Sub
                        End If
                        If txtPurchaseAnnuityLC.Text <> "" Then
                            If IsNumeric(Trim(txtPurchaseAnnuityLC.Text)) Then
                                purchaseAmount = CType(Trim(txtPurchaseAnnuityLC.Text), Double)
                            End If
                        Else
                            Me.lblMsg.Text = "Missing Purchase Ammount"
                            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                            Exit Sub
                        End If
                        If cboPrem_Fixed_Rate_Per.SelectedIndex <> 0 Then
                            If IsNumeric(Trim(cboPrem_Fixed_Rate_Per.SelectedValue)) Then
                                ratePer = CType(Trim(cboPrem_Fixed_Rate_Per.SelectedValue), Double)
                            End If
                        Else
                            Me.lblMsg.Text = "Missing Fixed Rate Per"
                            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                            Exit Sub
                        End If
                        annualAnnuity = (purchaseAmount * rate) / ratePer
                        If cboPrem_MOP_Type.SelectedValue = "M" Then
                            monthlyAnnuity = annualAnnuity / 12
                        ElseIf cboPrem_MOP_Type.SelectedValue = "Q" Then
                            monthlyAnnuity = annualAnnuity / 4
                        ElseIf cboPrem_MOP_Type.SelectedValue = "H" Then
                            monthlyAnnuity = annualAnnuity / 2
                        ElseIf cboPrem_MOP_Type.SelectedValue = "Y" Then
                            monthlyAnnuity = annualAnnuity
                        End If
                        txtAnnualAnnuityLC.Text = Format(CType(annualAnnuity, Decimal), "N2")
                        txtAnnualAnnuityFC.Text = Format(CType(annualAnnuity, Decimal), "N2")
                        'Format(CType(dr("TBIL_CLM_PAID_SA_AMT_LC"), Decimal), "N2")
                        txtMonthlyAnnuityLC.Text = Format(CType(monthlyAnnuity, Decimal), "N2")
                        txtMonthlyAnnuityFC.Text = Format(CType(monthlyAnnuity, Decimal), "N2")


                    Case "T"

                        'this is to do a Table_RATE method on ANNUITY

                        If txtPrem_Rate.Text <> "" Then
                            If IsNumeric(Trim(txtPrem_Rate.Text)) Then
                                rate = CType(Trim(Me.txtPrem_Rate.Text), Double)
                                ratePer = CType(Trim(Me.txtPrem_Rate_Per.Text), Double)
                                purchaseAmount = CType(Trim(txtPurchaseAnnuityLC.Text), Double)
                            End If
                        Else
                            Me.lblMsg.Text = "Missing Prem. Table Rate "
                            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                            Exit Sub
                        End If

                        If rate <> 0 And ratePer <> 0 Then
                            annualAnnuity = (purchaseAmount * rate) / ratePer

                            If cboPrem_MOP_Type.SelectedValue = "M" Then
                                monthlyAnnuity = annualAnnuity / 12
                            ElseIf cboPrem_MOP_Type.SelectedValue = "Q" Then
                                monthlyAnnuity = annualAnnuity / 4
                            ElseIf cboPrem_MOP_Type.SelectedValue = "H" Then
                                monthlyAnnuity = annualAnnuity / 2
                            ElseIf cboPrem_MOP_Type.SelectedValue = "Y" Then
                                monthlyAnnuity = annualAnnuity
                            End If
                        End If

                        txtAnnualAnnuityLC.Text = Format(CType(annualAnnuity, Decimal), "N2")
                        txtAnnualAnnuityFC.Text = Format(CType(annualAnnuity, Decimal), "N2")


                        'Format(CType(dr("TBIL_CLM_PAID_SA_AMT_LC"), Decimal), "N2")
                        txtMonthlyAnnuityLC.Text = Format(CType(monthlyAnnuity, Decimal), "N2")
                        txtMonthlyAnnuityFC.Text = Format(CType(monthlyAnnuity, Decimal), "N2")

                End Select

        End Select

        '******************************************************
        ' END CODES - Annuity
        '******************************************************


        'If objOLEConn.State = ConnectionState.Open Then
        '    objOLEConn.Close()
        'End If
        'objOLEConn = Nothing


    End Sub


    Private Sub Proc_DoCalc_AddPrem(ByVal pv_objOLEConn As OleDbConnection)
        '***********************************************************************************************
        ' START ADDITIONAL COVERS CODES 
        '***********************************************************************************************

        '====================================
        '(f)	Calculate Additional Premium
        '====================================

        'Read Records from the Additional Policy Cover Table (TBIL_ANN_POLICY_ADD_PREM) for the policy
        '(f1)	If TBIL_ANN_POL_ADD_PREM_RT_AMT_CD  = ‘A’    i.e Use Amount, 
        'Add  TBIL_ANN_POL_ADD_PREM_AMT to Total Additional Premium.

        '(f2)	If TBIL_ANN_POL_ADD_PREM_RT_AMT_CD = ‘F’   fixed rate  
        'and TBIL_ANN_POL_ADD_RATE_APPLY =    ‘P’      apply fixed rate on premium
        'Compute Additional Premium = TBIL_ANN_POL_DTL_BASIC_PREM_ LC
        ' 	Multiplied by TBIL_ANN_POL_ADD_PREM_FX_RATE  Divided by TBIL_ANN_POL_ADD_PREM_FX_RT_PER.
        'Add Additional Premium to Total Additional Premium

        '(f3)	if TBIL_ANN_POL_ADD_PREM_RT_AMT_CD = ‘R’    i.e use Rate Table
        '	Use TBIL_ANN_POL_ADD_MDLE and TBIL_ANN_POL_ADD_PREM_RT_CD.
        '	Product Code,  Policy-Tenor, and Assured Age to read  the Premium Rate Table (TBIL_ANN_PREM_RATE)
        '	Pick – Premium Rate   TBIL_ANN_PRM_RT_RATE
        '		-Rate Per    TBIL_ANN_PRM_RT_PER from the Table.
        'Compute Additional Premium as
        '	TBIL_ANN_POL_ADD_SA_LC  multiplied by 
        '	TBIL_ANN_PRM_RT_RATE divided by
        '                TBIL_ANN_PRM_RT_PER()
        '	Add Additional Premium to Total Additional Premium
        '(g)	Do the above additional Premium Routine for all additional records for the Policy.

        '(h)	Move the Total Additional Premium to
        '                TBIL_ANN_POL_PRM_DTL_ADDPREM_LC()
        '                TBIL_ANN_POL_PRM_DTL_ADDPREM_FC()

        'Display on screen

        'Compute TBIL_ANN_POL_PRM_DTL_TOT_PRM_LC = 
        'TBIL_ANN_PRM_DTL_BASIC_PRM_LC + TBIL_ANN_POL_PRM_DTL_ADDPREM_LC.

        'Move to FC
        'Display on screen

        dblAdd_Prem_Amt = 0
        dblTotal_Add_Prem_LC = 0

        strTable = strTableName
        strTable = "TBIL_ANN_POLICY_ADD_PREM"


        strSQL = ""
        strSQL = strSQL & "SELECT ADD_TBL.*"
        strSQL = strSQL & " FROM " & strTable & " AS ADD_TBL"
        strSQL = strSQL & " WHERE ADD_TBL.TBIL_ANN_POL_ADD_FILE_NO = '" & RTrim(strREC_ID) & "'"
        'If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
        'strSQL = strSQL & " AND ADD_TBL.TBIL_ANN_POL_ADD_REC_ID = '" & Val(FVstrRecNo) & "'"
        'End If
        strSQL = strSQL & " AND ADD_TBL.TBIL_ANN_POL_ADD_PROP_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND ADD_TBL.TBIL_ANN_POL_ADD_POLY_NO = '" & RTrim(strP_ID) & "'"

        'strSQL = "SPIL_GET_POLICY_ADD_PREM"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, pv_objOLEConn)
        'objOLECmd.CommandTimeout = 180
        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.CommandType = CommandType.StoredProcedure
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 30).Value = strREC_ID
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()

        Do While objOLEDR.Read()

            Select Case UCase(Trim(CType(objOLEDR("TBIL_ANN_POL_ADD_PREM_RT_AMT_CD") & vbNullString, String)))
                Case "A"
                    dblAdd_Prem_Amt = 0
                    dblTmp_Amt = 0
                    If IsNumeric(CType(objOLEDR("TBIL_ANN_POL_ADD_PREM_AMT") & vbNullString, String)) Then
                        dblAdd_Prem_Amt = CType(objOLEDR("TBIL_ANN_POL_ADD_PREM_AMT") & vbNullString, Double)
                    End If
                    dblTmp_Amt = dblAdd_Prem_Amt
                    dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt

                Case "F"
                    dblTmp_Amt = 0
                    dblAdd_Prem_Rate = 0
                    dblAdd_Rate_Per = 0
                    dblAdd_Prem_SA_LC = 0
                    dblAdd_Prem_SA_FC = 0

                    If IsNumeric(CType(objOLEDR("TBIL_ANN_POL_ADD_PREM_FX_RT") & vbNullString, String)) Then
                        dblAdd_Prem_Rate = CType(objOLEDR("TBIL_ANN_POL_ADD_PREM_FX_RT") & vbNullString, Double)
                    End If
                    If IsNumeric(CType(objOLEDR("TBIL_ANN_POL_ADD_PREM_FX_RT_PER") & vbNullString, String)) Then
                        dblAdd_Rate_Per = CType(objOLEDR("TBIL_ANN_POL_ADD_PREM_FX_RT_PER") & vbNullString, Double)
                    End If
                    If IsNumeric(CType(objOLEDR("TBIL_ANN_POL_ADD_SA_LC") & vbNullString, String)) Then
                        dblAdd_Prem_SA_LC = CType(objOLEDR("TBIL_ANN_POL_ADD_SA_LC") & vbNullString, Double)
                    End If
                    dblAdd_Prem_SA_FC = dblAdd_Prem_SA_LC


                    Select Case Trim(CType(objOLEDR("TBIL_ANN_POL_ADD_RATE_APPLY") & vbNullString, String))
                        Case "P"    'compute additional premium base on basic premium
                            If Val(dblAnnual_Basic_Prem_LC) <> 0 And Val(dblAdd_Prem_Rate) <> 0 And Val(dblAdd_Rate_Per) <> 0 Then
                                dblTmp_Amt = dblAnnual_Basic_Prem_LC * dblAdd_Prem_Rate / dblAdd_Rate_Per
                            End If
                            dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt

                        Case "S"    'compute additional premium base on sum assured
                            If Val(dblAdd_Prem_SA_LC) <> 0 And Val(dblAdd_Prem_Rate) <> 0 And Val(dblAdd_Rate_Per) <> 0 Then
                                dblTmp_Amt = dblAdd_Prem_SA_LC * dblAdd_Prem_Rate / dblAdd_Rate_Per
                            End If
                            dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt
                    End Select

                Case "R"    'compute additional premium using the rate table
                    dblTmp_Amt = 0
                    dblAdd_Prem_Rate = 0
                    dblAdd_Rate_Per = 0
                    dblAdd_Prem_SA_LC = 0
                    dblAdd_Prem_SA_FC = 0

                    If IsNumeric(CType(objOLEDR("TBIL_ANN_POL_ADD_RATE") & vbNullString, String)) Then
                        dblAdd_Prem_Rate = CType(objOLEDR("TBIL_ANN_POL_ADD_RATE") & vbNullString, Double)
                    End If
                    If IsNumeric(CType(objOLEDR("TBIL_ANN_POL_ADD_RATE_PER") & vbNullString, String)) Then
                        dblAdd_Rate_Per = CType(objOLEDR("TBIL_ANN_POL_ADD_RATE_PER") & vbNullString, Double)
                    End If
                    If IsNumeric(CType(objOLEDR("TBIL_ANN_POL_ADD_SA_LC") & vbNullString, String)) Then
                        dblAdd_Prem_SA_LC = CType(objOLEDR("TBIL_ANN_POL_ADD_SA_LC") & vbNullString, Double)
                    End If
                    dblAdd_Prem_SA_FC = dblAdd_Prem_SA_LC

                    Select Case Trim(CType(objOLEDR("TBIL_ANN_POL_ADD_RATE_APPLY") & vbNullString, String))
                        Case "P"    'compute additional premium base on basic premium
                            If Val(dblAnnual_Basic_Prem_LC) <> 0 And Val(dblAdd_Prem_Rate) <> 0 And Val(dblAdd_Rate_Per) <> 0 Then
                                dblTmp_Amt = dblAnnual_Basic_Prem_LC * dblAdd_Prem_Rate / dblAdd_Rate_Per
                            End If
                            dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt

                        Case "S"    'compute additional premium base on sum assured
                            If Val(dblAdd_Prem_SA_LC) <> 0 And Val(dblAdd_Prem_Rate) <> 0 And Val(dblAdd_Rate_Per) <> 0 Then
                                dblTmp_Amt = dblAdd_Prem_SA_LC * dblAdd_Prem_Rate / dblAdd_Rate_Per
                            End If
                            dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt
                    End Select

            End Select

            'Response.Write("<br/>Rate Type: " & UCase(Trim(CType(objOLEDR("TBIL_ANN_POL_ADD_PREM_RT_AMT_CD") & vbNullString, String))))
            'Response.Write("<br/>Additional Amount: " & UCase(Trim(CType(objOLEDR("TBIL_ANN_POL_ADD_PREM_AMT") & vbNullString, String))))
            'Response.Write("<br/>Fixed Rate: " & UCase(Trim(CType(objOLEDR("TBIL_ANN_POL_ADD_RATE_APPLY") & vbNullString, String))))
            'Response.Write("<br/>Fixed Rate: " & UCase(Trim(CType(objOLEDR("TBIL_ANN_POL_ADD_PREM_FX_RT") & vbNullString, String))))
            'Response.Write("<br/>fixed Rate Per: " & UCase(Trim(CType(objOLEDR("TBIL_ANN_POL_ADD_PREM_FX_RT_PER") & vbNullString, String))))
            'Response.Write("<br/>Table Rate: " & UCase(Trim(CType(objOLEDR("TBIL_ANN_POL_ADD_RATE") & vbNullString, String))))
            'Response.Write("<br/>Table Rate Per: " & UCase(Trim(CType(objOLEDR("TBIL_ANN_POL_ADD_RATE_PER") & vbNullString, String))))
            'Response.Write("<br/>Sum Assured: " & UCase(Trim(CType(objOLEDR("TBIL_ANN_POL_ADD_SA_LC") & vbNullString, String))))

        Loop

        objOLECmd = Nothing
        objOLEDR = Nothing


        'Me.txtCalc_Add_Prem_LC.Text = dblTotal_Add_Prem_LC.ToString()
        dblTotal_Add_Prem_FC = dblTotal_Add_Prem_LC
        'Me.txtCalc_Add_Prem_FC.Text = dblTotal_Add_Prem_FC.ToString()


        '***********************************************************************************************
        ' END ADDITIONAL COVERS CODES 
        '***********************************************************************************************

    End Sub


    Private Sub Proc_DoDelete()

        If Trim(Me.txtFileNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtQuote_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Exit Sub
        End Try


        strTable = strTableName

        strREC_ID = Trim(Me.txtFileNum.Text)

        'Delete record
        'Me.textMessage.Text = "Deleting record... "
        strSQL = ""
        strSQL = "DELETE FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_ANN_POL_PRM_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_ANN_POL_PRM_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"

        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try
            objOLECmd2.Connection = objOLEConn
            objOLECmd2.CommandType = CommandType.Text
            objOLECmd2.CommandText = strSQL
            intC = objOLECmd2.ExecuteNonQuery()

            If intC >= 1 Then
                Call Proc_DoNew()
                Me.lblMsg.Text = "Record deleted successfully."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Else
                Me.lblMsg.Text = "Sorry!. Record not deleted..."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"

        End Try


        objOLECmd2.Dispose()
        objOLECmd2 = Nothing


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        'Me.txtNum.Enabled = True
        'Me.txtNum.Focus()

    End Sub

    Private Sub Proc_DoNew()

        'Call Proc_DDL_Get(Me.cboransList, RTrim("*"))

        'Scan through textboxes on page or form
        'Try
        'Catch ex As Exception

        'End Try

        Dim ctrl As Control
        For Each ctrl In Page.Controls
            If TypeOf ctrl Is HtmlForm Then
                Dim subctrl As Control
                For Each subctrl In ctrl.Controls
                    If TypeOf subctrl Is System.Web.UI.WebControls.TextBox Then
                        If subctrl.ID = "txtFileNum" Or _
                           subctrl.ID = "txtQuote_Num" Or _
                           subctrl.ID = "txtPolNum" Or _
                           subctrl.ID = "txtProductClass" Or _
                           subctrl.ID = "txtProduct_Num" Or _
                           subctrl.ID = "txtPlan_Num" Or _
                           subctrl.ID = "txtCover_Num" Or _
                           subctrl.ID = "txtDOB" Or _
                           subctrl.ID = "txtDOB_ANB" Or _
                           subctrl.ID = "xyz_123" Then
                        Else
                            'Response.Write("<br> Control ID: " & subctrl.ID)
                            'CType(subctrl, TextBox).Text = ""
                        End If
                    End If
                    If TypeOf subctrl Is System.Web.UI.WebControls.DropDownList Then
                        'CType(subctrl, DropDownList).SelectedIndex = -1
                    End If
                Next
            End If
        Next

        Me.chkFileNum.Enabled = True
        Me.chkFileNum.Checked = False
        Me.lblFileNum.Enabled = False
        Me.txtFileNum.Enabled = False
        Me.cmdFileNum.Enabled = False

        Me.cmdSave_ASP.Enabled = True
        Me.cmdNext.Enabled = False

    End Sub

    Private Function Proc_DoOpenRecord(ByVal FVstrGetType As String, ByVal FVstrRefNum As String, Optional ByVal FVstrRecNo As String = "", Optional ByVal strSearchByWhat As String = "FILE_NUM") As String

        strErrMsg = "false"

        lblMsg.Text = ""
        If Trim(FVstrRefNum) = "" Then
            Return strErrMsg
            Exit Function
        End If

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Return strErrMsg
            Exit Function
        End Try


        strREC_ID = Trim(FVstrRefNum)

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 PRM_TBL.*"
        strSQL = strSQL & " FROM " & strTable & " AS PRM_TBL"
        strSQL = strSQL & " WHERE PRM_TBL.TBIL_ANN_POL_PRM_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND PRM_TBL.TBIL_ANN_POL_PRM_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If

        strSQL = "SPANN_GET_POLICY_PREM_INFO"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        'objOLECmd.CommandType = CommandType.Text
        objOLECmd.CommandType = CommandType.StoredProcedure
        objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = strREC_ID
        objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strErrMsg = "true"
            '("TBIL_ANN_POL_PRM_RT_TAB_FIX") = Trim(Me.txtPrem_Rate_TypeNum.Text)
            Me.txtCover_Num.Text = RTrim(CType(objOLEDR("TBIL_ANN_POLY_COVER_CD") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboCover_Name, RTrim(Me.txtCover_Num.Text))

            Me.txtPlan_Num.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_PLAN_CD") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboPlan_Name, RTrim(Me.txtPlan_Num.Text))


            Me.txtProductClass.Text = RTrim(CType(objOLEDR("TBIL_ANN_PRDCT_DTL_CAT") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim(Me.txtProductClass.Text))

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_FILE_NO") & vbNullString, String))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_REC_ID") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_PROP_NO") & vbNullString, String))
            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_POLY_NO") & vbNullString, String))


            Me.txtPrem_Period_Yr.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_PERIOD_YRS") & vbNullString, String))

            If IsDate(objOLEDR("TBIL_ANN_POL_PRM_FROM")) Then
                Me.txtPrem_Start_Date.Text = Format(CType(objOLEDR("TBIL_ANN_POL_PRM_FROM"), DateTime), "dd/MM/yyyy")
            End If
            If IsDate(objOLEDR("TBIL_ANN_POL_PRM_TO")) Then
                Me.txtPrem_End_Date.Text = Format(CType(objOLEDR("TBIL_ANN_POL_PRM_TO"), DateTime), "dd/MM/yyyy")
            End If

            Me.txtPrem_MOP_Type.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_MODE_PAYT") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_MOP_Type, RTrim(Me.txtPrem_MOP_Type.Text))

            Me.txtPrem_SA_CurrencyCode.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_CURRCY") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_SA_Currency, RTrim(Me.txtPrem_SA_CurrencyCode.Text))
            Me.txtPrem_Exchange_Rate.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_EXCHG_RATE") & vbNullString, String))

            'Me.txtPrem_Life_CoverNum.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_LIFE_COVER") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboPrem_Life_Cover, RTrim(Me.txtPrem_Life_CoverNum.Text))

            Me.txtPrem_MOP_Rate.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_MOP_RATE") & vbNullString, String))
            Me.txtPrem_Exchange_Rate.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_EXCHG_RATE") & vbNullString, String))

            Me.txtPrem_Rate_TypeNum.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_RT_TAB_FIX") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Rate_Type, RTrim(Me.txtPrem_Rate_TypeNum.Text))

            Me.txtPrem_Fixed_Rate.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_RT_FIXED") & vbNullString, String))


            If Trim(Me.txtPrem_Rate_TypeNum.Text) = "F" Then
                Me.lblPrem_Fixed_Rate.Enabled = True
                Me.txtPrem_Fixed_Rate.Enabled = True
                Me.lblPrem_Fixed_Rate_Per.Enabled = True
                Me.cboPrem_Fixed_Rate_Per.Enabled = True
                Me.lblPrem_Rate_Code.Enabled = False
                Me.cboPrem_Rate_Code.Enabled = False
            ElseIf Trim(Me.txtPrem_Rate_TypeNum.Text) = "N" Then
                Me.lblPrem_Fixed_Rate.Enabled = False
                Me.txtPrem_Fixed_Rate.Enabled = False
                Me.lblPrem_Fixed_Rate_Per.Enabled = False
                Me.cboPrem_Fixed_Rate_Per.Enabled = False
                Me.lblPrem_Rate_Code.Enabled = False
                Me.cboPrem_Rate_Code.Enabled = False
            ElseIf Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" Then
                Me.lblPrem_Fixed_Rate.Enabled = False
                Me.txtPrem_Fixed_Rate.Enabled = False
                Me.lblPrem_Fixed_Rate_Per.Enabled = False
                Me.cboPrem_Fixed_Rate_Per.Enabled = False
                Me.lblPrem_Rate_Code.Enabled = True
                Me.cboPrem_Rate_Code.Enabled = True
            End If

            If IsNumeric(objOLEDR("TBIL_ANN_POL_PURCHASE_LC")) Then
                txtPurchaseAnnuityLC.Text = Format(CType(objOLEDR("TBIL_ANN_POL_PURCHASE_LC"), Decimal), "N2")
                txtPurchaseAnnuityFC.Text = Format(CType(objOLEDR("TBIL_ANN_POL_PURCHASE_FC"), Decimal), "N2")
            End If

            'If IsNumeric(objOLEDR("TBIL_ANN_POL_PURCHASE_LC")) Then
            '    txtAnnualAnnuityLC.Text = Format(CType(objOLEDR("TBIL_ANN_POL_PURCHASE_LC"), Decimal), "N2")
            '    txtAnnualAnnuityFC.Text = Format(CType(objOLEDR("TBIL_ANN_POL_PURCHASE_FC"), Decimal), "N2")
            'End If
            If Not IsNumeric(CType(objOLEDR("TBIL_ANN_POL_PREM_WITH_LS_LC") & vbNullString, String)) Then

            Else
                txtPremiumWithLumpSumLC.Text = Format(CType(objOLEDR("TBIL_ANN_POL_PREM_WITH_LS_LC"), Decimal), "N2")
                txtPremiumWithLumpSumFC.Text = Format(CType(objOLEDR("TBIL_ANN_POL_PREM_WITH_LS_FC"), Decimal), "N2")
            End If

            If Not IsNumeric(CType(objOLEDR("TBIL_ANN_POL_PREM_WITHOUT_LS_LC") & vbNullString, String)) Then
            Else

                txtPremiumWithoutLumpSumLC.Text = Format(CType(objOLEDR("TBIL_ANN_POL_PREM_WITHOUT_LS_LC"), Decimal), "N2")
                txtPremiumWithoutLumpSumFC.Text = Format(CType(objOLEDR("TBIL_ANN_POL_PREM_WITHOUT_LS_FC"), Decimal), "N2")
            End If
            If Not IsNumeric(CType(objOLEDR("TBIL_ANN_ANNUAL_LC") & vbNullString, String)) Then
            Else

                txtAnnualAnnuityLC.Text = Format(CType(objOLEDR("TBIL_ANN_ANNUAL_LC"), Decimal), "N2")
                txtAnnualAnnuityFC.Text = Format(CType(objOLEDR("TBIL_ANN_ANNUAL_FC"), Decimal), "N2")
            End If
            If Not IsNumeric(CType(objOLEDR("TBIL_ANN_MONTHLY_LC") & vbNullString, String)) Then
            Else

                txtMonthlyAnnuityLC.Text = Format(CType(objOLEDR("TBIL_ANN_MONTHLY_LC"), Decimal), "N2")
                txtMonthlyAnnuityFC.Text = Format(CType(objOLEDR("TBIL_ANN_MONTHLY_FC"), Decimal), "N2")
            End If

            Me.txtPrem_Fixed_Rate_PerNum.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_RT_FIX_PER") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Fixed_Rate_Per, RTrim(Me.txtPrem_Fixed_Rate_PerNum.Text))
            txtProductClass.Text = RTrim(CType(objOLEDR("TBIL_ANN_PRDCT_DTL_CAT") & vbNullString, String))
            txtProduct_Num.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_PRDCT_CD") & vbNullString, String))

            Me.txtPrem_Rate_Code.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_RATE_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Rate_Code, RTrim(Me.txtPrem_Rate_Code.Text))
            Me.txtPrem_Rate.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_RATE") & vbNullString, String))
            Me.txtPrem_Rate_Per.Text = RTrim(CType(objOLEDR("TBIL_ANN_POL_PRM_RATE_PER") & vbNullString, String))


            Me.lblFileNum.Enabled = False
            'Call DisableBox(Me.txtFileNum)
            Me.chkFileNum.Enabled = False
            Me.txtFileNum.Enabled = False
            Me.txtQuote_Num.Enabled = False
            Me.txtPolNum.Enabled = False

            Me.cmdNew_ASP.Enabled = True
            Me.cmdDelete_ASP.Enabled = True
            Me.cmdNext.Enabled = True


            strOPT = "2"
            Me.lblMsg.Text = "Status: Data Modification"

        Else
            Me.cmdDelete_ASP.Enabled = False
            Me.cmdNext.Enabled = False

            strOPT = "1"
            Me.lblMsg.Text = "Status: New Entry..."

        End If


        ' dispose of open objects
        objOLECmd.Dispose()
        objOLECmd = Nothing

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If
        objOLEDR = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        Return strErrMsg

    End Function

    Protected Sub cmdPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        Dim pvURL As String = "PRG_ANNTY_POLY_PERSNAL.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)

    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Dim pvURL As String = ""
        pvURL = "prg_annuity_poly_benefry.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        Select Case Trim(Me.txtProduct_Num.Text)
            Case "F001", "F002"
                pvURL = "prg_annuity_poly_funeral.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        End Select
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)

    End Sub


    Protected Sub txtPurchaseAnnuityLC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPurchaseAnnuityLC.TextChanged
        If txtPurchaseAnnuityLC.Text <> "" Then
            Dim pAnn As String = Format(CType(txtPurchaseAnnuityLC.Text, Decimal), "N2")
            'Format(CType(annualAnnuity, Decimal), "N2")
            txtPurchaseAnnuityLC.Text = CType(pAnn, String)
            txtPurchaseAnnuityFC.Text = CType(pAnn, String)
            txtPremiumWithoutLumpSumLC.Text = CType(pAnn, String)
            txtPremiumWithoutLumpSumFC.Text = CType(pAnn, String)

            Proc_DoCalc_Annuity()
        End If
    End Sub

    Protected Sub txtPrem_Fixed_Rate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrem_Fixed_Rate.TextChanged
        If txtPurchaseAnnuityLC.Text <> "" And txtPrem_Fixed_Rate.Text <> "" And cboPrem_Fixed_Rate_Per.SelectedIndex <> 0 Then
            Dim pAnn As Decimal = CType(Format(CType(txtPurchaseAnnuityLC.Text, Decimal), "N2"), Decimal)
            'Format(CType(annualAnnuity, Decimal), "N2")
            txtPurchaseAnnuityLC.Text = CType(pAnn, String)
            txtPurchaseAnnuityFC.Text = CType(pAnn, String)
            txtPremiumWithoutLumpSumLC.Text = CType(pAnn, String)
            txtPremiumWithoutLumpSumFC.Text = CType(pAnn, String)

            Proc_DoCalc_Annuity()
        End If
    End Sub


    Protected Sub cboPrem_Rate_Code_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPrem_Rate_Code.SelectedIndexChanged

    End Sub

    Protected Sub txtPremiumWithLumpSumLC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPremiumWithLumpSumLC.TextChanged
        If txtPremiumWithLumpSumLC.Text <> String.Empty And IsNumeric(txtPremiumWithLumpSumLC.Text) Then
            txtPremiumWithLumpSumFC.Text = txtPremiumWithLumpSumLC.Text
        End If
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

    End Sub
    Public Sub LoadModeofPayment(ByVal ProdCode As String)
        'Populate box with codes
        Dim strTable As String = ""
        Dim strTableName As String = ""
        Dim strSQL As String = ""

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Exit Sub
        End Try

        Dim li As ListItem
        li = New ListItem
        li.Text = "SELECT"
        li.Value = "*"
        cboPrem_MOP_Type.Items.Add(li)
        cboPrem_MOP_Type.SelectedIndex = 0

        strTable = strTableName
        strTable = RTrim("TBIL_MOP_FACTOR")
        strSQL = ""
        strSQL = strSQL & "SELECT TBIL_MOP_TYPE_CD AS MyFld_Value, TBIL_MOP_TYPE_DESC AS MyFld_Text"
        strSQL = strSQL & " FROM " & strTable
        If ProdCode = "A001" Then
            strSQL = strSQL & " WHERE TBIL_MOP_TYPE_CD IN('M','Q')"
        Else
            strSQL = strSQL & " WHERE TBIL_MOP_TYPE_CD IN('M','Q','H','Y')"
        End If

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        objOLECmd.CommandType = CommandType.Text
        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        While (objOLEDR.Read())
            li = New ListItem
            li.Text = objOLEDR("MyFld_Text")
            li.Value = Trim(objOLEDR("MyFld_Value") & vbNullString)
            cboPrem_MOP_Type.Items.Add(li)

            'cboPrem_MOP_Type.DataTextField = objOLEDR("MyFld_Text")
            'cboPrem_MOP_Type.DataTextField = objOLEDR("MyFld_Value")
        End While

        ' dispose of open objects
        objOLECmd.Dispose()
        objOLECmd = Nothing

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If
        objOLEDR = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing
    End Sub
End Class
