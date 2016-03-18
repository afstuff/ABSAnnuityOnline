<%@ page language="VB" autoeventwireup="false" inherits="PRG_ANN_PFA_DTL, App_Web_qkk2jm9h" %>
<%@ Register src="UC_BANX.ascx" tagname="UC_BANX" tagprefix="uc1" %>
<%@ Register src="UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PFA Details Setup Page</title>
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
   <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
   </script>
    <style type="text/css">
        .style2
        {
            width: 82px;
        }
        .style3
        {
            width: 142px;
        }
        .style4
        {
            width: 93px;
        }
        .auto-style1 {
            width: 104px;
        }
    </style>
</head>
<body onload="<%= FirstMsg %>">
    <form id="Form1" name="Form1" runat="server">

    <!-- start banner -->
    <div id="div_banner" align="center">                      
        
        <uc1:UC_BANX ID="UC_BANX1" runat="server" />
        
    </div>


    <!-- content -->
    <div id="div_content" align="center">

        <table id="tbl_content" align="center" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" colspan="4" class="tbl_buttons">
                    <table align="center" border="1">
                        <tr>
                            <td align="left" colspan="2" valign="baseline"><asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP()"></asp:button>
                                &nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
                                &nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt" runat="server" text="Delete Data" OnClientClick="JSDelete_ASP()"></asp:button>
                                &nbsp;<asp:Button ID="cmdPrint_ASP" Enabled="false" CssClass="cmd_butt" Text="Print" runat="server" />
                                &nbsp;&nbsp;Status:
                                &nbsp;<asp:textbox id="txtAction" Visible="true" runat="server" EnableViewState="False" Width="50px"></asp:textbox>&nbsp;
                                <!-- <A HREF="<%= request.ApplicationPath %>/Setup/UCD001.aspx" TARGET="fraDetails">Previous Menu</A>&nbsp;| -->
                                <!-- &nbsp;|&nbsp;<a href="javascript:window.close()" style="font-weight:bold;">Close Page</a>&nbsp;| -->
    	                    </td>
    	                    <td align="right" colspan="2" valign="baseline">Find:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}"></input>&nbsp;
                                <asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                    </td>
                        </tr>
                    
                    </table>
                </td>
            </tr>


            <tr>
                <td align="center" colspan="4" valign="top" class="td_menu">
                    <table align="center" border="0" cellpadding="1" cellspacing="1"  class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="5" class="myMenu_Title"><%=STRPAGE_TITLE%></td>
                        </tr>
                	    <tr>
                	        <td align="left" nowrap colspan="4"><asp:Label id="lblMessage" Text="Staus:" runat="server" Font-Size="Small" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                &nbsp;<a id="PageAnchor_Return_Link" runat="server" class="a_return_menu" href="#" >Returns to Previous Page</a>
                                &nbsp;<%=PageLinks%>&nbsp;
                                <%--onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=il_code_cust')"--%>
                            </td>
    	               	</tr>

                    <tr>
                    <td align="center" colspan="5" valign="top">
                               <%-- <asp:GridView id="GridView2" CellPadding="2" runat="server" CssClass="grd_ctrl"
                                    DataKeyNames="TBIL_PFA_REC_ID" HorizontalAlign="Left"
                                    AutoGenerateColumns="False" AllowPaging="True" 
                            AllowSorting="false" PageSize="10"
                                    PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                    PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next"
                                    PagerSettings-PreviousPageText="Previous" PagerSettings-LastPageText="Last"
                                    EmptyDataText="No data available..."
                                    GridLines="Both" ShowFooter="True" Visible="False">                        

                        
                                    <PagerStyle CssClass="grd_page_style" />
                                    <HeaderStyle CssClass="grd_header_style" />
                                    <RowStyle CssClass="grd_row_style" />
                                    <SelectedRowStyle CssClass="grd_selrow_style" />
                                    <EditRowStyle CssClass="grd_editrow_style" />
                                    <AlternatingRowStyle CssClass="grd_altrow_style" />
                                    <FooterStyle CssClass="grd_footer_style" />
                    
                                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom" 
                                        PreviousPageText="Previous">
                                    </PagerSettings>
                        
                                    <Columns>
                                        <asp:TemplateField>
        			                        <ItemTemplate>
        						                <asp:CheckBox id="chkSel0" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            
                                        <asp:CommandField ShowSelectButton="True" />
                            
                                        <asp:BoundField readonly="true" DataField="TBIL_PFA_REC_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <%--<asp:BoundField readonly="true" DataField="TBIL_PFA_ID" HeaderText="Ref.ID" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />--%>
                                        <%--<asp:BoundField readonly="true" DataField="TBIL_PFA_CODE" HeaderText="Customer Code" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_PFA_FULL_NAME" HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_PFA_PHONE_NUM" HeaderText="Phone No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                    </Columns>
   
                                </asp:GridView>--%>
                    <table align="center" style="background-color: White; width: 95%;">
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <asp:GridView id="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl"
                                    DataKeyNames="TBIL_PFA_REC_ID" HorizontalAlign="Left"
                                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="false" PageSize="10"
                                    PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                    PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next"
                                    PagerSettings-PreviousPageText="Previous" PagerSettings-LastPageText="Last"
                                    EmptyDataText="No data available..."
                                    GridLines="Both" ShowFooter="True">                        

                        
                                    <PagerStyle CssClass="grd_page_style" />
                                    <HeaderStyle CssClass="grd_header_style" />
                                    <RowStyle CssClass="grd_row_style" />
                                    <SelectedRowStyle CssClass="grd_selrow_style" />
                                    <EditRowStyle CssClass="grd_editrow_style" />
                                    <AlternatingRowStyle CssClass="grd_altrow_style" />
                                    <FooterStyle CssClass="grd_footer_style" />
                    
                                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom" 
                                        PreviousPageText="Previous">
                                    </PagerSettings>
                        
                                    <Columns>
                                        <asp:TemplateField>
        			                        <ItemTemplate>
        						                <asp:CheckBox id="chkSel" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            
                                        <asp:CommandField ShowSelectButton="True" />
                            
                                        <asp:BoundField readonly="true" DataField="TBIL_PFA_REC_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <%--<asp:BoundField readonly="true" DataField="TBIL_PFA_ID" HeaderText="Ref.ID" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />--%>
                                        <asp:BoundField readonly="true" DataField="TBIL_PFA_CODE" HeaderText="PFA Code" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_PFA_FULL_NAME" HeaderText="PFA Name" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_PFA_PHONE_NUM" HeaderText="Phone No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                    </Columns>
   
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    </td>
                    </tr>

                    <tr>
                        <td colspan="5"><hr /></td>
                    </tr>


                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblCustID" Enabled="false" Text="Record ID:" runat="server"></asp:Label>&nbsp;</td>
    	                    <td align="left" nowrap><asp:textbox id="txtPfaID" Enabled="false" 
                                    MaxLength="3" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
            	    	        </td>
            	    	    <td align="left" nowrap class="style2"><asp:Label ID="lblTBIL_PFA_CODE" 
                                    Enabled="False" Text="PFA Code:" runat="server"></asp:Label>
                            </td>
            	    	    <td align="left" nowrap class="auto-style1">
                                <asp:TextBox ID="txtTBIL_PFA_CODE" runat="server"></asp:TextBox>
                            </td>
        	        	</tr>


                		<tr>
    	                    <td align="right" nowrap>
                                <asp:Label ID="lblTBIL_PFA_CATG" 
                                    Enabled="False" Text="PFA Category:" runat="server"></asp:Label></td>
    	                    <td align="left" nowrap>
                                <asp:TextBox ID="txtTBIL_PFA_CATG" runat="server" Width="250px"></asp:TextBox>
                            </td>
            	    	    <td align="left" nowrap>
                                <asp:Label ID="lblTBIL_PFA_MDLE" 
                                    Enabled="False" Text="PFA Module:" runat="server"></asp:Label></td>
            	    	    <td align="left" nowrap class="auto-style1">
                                <asp:TextBox ID="txtTBIL_PFA_MDLE" runat="server" Width="250px"></asp:TextBox>
                            </td>
        	        	</tr>


                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblTBIL_PFA_DESC" 
                                    Enabled="False" Text="PFA Description:" runat="server"></asp:Label></td>
    	                    <td align="left" nowrap>
                                <asp:TextBox ID="txtTBIL_PFA_DESC" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
            	    	    <td align="left" nowrap valign="top">
                                <asp:Label ID="lblTBIL_PFA_DESC0" 
                                    Enabled="False" Text="PFA Short Description:" runat="server"></asp:Label></td>
            	    	    <td align="left" nowrap valign="top" class="auto-style1">
                                <asp:TextBox ID="txtTBIL_PFA_SHRT_DESC" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
        	        	</tr>


                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblTBIL_PFA_BRANCH" 
                                    Enabled="False" Text="Branch:" runat="server"></asp:Label></td>
    	                    <td align="left" nowrap>
                                <asp:TextBox ID="txtTBIL_PFA_BRANCH" runat="server" Width="250px"></asp:TextBox>
                            </td>
            	    	    <td align="left" nowrap colspan="2">
                                &nbsp;</td>
        	        	</tr>


                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblTBIL_PFA_BRANCH0" 
                                    Enabled="False" Text="Address 1:" runat="server"></asp:Label></td>
    	                    <td align="left" nowrap>
                                <asp:TextBox ID="txtTBIL_PFA_ADRES1" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
            	    	    <td align="left" nowrap>
                                <asp:Label ID="lblTBIL_PFA_BRANCH1" 
                                    Enabled="False" Text="Address 2:" runat="server"></asp:Label></td>
            	    	    <td align="left" nowrap class="auto-style1">
                                <asp:TextBox ID="txtTBIL_PFA_ADRES2" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
        	        	</tr>


                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblTBIL_PFA_BRANCH2" Enabled="False" 
                                    Text="Phone 1:" runat="server"></asp:Label></td>
    	                    <td align="left" nowrap>
                                <asp:TextBox ID="txtTBIL_PFA_PHONE1" runat="server" Width="250px"></asp:TextBox>
                            </td>
            	    	    <td align="left" nowrap><asp:Label ID="lblTBIL_PFA_BRANCH3" Enabled="False" 
                                    Text="Phone 1:" runat="server"></asp:Label>
                            </td>
            	    	    <td align="left" nowrap class="auto-style1">
                                <asp:TextBox ID="txtTBIL_PFA_PHONE2" runat="server" Width="250px"></asp:TextBox>
                            </td>
        	        	</tr>


                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblTBIL_PFA_BRANCH4" Enabled="False" 
                                    Text="Email 1:" runat="server"></asp:Label></td>
    	                    <td align="left" nowrap>
                                <asp:TextBox ID="txtTBIL_PFA_EMAIL1" runat="server" Width="250px"></asp:TextBox>
                            </td>
            	    	    <td align="left" nowrap><asp:Label ID="lblTBIL_PFA_BRANCH5" Enabled="False" 
                                    Text="Email 2:" runat="server"></asp:Label>
                            </td>
            	    	    <td align="left" nowrap class="auto-style1">
                                <asp:TextBox ID="txtTBIL_PFA_EMAIL2" runat="server" Width="250px"></asp:TextBox>
                            </td>
        	        	</tr>


                		<tr>
    	                    <td align="right" nowrap colspan="2">&nbsp;</td>
            	    	    <td align="left" nowrap colspan="2">&nbsp;</td>
        	        	</tr>


                    </table>
                
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>

        </table>
    
    </div>


<!-- footer -->
<div id="div_footer" align="center">

    <table id="tbl_footer" align="center">
        <tr>
            <td valign="top">
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
