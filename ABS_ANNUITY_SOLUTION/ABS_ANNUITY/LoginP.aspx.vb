Imports System.Web.Security
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class LoginP
    Inherits System.Web.UI.Page
    Protected strCopyRight As String
    Protected dteMydate As String = CType(Format(Now, "dd-MMM-yyyy"), String)
    Dim strSQL As String
    Protected Structure TabItem
        Dim TabText As String
        Dim TabKey As String
    End Structure

    Protected MenuItems As New ArrayList()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cstype As Type = Me.GetType()
        strCopyRight = "Copyright &copy;" & Year(Now)

        If Not (Page.IsPostBack) Then
            Me.txtUserID.Enabled = True
            Me.txtUserID.Focus()
        End If
    End Sub

    Protected Sub LoginBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginBtn.Click
        lblMessage.Text = ""
        Dim mystrCONN_Chk As String = ""

        Dim objOLEConn_Chk As OleDbConnection = Nothing
        Dim objOLECmd_Chk As OleDbCommand = Nothing
        Dim objOLEDR_Chk As OleDbDataReader

        Dim myTmp_Chk As String
        Dim myTmp_Ref As String
        myTmp_Chk = "N"
        myTmp_Ref = ""


        mystrCONN_Chk = CType(Session("connstr"), String)
        objOLEConn_Chk = New OleDbConnection()
        objOLEConn_Chk.ConnectionString = mystrCONN_Chk

        Try
            'open connection to database
            objOLEConn_Chk.Open()
        Catch ex As Exception
            lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn_Chk = Nothing
            Exit Sub
        End Try

        Try
            Dim User_Login = Trim(txtUserID.Text)
            Dim User_Password = Trim(EncryptNew(txtUser_PWD.Text))
            'strSQL = "SELECT * FROM SEC_USER_LIFE_DETAIL WHERE SEC_USER_LOGIN=@pUser_Login " & _
            '         "and SEC_USER_PASSWORD=@pUser_Password"
            strSQL = "SELECT * FROM SEC_USER_LIFE_DETAIL WHERE SEC_USER_LOGIN='" & User_Login & "' " & _
                   "and SEC_USER_PASSWORD='" & User_Password & "' "
            objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
            objOLECmd_Chk.CommandType = CommandType.Text

            'objOLECmd_Chk.Parameters.AddWithValue("pUser_Login", Trim(txtUserID.Text))
            objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
            If (objOLEDR_Chk.Read()) Then
                Session("MyUserIDX") = Trim(Me.txtUserID.Text)
                'Session("MyUserName") = UCase(Me.txtUserName.Text)
                Session("MyUserName") = objOLEDR_Chk("SEC_USER_NAME")
                Session("MyUserRole") = objOLEDR_Chk("SEC_USER_ROLE")
                If Request.QueryString("Goto") <> "" Then
                    Response.Redirect(Request.QueryString("Goto"))
                Else
                    Response.Redirect("MENU_AN.aspx?menu=HOME")
                End If
            Else
                Me.lblMessage.Text = "Login information is not correct. Enter Valid User ID and Password..."
                Me.txtUserID.Enabled = True
                Me.txtUserID.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            Me.lblMessage.Text = "Error has occured. Reason: " & ex.Message.ToString()
        End Try
        objOLEDR_Chk = Nothing
        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing
        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing
    End Sub

    Protected Sub txtUserID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.TextChanged
        lblMessage.Text = ""
        Dim mystrCONN_Chk As String = ""

        Dim objOLEConn_Chk As OleDbConnection = Nothing
        Dim objOLECmd_Chk As OleDbCommand = Nothing
        Dim objOLEDR_Chk As OleDbDataReader

        Dim myTmp_Chk As String
        Dim myTmp_Ref As String
        myTmp_Chk = "N"
        myTmp_Ref = ""


        mystrCONN_Chk = CType(Session("connstr"), String)
        objOLEConn_Chk = New OleDbConnection()
        objOLEConn_Chk.ConnectionString = mystrCONN_Chk

        Try
            'open connection to database
            objOLEConn_Chk.Open()
        Catch ex As Exception
            lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn_Chk = Nothing
            Exit Sub
        End Try

        Try
            Dim User_Login = Trim(txtUserID.Text)
            strSQL = "SELECT * FROM SEC_USER_LIFE_DETAIL WHERE SEC_USER_LOGIN='" & User_Login & "'"
            objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
            objOLECmd_Chk.CommandType = CommandType.Text
            objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
            If (objOLEDR_Chk.Read()) Then
                Session("MyUserIDX") = Trim(Me.txtUserID.Text)
                txtUserName.Text = objOLEDR_Chk("SEC_USER_NAME")
            Else
                Me.lblMessage.Text = "User ID does not exist"
                txtUserName.Text = ""
                Me.txtUserID.Enabled = True
                Me.txtUserID.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            Me.lblMessage.Text = "Error has occured. Reason: " & ex.Message.ToString()
        End Try
        objOLEDR_Chk = Nothing
        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing
        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing
    End Sub
End Class
