<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KurumPaneli.aspx.cs" Inherits="kPanel.KurumPaneli" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Çıkış yap" />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Staj Defterlerini Görüntüle" />
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Staj Formları:"></asp:Label>
            <br />
            <br />
            <asp:DataList ID="DataList1" runat="server" OnItemCommand="DataList1_ItemCommand">
            <itemtemplate>       
        Öğrenci Adı : <strong><%#Eval("OgrenciAd")%></strong>
        TC : <strong><%#Eval("OgrenciTC")%></strong>
        Numara : <strong><%#Eval("oNO")%></strong>
        Bölüm : <strong><%#Eval("Bolum")%></strong><br>
    Baslama : <strong><%#Eval("Baslama")%></strong>
                Bitiş : <strong><%#Eval("Bitis")%></strong><br>
                Firma Adı: <strong><%#Eval("FirmaAd")%></strong>
                Adres : <strong><%#Eval("Adres")%></strong>
                İş Tanımı : <strong><%#Eval("IsTanim")%></strong>
                Telefon : <strong><%#Eval("Tel")%></strong><br>
                <asp:linkbutton id="detayButon" runat="server" text="Onayla" CommandName="Onayla" CommandArgument='<%#Eval("oID")%>'></asp:linkbutton>
                <asp:linkbutton id="Linkbutton1" runat="server" text="Reddet" CommandName="Reddet" CommandArgument='<%#Eval("oID")%>'></asp:linkbutton><br>
        </itemtemplate>
        <separatortemplate><hr></separatortemplate>
            </asp:DataList>
            <br />
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Mesajlar:"></asp:Label>
            
            <asp:DataList ID="DataList2" runat="server">
                <itemtemplate>       
        Gönderen : <strong><%#Eval("gonderenMail")%></strong>
        Mesaj: <strong><%#Eval("Mesaj")%></strong>
        
                 </itemtemplate>
        <separatortemplate><hr></separatortemplate>
       
            </asp:DataList>
        </div>
    </form>
</body>
</html>
