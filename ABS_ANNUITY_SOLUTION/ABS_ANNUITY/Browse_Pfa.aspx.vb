Imports System.Data
Imports System.Data.OleDb
Partial Class Browse_Pfa
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Protected PageLinks As String
    Protected STRPAGE_TITLE As String

    Dim strQRY_TYPE As String
    Dim strFRM_NAME As String
    Dim strCTR_VAL As String
    Dim strCTR_TXT As String

    Dim strR_ID As String
    Dim strFT As String

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strQRY_TYPE = CType(Request.QueryString("QRY_TYPE"), String)
        Catch ex As Exception
            strQRY_TYPE = ""
        End Try

        Try
            strFRM_NAME = CType(Request.QueryString("FRM_NAME"), String)
        Catch ex As Exception
            strFRM_NAME = "Form1"
        End Try
        Try
            strCTR_VAL = CType(Request.QueryString("CTR_VAL"), String)
        Catch ex As Exception
            strCTR_VAL = "Field_Value"
        End Try
        Try
            strCTR_TXT = CType(Request.QueryString("CTR_TXT"), String)
        Catch ex As Exception
            strCTR_TXT = "Field_Text"
        End Try

        Select Case UCase(Trim(strQRY_TYPE))
            Case "PFA"
                strR_ID = "001"
                strTableName = "TBIL_PFA_DETAIL"
            Case "MKT"
                strR_ID = "001"
                strTableName = "TBIL_AGENCY_CD"
            Case "BRK"
                strR_ID = "001"
                strTableName = "TBIL_CUST_DETAIL"
            Case "INS"
                strR_ID = "001"
                strTableName = "TBIL_INS_DETAIL"
            Case Else
                strR_ID = "XXX"
                strTableName = ""

        End Select
        'Response.Write("<br/>Form Name: " & strFRM_NAME)
        'Response.Write("<br/>Field Value Name: " & strCTR_VAL)
        'Response.Write("<br/>Field Text Name: " & strCTR_TXT)

        Me.hidFRM_NAME.Value = strFRM_NAME
        Me.hidCTR_VAL.Value = strCTR_VAL
        Me.hidCTR_TXT.Value = strCTR_TXT

        If Not (Page.IsPostBack) Then
            strFT = "Y"
            Me.cmdOK.Disabled = True
            'Call Proc_DataBind(strR_ID)
            Me.txtAction.Text = ""
            Call Proc_DataBind()
        Else
            strFT = "N"
        End If
    End Sub
    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True

        Select Case UCase(Trim(strQRY_TYPE))
            Case "PFA"
                STRPAGE_TITLE = "List of Agents Codes..."
                strTableName = "TBIL_PFA_DETAIL"
                strTable = strTableName

                strSQL = "SELECT * FROM " & strTableName & ""

            Case Else
                strSQL = ""
                STRPAGE_TITLE = "List of ..."
                Me.lblMessage.Text = "Missing or Invalid Parameter: " & strQRY_TYPE
                ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Msg('" & Me.lblMessage.Text & "');", True)
                Exit Sub

        End Select


        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        'open connection to database
        objOLEConn.Open()

        'Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        'objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID
        'Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)
        'objDA.SelectCommand = objOLECmd

        Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)

        Dim objDS As DataSet = New DataSet()
        objDA.Fill(objDS, strTable)

        'Dim objDV As New DataView
        'objDV = objDS.Tables(strTable).DefaultView
        'objDV.Sort = "ACT_REC_NO"
        'Session("myobjDV") = objDV

        'With Me.DataGrid1
        '.DataSource = objDS
        '.DataBind()
        'End With

        With GridView1
            .DataSource = objDS
            .DataBind()
        End With

        'With Me.Repeater1
        '.DataSource = objDS
        '.DataBind()
        'End With

        'objDV.Dispose()
        'objDV = Nothing
        objDS = Nothing
        objDA = Nothing
        'objOLECmd.Dispose()
        'objOLECmd = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        Me.cmdOK.Disabled = True

        Dim P As Integer = 0
        Dim C As Integer = 0

        C = 0
        For P = 0 To Me.GridView1.Rows.Count - 1
            C = C + 1
        Next
        If C >= 1 Then
            'Me.cmdOK.Disabled = False
        End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Me.txtCustID.Text = Now.ToString
        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = GridView1.SelectedRow

        ' Display the required value from the selected row.
        Me.txtRecNo.Text = row.Cells(1).Text
        Me.txtCustID.Text = row.Cells(1).Text
        Me.txtCustName.Text = row.Cells(2).Text

        Me.hidRecNo.Value = Me.txtRecNo.Text
        Me.hidCustID.Value = Me.txtCustID.Text
        Me.hidCustName.Value = Me.txtCustName.Text

        Me.cmdOK.Disabled = False
    End Sub
End Class
