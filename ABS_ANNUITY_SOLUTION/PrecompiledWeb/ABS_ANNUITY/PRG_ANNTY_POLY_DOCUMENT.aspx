<%@ page language="VB" autoeventwireup="false" inherits="PRG_ANNTY_POLY_DOCUMENT, App_Web_qkk2jm9h" %>
<%@ Register src="UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Annuity Document Print</title>
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js"></script>
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
        RadioButtonList label
        {
        	display: inline;
        	}
        .style2
        {
            width: 223px;
        }
        .style3
        {
            height: 30px;
            width: 223px;
        }
    </style>
</head>

<body onload="<%= FirstMsg %>">
    <form id="Form1" runat="server">
    
    <!-- start banner -->
    <div id="div_banner" align="center">    
        <uc1:UC_BANT ID="UC_BANT1" runat="server" />    
    </div>

    <div id="div_content" align="center">
        <table id="tbl_content" align="center">
        <tr>
            <td align="center" valign="top" class="td_menu_new">
	            <table align="center" border="0" cellspacing="0" class="tbl_menu_new">
	                <tr>
                        <td align="right" colspan="2" valign="top">    
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="150px" runat="server"></asp:DropDownList>
                        </td>	                
	                </tr>

                    <tr>
                        <td align="left" colspan="2" valign="top" class="myMenu_Title"><%=STRMENU_TITLE%>ANNUITY 
                            DOCUMENT PRINT</td>
                    </tr>
                    
                    <tr>
                        <td align="left" colspan="2" valign="top">
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp;<asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
                                        &nbsp;
                                    </td>
                                    <td align="right" colspan="1" valign="top">&nbsp;
                                         <a href="PRG_ANNTY_PROP_POLICY.aspx?menu=AN_QUOTE" class="a_sub_menu">Return to Menu</a>&nbsp;
                                    </td>                           
                                    
                                    <td align="right" colspan="1" valign="top" style="display:none;">    
                                        &nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox></td>
                                </tr>
                            </table>                            
                        </td>                           
                    </tr>

                    <tr>
                        <td align="left" colspan="2" valign="top">&nbsp;
                            <asp:Label ID="lblMsg" Text="Status..." ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" colspan="1" class="style2">
                            <asp:Label ID="lblPro_Pol_Num" Text="Proposal Number:" runat="server"></asp:Label>
           
                            &nbsp;
                            </td>
                        <td align="left" colspan="1">&nbsp;                     
                            <asp:TextBox ID="txtPro_Pol_Num" Width="250px" runat="server"></asp:TextBox>
                            &nbsp;<asp:Button ID="cmdFileNum" Enabled="true" Font-Bold="true" Text="Get Record" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1" class="style2">
                            <asp:Label ID="lblFileNum" Text="File Number:" runat="server"></asp:Label>
                            &nbsp;
                            </td>
                        <td align="left" colspan="1">&nbsp;
                            <asp:TextBox ID="txtFileNum" Font-Bold="true" ForeColor="Red" Width="250px" 
                                runat="server"></asp:TextBox>
                            </td>
                    </tr>

                    <tr>
                        <td align="left" colspan="2" valign="top" class="myMenu_Title">Policy Information</td>
                    </tr>

                    <tr>
                        <td align="right" colspan="1" class="style3">
                            <asp:Label ID="lblPol_Num" Text="Policy Number:" runat="server"></asp:Label>
           
                            </td>
                        <td align="left" colspan="1" class="style1">&nbsp;                     
                            <asp:TextBox ID="txtPol_Num" 
                                Font-Bold="true" Font-Size="Large" 
                                ForeColor="Red" Width="350px" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" colspan="1" class="style2">&nbsp;
                            <asp:Label ID="lblAssuredName" Text="Assured Name:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="1">&nbsp;                     
                            <asp:TextBox ID="txtAssured_Name" Enabled="False" runat="server" Width="350px" 
                                Height="22px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" colspan="1" class="style3">&nbsp;
                            <asp:Label ID="lblProduct_Num" Text="Product:" runat="server"></asp:Label>
                        </td>    
                        <td align="left" colspan="1" class="style1">&nbsp;
                            <asp:TextBox ID="txtProduct_Num" Enabled="false" Width="80px" runat="server"></asp:TextBox>&nbsp;
                            <asp:TextBox ID="txtProduct_Name" Enabled="false" Font-Bold="true" Width="260px" runat="server"></asp:TextBox>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td align="right" valign="top" class="style2">
                            <asp:Label ID="lblReportType" Text="Select document:" runat="server"></asp:Label>
           
                            </td>                           
                        <td align="left" valign="top">
                            <asp:RadioButtonList ID="rblTransType" runat="server"
                                style="display:inline">
                              <asp:ListItem Text="Policy Schedule" Value="rptAnnuityPolicySchedule"></asp:ListItem>
                              <asp:ListItem Text="Annuity Contract Letter" Value="rptRetireAnnuityContract"></asp:ListItem>
                              <asp:ListItem Text="Retirement Annuity Agreement" 
                                    Value="rptRetireAnnuityAgreement"></asp:ListItem>
                             <asp:ListItem Text="Annuity Agreemen Letter" Value="rptAnnuityAgreementLetter"></asp:ListItem>
                              <asp:ListItem Text="Annuity Notification" Value="rptAnnuityNotification"></asp:ListItem>
                              <asp:ListItem Text="Annuity Cancellation" Value="rptAnnuityCancellationLetter"></asp:ListItem>
                             
                            </asp:RadioButtonList>
                        </td>                           
                    </tr>
                    
                    <tr>
                        <td align="right" colspan="2" valign="top">
                            <asp:Button ID="CmdPrint" runat="server" Font-Bold="True" Font-Size="14pt" 
                                style="margin-left: 0px" Text="Print" Width="100px" />
                        </td>                           
                    </tr>
                    
                    <tr>
                        <td align="right" colspan="2" valign="top">&nbsp;</td>                           
                    </tr>
                    
                    <tr>
                        <td align="right" colspan="2" valign="top">&nbsp;
                            <a href="PRG_ANNTY_PROP_POLICY.aspx?menu=AN_QUOTE" class="a_sub_menu">Return to Menu</a>&nbsp;
                        </td>                           
                    </tr>
                    
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
				</table>
			</td>
        </tr>
        </table>
    </div>

<div id="div_footer" align="center">    

    <table id="tbl_footer" align="center">
        <tr>
            <td align="center" valign="top">
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td>                                                                                   
                            
                            <uc2:UC_FOOT ID="UC_FOOT1" runat="server" />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>    


    </form>
</body>
</html>
