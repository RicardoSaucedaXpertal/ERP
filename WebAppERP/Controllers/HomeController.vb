Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        Return View()
    End Function

    Function About() As ActionResult
        ViewData("Message") = ""


        'NewChart()
        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function

    Function admonconceptosvista() As ActionResult
        ViewData("Message") = "Prueba."

        Return View()
    End Function




    Function NewChart() As JsonResult
        Dim list As List(Of Object) = New List(Of Object)()

        Dim tableData As New DataTable
        tableData.Columns.Add("FechaCaptura", GetType(String))
        tableData.Columns.Add("VentaTotal", GetType(Decimal))

        tableData.Rows.Add("2023-10-02", "475")
        tableData.Rows.Add("2023-10-03", "374")
        tableData.Rows.Add("2023-10-04", "398")
        tableData.Rows.Add("2023-10-05", "190")
        tableData.Rows.Add("2023-10-06", "830")
        tableData.Rows.Add("2023-10-07", "709")
        tableData.Rows.Add("2023-10-08", "899")

        ' Recorriendo y extrayendo cada DataColumn a List(Of Object)
        For Each dc As DataColumn In tableData.Columns
            Dim x As New List(Of Object)()
            x = (From drr In tableData.Rows Select drr(dc.ColumnName)).ToList()
            list.Add(x)
        Next
        ViewBag.reporte2 = tableData
        Return Json(list, JsonRequestBehavior.AllowGet)
    End Function
End Class
