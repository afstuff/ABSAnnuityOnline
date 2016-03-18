
Partial Class MENU_AN
    Inherits System.Web.UI.Page
    Protected STRMENU_TITLE As String
    Protected BufferStr As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            'Me.LNK_CODE.Text = "Master Setup"
            Me.LNK_QUOTE.Text = "Proposal"
            'Me.LNK_UND.Text = "Underwriting"
            'Me.LNK_ENDORSE.Text = "Endorsement"
            'Me.LNK_PROCESS.Text = "Processing"
            'Me.LNK_CLP.Text = "Claims"
            'Me.LNK_REINS.Text = "Reinsurance"
        End If

        If Me.txtAction.Text = "Log_Out" Then
            Call DoProc_LogOut()
            Me.txtAction.Text = ""
            'Response.Redirect("LoginP.aspx")
            'Response.Redirect(Request.ApplicationPath & "~/Default.aspx")
            Response.Redirect(Request.ApplicationPath & "~/m_menu.aspx?menu=home")
            Exit Sub
        End If


        'Put user code to initialize the page here
        Dim mKey As String
        Try
            mKey = Page.Request.QueryString("menu").ToString
            'mkey options = MKT UND CLM REIN CRCO ACC ADMIN
        Catch
            mKey = "HOME"
        End Try

        STRMENU_TITLE = "+++ Home Page +++ "
        BufferStr = ""

        'Me.LNK_CODE.Enabled = True
        'Me.LNK_QUOTE.Enabled = True
        'Me.LNK_UND.Enabled = True
        'Me.LNK_ENDORSE.Enabled = True
        'Me.LNK_PROCESS.Enabled = True
        'Me.LNK_CLP.Enabled = True
        'Me.LNK_REINS.Enabled = True

        'Me.LNK_CODE.BackColor = Drawing.Color.White
        'Me.LNK_QUOTE.BackColor = Drawing.Color.White
        'Me.LNK_UND.BackColor = Drawing.Color.White
        'Me.LNK_ENDORSE.BackColor = Drawing.Color.White
        'Me.LNK_PROCESS.BackColor = Drawing.Color.White
        'Me.LNK_CLP.BackColor = Drawing.Color.White
        'Me.LNK_REINS.BackColor = Drawing.Color.White

        Call DoProc_Menu(mKey)
    End Sub
    Private Sub DoProc_Menu(ByVal pvMenu As String)
        Select Case pvMenu.ToUpper
            Case "HOME"
                STRMENU_TITLE = "+++ Annuity Application Module +++ "
                'AddMenuItem("", "Welcome to ABS Web Accounts Manager", "MainM.aspx?menu=HOME")
                BufferStr += "<tr>"
                BufferStr += "<td align='center' valign='top'>"
                BufferStr += "&nbsp;<img alt='Image' src='Images/Family.jpg' style='width: 850px; height: 450px' />&nbsp;"
                BufferStr += "</td>"
                BufferStr += "</tr>"

            Case "GEN"
                STRMENU_TITLE = "+++ Parameters Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Company Data Setup", "javascript:jsDoPopNew_Full('General/GEN110.aspx?TTYPE=COY')")
                AddMenuItem("", "Server Parameters Setup", "menu_an.aspx?menu=GEN")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")

            Case "AN_CODE"
                'Me.LNK_CODE.BackColor = Drawing.Color.Teal
                'Me.LNK_CODE.Font.Bold = True
                STRMENU_TITLE = "+++ Master Setup Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("Master Setup", "Codes File Setup", "menu_an.aspx?menu=il_code_std")
                AddMenuItem("", "Customer Masters", "menu_an.aspx?menu=il_code_cust")
                'AddMenuItem("", "Master Codes Setup", "menu_an.aspx?menu=il_code_mst")
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Rate Masters", "menu_an.aspx?menu=il_code_rate")
                'AddMenuItem("", "Product Master", "menu_an.aspx?menu=il_code_prod")
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Underwriting Codes Masters", "menu_an.aspx?menu=il_code_und")
                'AddMenuItem("", "Reinsurance Codes Masters", "menu_an.aspx?menu=il_code_reins")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")
            Case "AN_CODE_STD"
                STRMENU_TITLE = "+++ Master Setup >>> Codes File Setup Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=il_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("Codes File Setup", "Nationality Codes Setup", "an_1010.aspx?optid=1000&optd=Country")
                AddMenuItem("", "State Codes Setup", "an_1010.aspx?optid=1002&optd=State")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Branch Codes Setup", "an_1010.aspx?optid=1004&optd=Branch")
                AddMenuItem("", "Division Codes Setup", "an_1010.aspx?optid=1006&optd=Division")
                AddMenuItem("", "Department Codes Setup", "an_1010.aspx?optid=1008&optd=Department")
                AddMenuItem("", "Location Codes Setup", "an_1010.aspx?optid=1010&optd=Location")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Occupation Class Setup", "an_1010.aspx?optid=1012&optd=Occp_Class")
                AddMenuItem("", "Occupation Codes Setup", "an_1010.aspx?optid=1014&optd=Occupation")
                AddMenuItem("", "Religion/Belief Codes Setup", "an_1010.aspx?optid=1016&optd=Religion")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Gender Codes Setup", "an_1010.aspx?optid=1018&optd=Gender")
                AddMenuItem("", "Customer Title Codes Setup", " an_1010.aspx?optid=1020&optd=Customer_Title")
                AddMenuItem("", "Rider Codes Setup", "an_1010.aspx?optid=1022&optd=Rider")
                AddMenuItem("", "Relation Codes Setup", "an_1010.aspx?optid=1024&optd=Relation")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=il_code")
            Case "AN_CODE_CUST"
                STRMENU_TITLE = "+++ Master Setup >>> Customer Masters Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=il_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("Customer Masters", "Customer Class Setup", "an_1110.aspx?optd=Cust_Category")
                AddMenuItem("", "Customer Details (Assured Details)", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Agents/Brokers Category Setup", "")
                AddMenuItem("", "Agency/Brokers Details", "")
                AddMenuItem("", "Marketers Codes Setup (Agencies)", "")
                AddMenuItem("", "Contractor Details", "")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=il_code")
            Case "AN_CODE_MST"
            Case "AN_CODE_RATE"
                STRMENU_TITLE = "+++ Master Setup >>> Rate Master Codes Setup Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=il_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("Rates", "Rate Type/Category Setup", "an_1210.aspx")
                AddMenuItem("", "Rate Master Setup", "")
                AddMenuItem("", "Allocation to Investment Rate Setup", "")
                AddMenuItem("", "Agencies Commission Rate Setup", "")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=il_code")
            Case "AN_CODE_PROD"
                STRMENU_TITLE = "+++ Master Setup >>> Product Master Codes Setup Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=il_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("Products", "Product Category/Class Setup", "an_1310.aspx")
                AddMenuItem("", "Product Details", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Cover Master", "")
                AddMenuItem("", "Plan Master", "")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=il_code")

            Case "AN_CODE_UND"
                STRMENU_TITLE = "+++ Master Setup >>> Underwriting Codes Masters Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=il_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("Underwriting Codes", "Disability Type Setup", "an_1410.aspx?optid=1400&optd=Disability")
                AddMenuItem("", "Health Status Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Medical Illness Codes Setup", "")
                AddMenuItem("", "Medical Examination Codes Setup", "")
                AddMenuItem("", "Medical Clinic Details Setup", "")
                AddMenuItem("", "Mortality Codes Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Loading Codes Setup", "")
                AddMenuItem("", "Discount Codes Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Policy Condition Codes Setup", "")
                AddMenuItem("", "Loss Types Codes Setup", "")
                AddMenuItem("", "Source of Business Codes Setup", "")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=il-code")

            Case "AN_CODE_REINS"
                STRMENU_TITLE = "+++ Master Setup >>> Reinsurance Codes Masters Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=il_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                'AddMenuItem("", "MENU CAPTION", "PAGE URL")
                AddMenuItem("Reinsurance Codes", "Treaty Compamy Category (Local, Overseas)", "an_1510.aspx")
                AddMenuItem("", "Treaty Compamy Setup", "")
                AddMenuItem("", "Treaty Proportion Setup (Yearly Treaty Arrangement", "")
                'AddMenuItem("", "Treaty Commission Setup", "")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=il-code")

            Case "AN_QUOTE"
                STRMENU_TITLE = "+++ Proposal Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                'AddMenuItem("Proposal", "New Proposal", "an_2010.aspx")
                AddMenuItem("New Proposal", "Retiree Life Annuity", "")
                AddMenuItem("", "Dependant Annuity", "")
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Convert Proposal to Policy", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Reports", "Proposal Status Report", "")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")


            Case "AN_UND"
                'Me.LNK_UND.BackColor = Drawing.Color.Teal
                'Me.LNK_UND.Font.Bold = True
                STRMENU_TITLE = "+++ Underwriting Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")
                AddMenuItem("New Proposal", "Retiree Life Annuity", "")
                AddMenuItem("", "Dependant Annuity", "")
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Convert Proposal to Policy", "an_3010.aspx")
                'AddMenuItem("", "New Policy", "an_3020.aspx")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Reports", "Policy Schedule", "")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")


            Case "AN_ENDORSE"
                'Me.LNK_ENDORSE.BackColor = Drawing.Color.Teal
                'Me.LNK_ENDORSE.Font.Bold = True
                STRMENU_TITLE = "+++ Endorsement Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("Operations", "Additional Beneficiary Annuity", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")
            Case "AN_PROCESS"
                'Me.LNK_PROCESS.BackColor = Drawing.Color.Teal
                'Me.LNK_PROCESS.Font.Bold = True
                STRMENU_TITLE = "+++ Processing Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("Operations", "Periodic Annuity Processing", "")
                AddMenuItem("", "Guarantee Benefit on Death", "")
                AddMenuItem("", "Policy Revival Processing", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")

            Case "AN_CLAIM"
                'Me.LNK_CLP.BackColor = Drawing.Color.Teal
                'Me.LNK_CLP.Font.Bold = True
                STRMENU_TITLE = "+++ Claims Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                'AddMenuItem("", "MENU CAPTION", "PAGE URL")
                'AddMenuItem("Transactions", "Transaction Entry", "")
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("Reports", "Report Line 1", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")

            Case "AN_REINS"
                'Me.LNK_REINS.BackColor = Drawing.Color.Teal
                'Me.LNK_REINS.Font.Bold = True
                STRMENU_TITLE = "+++ Reinsurance Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                'AddMenuItem("", "MENU CAPTION", "PAGE URL")
                'AddMenuItem("Transactions", "Transaction Entry", "")
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("Reports", "Report Line 1", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")


            Case "AL_SEC"
                STRMENU_TITLE = "+++ Administration Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Administrator Password Change", "")
                AddMenuItem("", "Create New User", "")
                AddMenuItem("", "User Password Change", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_an.aspx?menu=home")

        End Select

    End Sub

    Private Sub AddMenuItem(ByVal LeadItem As String, ByVal MenuItemText As String, ByVal LinkUrl As String)
        Dim myURL As String
        myURL = LinkUrl
        If Trim(myURL) = "" Then
            myURL = "'#'"
        Else
            myURL = "'" & myURL & "'"
        End If

        BufferStr += "<tr>"
        If LeadItem.Trim() = "" Then
            BufferStr += "<td nowrap align='left' valign='top'>&nbsp;&nbsp;</td>"
        Else
            BufferStr += "<td nowrap align='left' valign='top' class='a_sub_menu'>&nbsp;"
            BufferStr += "<img alt='Menu Image' src='Images/ballred.gif' class='MY_IMG_LINK' />&nbsp;"
            BufferStr += LeadItem & "&nbsp;</td>"
        End If

        If MenuItemText.Trim = "" Then
            BufferStr += "<td nowrap align='left' valign='top'>&nbsp;</td>"
        ElseIf MenuItemText.Trim = "UNDER_LINE" Then
            BufferStr += "<td nowrap align='left' valign='top' class='td_under_line'>&nbsp;</td>"
        ElseIf MenuItemText.Trim = "Returns to Previous Page" Then
            BufferStr += "<td nowrap align='left' valign='top' class='td_return_menu'>"
            BufferStr += "<a href=" & myURL & " valign='top' class='a_return_menu'>" & MenuItemText & "</a>"
            BufferStr += "</td>"
        Else
            BufferStr += "<td nowrap align='left' valign='top' class='td_sub_menu2'>"
            BufferStr += "<img alt='Menu Image' src='Images/ballredx.gif' class='MY_IMG_LINK' />&nbsp;"
            BufferStr += "<a href=" & myURL & " valign='top' class='a_sub_menu2'>" & MenuItemText & "</a>"
            BufferStr += "</td>"
        End If
        BufferStr += "</tr>"
    End Sub

    Protected Sub DoProc_LogOut()

        Dim strSess As String = ""
        Dim intC As Integer = 0
        Dim intCX As Integer = 0
        Dim MyArray(15) As String

        intC = 0
        intCX = 0
        Try
            'Session("STFID") = RTrim(Me.txtNum.Text)
            'Session("STFID") = RTrim("")

            'Session.Keys
            'Session.Count
            'LOOP THROUGH THE SESSION OBJECT
            '*******************************

            'For intC = 0 To Session.Count - 1
            'Response.Write("<br />" & "Item " & intC & " - Key: " & Session.Keys(intC).ToString & " - Value: : " & Session.Item(intC).ToString)
            '
            'Next

            'SAMPLE SESSION DATA
            '*******************
            ''Item 0 - Key: ActiveSess - Value: : 1
            ''Item 1 - Key: StartdDate - Value: : 06/14/2013 7:01:55 PM
            ''Item 2 - Key: connstr - Value: : Provider=SQLOLEDB;Data Source=ABS-PC;Initial Catalog=ABS;User ID=SA;Password=;
            ''Item 3 - Key: connstr_SQL - Value: : Data Source=ABS-PC;Initial Catalog=ABS;User ID=SA;Password=;
            ''Item 4 - Key: CL_COMP_NAME - Value: : CUSTODIAN AND ALLIED INSURANCE PLC
            ''Item(5 - Key) : MyUserIDX(-Value) : ADM()
            ''Item(6 - Key) : MyUserName(-Value) : System(Administrator)
            ''Item 7 - Key: MyUserLevelX - Value: : 0
            ''Item(8 - Key) : MyUserIDX_NIM(-Value) : ADM()
            ''Item(9 - Key) : MyUserName_NIM(-Value) : System(Administrator)
            ''Item 10 - Key: MyUserLevelX_NIM - Value: : 0
            ''Item 11 - Key: MyLogonTime - Value: : 06/14/2013 7:02:05 PM
            ''Item(12 - Key) : MyUserID(-Value) : ADM()


            'For Each strS As String In Session.Keys
            '    '    ' ...
            '    'Response.Write("<br /> Session ID: " & Session.SessionID & " - Key: " & strSess.ToString & " - Value: " & Session.Item(strSess).ToString)

            '    '    If UCase(strSess) = UCase("connstr") Or _
            '    '      UCase(strSess) = UCase("connstr_SQL") Or _
            '    '      UCase(strSess) = UCase("CL_COMP_NAME") Then
            '    '    Else
            '    '        'Session.Remove(strSess)
            '    '    End If
            '    strSess = strS
            '    Response.Write("<br />" & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
            'Next

            For intCX = 0 To Session.Count - 1

                strSess = Session.Keys(intCX).ToString

                If UCase(strSess) = UCase("connstr") Or _
                  UCase(strSess) = UCase("connstr_SQL") Or _
                  UCase(strSess) = UCase("CL_COMP_NAME") Or _
                  UCase(strSess) = UCase("ActiveSess") Or _
                  UCase(strSess) = UCase("StartdDate") Then
                Else
                    intC = intC + 1
                    MyArray(intC) = strSess
                    'Response.Write("<br />" & "Item " & intC & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)

                End If

            Next

            'Response.Write("<br />" & "Item ubound(): " & UBound(MyArray).ToString)
            'Response.Write("<br />" & "Item Length: " & MyArray.Length)

            For intCX = 1 To intC

                strSess = MyArray(intCX).ToString

                Response.Write("<br />" & "Removing session Item " & intCX & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
                Session.Remove(strSess.ToString)
                'Session.Contents.Remove(strSess)

            Next

            'Session.Clear()

        Catch ex As Exception
            Response.Write("<br /> Error has Occured at key: " & strSess.ToString & " Reason: " & ex.Message.ToString)
            'Exit Try
        End Try


    End Sub

    Protected Sub LNK_CODE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LNK_CODE.Click
        Response.Redirect("MENU_AN.aspx?menu=AN_CODE")
    End Sub

    Protected Sub LNK_QUOTE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LNK_QUOTE.Click
        Response.Redirect("MENU_AN.aspx?menu=AN_QUOTE")
    End Sub

    
End Class
