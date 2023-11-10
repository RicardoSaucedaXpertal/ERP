@Code
    'ViewData("Title") = "About"

    ViewData("Title") = "Comun / Conceptos Contables"
    'Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim nuevo = String.Empty
    If (ViewData("Nuevo") IsNot Nothing) Then
        nuevo = ViewData("Nuevo")
    End If


    Dim ClaveconceptoIE, NombreconceptoIE, ImporteConceptoIE, CuentaContable, NombreCuentaContable As String

    ClaveconceptoIE = "" : NombreconceptoIE = "" : ImporteConceptoIE = 0 : CuentaContable = "" : NombreCuentaContable = ""
    If (ViewBag.Registro Is Nothing = False) Then
        Dim registro As Data.DataRow = ViewBag.Registro
        'ClaveconceptoIE = IIf(registro("ClaveconceptoIE") IsNot DBNull.Value, registro("ClaveconceptoIE"), "")
        ClaveconceptoIE = IIf(registro("ClaveconceptoIE") IsNot DBNull.Value, registro("ClaveconceptoIE"), 0)
        NombreconceptoIE = IIf(registro("NombreconceptoIE") IsNot DBNull.Value, registro("NombreconceptoIE"), "")
        CuentaContable = IIf(registro("cuentacontable") IsNot DBNull.Value, registro("cuentacontable"), "")
        NombreCuentaContable = IIf(registro("nombrecuentacontable") IsNot DBNull.Value, registro("nombrecuentacontable"), "")

        ImporteConceptoIE = IIf(registro("ImporteConceptoIE") IsNot DBNull.Value, registro("ImporteConceptoIE"), "")
    Else
        If (ViewData("ClaveconceptoIE") IsNot Nothing) Then
            ClaveconceptoIE = ViewData("ClaveconceptoIE")
        End If
    End If


    Dim errorMsg = String.Empty
    If (ViewData("Error") IsNot Nothing) Then
        errorMsg = ViewData("Error")
    End If




    Dim reporteDS As System.Data.DataTable = Nothing
    If ViewBag.Reporte IsNot Nothing Then
        reporteDS = ViewBag.Reporte
    End If

    Dim regresaUrl = String.Empty
    If (ViewData("RegresaUrl") IsNot Nothing) Then
        regresaUrl = ViewData("RegresaUrl")
    Else
        regresaUrl = "~/Forms/Index.aspx"
    End If


    Dim infoData As New System.Data.DataTable
    infoData.Columns.Add("FechaCaptura", GetType(String))
    infoData.Columns.Add("VentaTotal", GetType(Decimal))

    infoData.Rows.Add("2023-10-02", "475")
    infoData.Rows.Add("2023-10-03", "374")
    infoData.Rows.Add("2023-10-04", "398")
    infoData.Rows.Add("2023-10-05", "190")
    infoData.Rows.Add("2023-10-06", "830")
    infoData.Rows.Add("2023-10-07", "709")
    infoData.Rows.Add("2023-10-08", "899")
End Code

<h2>@ViewData("Title").</h2>
<h3>@ViewData("Message")</h3>

<div id="mySidenav" class="sidenav">
    <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>

    <div id="busqueda" class="padding-left-1 padding-right-1">
        <div class="form-inline" style="padding-bottom:10px;">
            <!-- params -->
            <input id="txtPaginaConsulta" value="@Url.Action("Index", "AdmonConceptosIE")" type="hidden">
            <!-- EDITABLE -->
            <input id="txtClaveBusqueda" value="4102" type="hidden">
            <!-- EDITABLE -->
            <div class="input-group" style="width:100%;">
                @*<input id="txtBuscar" onkeydown="return (event.keyCode!=13);" onkeyup="ValidaEnterBusqueda(event, '@Url.Action("Index", "Busquedas")');return false;" type="text" class="form-control" placeholder="Ingresa tu busqueda" value="" />
                    <span class="input-group-btn input-group-sm">
                        <button class="btn btn-primary btn-block" onclick="Buscar('@Url.Action("Index", "Busquedas")'); return false;"><i class="glyphicon glyphicon-search"></i></button>
                    </span>*@
            </div>
        </div>
        <div id="busqueda-contenido" class="container" style=""></div>
    </div>
</div>

<div class="container">
    <div class="row" style="margin-top:40px; margin-bottom:40px;">
        <h2>@ViewData("Title")</h2>

        <div class="clearfix"></div>
        <br />

        @If ViewData("Error") IsNot Nothing Then
            If ViewData("Error") Then
                @<div class="alert alert-danger alert-dismissible" role="alert">
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <strong>Error:</strong> @ViewData("ErrorMensaje")
                </div>
            End If
        End If

       
    </div>
</div>


<div class="clearfix"></div>

<button class="btn btn-info" onclick="callServerMethod()"><i class="glyphicon glyphicon-search"></i> Buscar</button>

<div class="reporte-resultados">
    <h4>Ultimos Registros Capturados</h4>
    <table id="table1" class="table table-striped table-bordered">

        @If reporteDS IsNot Nothing Then
            @<thead>
                <tr>
                    @For each col As Data.DataColumn In reporteDS.Columns
                        @<th> @col.ColumnName</th>
                    Next
                    <th>  </th>
                    <th>  </th>
                </tr>
            </thead>

            @<tbody>
                @For Each row As Data.DataRow In reporteDS.Rows

                    @code
                        'totalNeto += row("TotalNeto")
                    End Code

                    @<tr>
                        @For each col As Data.DataColumn In reporteDS.Columns
                            @If (col.DataType = GetType(DateTime)) Then
                                Dim val = DirectCast(row(col.ColumnName), DateTime)
                                @<td>@val.ToString("yyyy/MM/dd")</td>
                            Else
                                @<td>@row(col.ColumnName)</td>
                            End If

                            @If (col.ColumnName = "TotalNeto") Then
                                'Dim val = DirectCast(row(col.ColumnName), DateTime)
                                'totalNeto += row("TotalNeto")
                            End If

                        Next
                        <td>
                            @*  <button class="btn btn-sm btn-danger" type="button" onclick="EliminaRegistro('@row("claveconceptoie")')"><i class="glyphicon glyphicon-trash"></i></button>*@
                        </td>
                        <td>
                            @*  <a class="btn btn-sm btn-info" href="@Url.Action("Index", "AdmonConceptosIE", New With {.clave = row("claveconceptoie")})"><i class="glyphicon glyphicon-edit"></i></a>*@
                        </td>
                    </tr>
                Next

            </tbody>
        Else
            @<thead style="display:none"><tr><th></th></tr></thead>
        End If
    </table>
    <div>
        @*Total Ventas   : @totalNeto.ToString("C2")*@
    </div>
</div>

<nav>
    <div class="nav nav-tabs" id="nav-tab" role="tablist">

        <button class="nav-link active" id="nav-CxC-tab" data-bs-toggle="tab"
                data-bs-target="#nav-CxC"
                type="button" role="tab"
                aria-controls="nav-CxC"
                aria-selected="true">
            Detalle
        </button>
        <button class="nav-link" id="nav-PagosCliente-tab" data-bs-toggle="tab"
                data-bs-target="#nav-PagosCliente" type="button" role="tab"
                aria-controls="nav-PagosCliente" aria-selected="false">
            Grafica
        </button>
    </div>
</nav>
<hr />

<div class="tab-content" id="nav-tabContent">
    <!-- div contenedor de paneles -->
    <!----- Lineas -------------->
    <div class="clearfix"></div>
    <div class="tab-pane fade show active" id="nav-CxC" role="tabpanel" aria-labelledby="nav-CxC-tab">
        <div class="col-sm-12 row">
            <div role="tabpanel" class=" tab-pane  active in " id="CobranzaCxC">

                <table id="table2" class="table table-striped table-bordered">

                    <thead>
                        <tr>
                            @For Each col As Data.DataColumn In infoData.Columns
                                @<th> @col.ColumnName</th>
                            Next
                            <th>  </th>
                            <th>  </th>
                        </tr>
                    </thead>


                    <tbody>
                        @For Each row As Data.DataRow In infoData.Rows

                            @code
                                'totalNeto += row("TotalNeto")
                            End Code

                            @<tr>
                                @For each col As Data.DataColumn In infoData.Columns
                                    @If (col.DataType = GetType(DateTime)) Then
                                        Dim val = DirectCast(row(col.ColumnName), DateTime)
                                        @<td>@val.ToString("yyyy/MM/dd")</td>
                                    Else
                                        @<td>@row(col.ColumnName)</td>
                                    End If

                                    @If (col.ColumnName = "TotalNeto") Then
                                    End If

                                Next
                                <td>
                                    @*<button class="btn btn-sm btn-danger" type="button" onclick="EliminaRegistro('1')"><i class="glyphicon glyphicon-trash"></i></button>*@
                                </td>
                                <td>
                                    @* <a class="btn btn-sm btn-info" href="@Url.Action("Index", "AdmonConceptosIE", New With {.clave = row("claveconceptoie")})"><i class="glyphicon glyphicon-edit"></i></a>*@
                                </td>
                            </tr>
                        Next

                    </tbody>


                    @*Else*@

                    <thead style="display:none"><tr><th></th></tr></thead>
                    @*End If*@
                </table>


            </div>
        </div>
    </div>

    <div class="clearfix"></div>
    <div class="tab-pane fade " id="nav-PagosCliente" role="tabpanel" aria-labelledby="nav-PagosCliente-tab">
        <div role="tabpanel" class="tab-pane  " id="PagosDelCliente">
            <div class="panel-body">
                <div class="container">
                    <div class="row my-2">
                        <div class="col-md-12 py-1">
                            <div class="card">
                                <div class="card-body">
                                    <canvas id="chBar"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</div>

<hr />

<div class="clearfix"></div>



@section scripts

    @*<script src="js/bootstrap.bundle.min.js"></script>*@
    @*<script src="~/Scripts/jquery-1.10.2.js"></script>*@
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/5.0.0-alpha1/js/bootstrap.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.3/Chart.js"></script>

    <script type="text/javascript">

        // chart colors
        var colors = ['#007bff', '#28a745', '#333333', '#c3e6cb', '#dc3545', '#6c757d'];

        function callServerMethod() {

            $.ajax({
                type: "POST",
                url: "/Home/NewChart",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (chData) {
                    var aData = chData;
                    var aLabels = aData[0];
                    var aDatasets1 = aData[1];
                    
                    
                    var chBar = document.getElementById("chBar");
                    if (chBar) {

                        new Chart(chBar, {
                            type: 'bar',
                            data: {
                                labels: aLabels,
                                datasets: [{
                                    label: "Venta total diaria",
                                    data: aDatasets1,
                                    backgroundColor: colors[0],
                                    width: 4,
                                    barPercentage: .75,
                                    categoryPercentage: .5,
                                },
                                ],

                            },
                            options: {
                                plugins: {
                                    legend: { position: 'top' },
                                    title: {
                                        display: true,
                                        text: 'Custom Chart Title',
                                        padding: {
                                            top: 10,
                                            bottom: 30
                                        }
                                    }
                                },
                                // legend: {
                                //   display: false
                                // },
                                scales: {
                                    xAxes: [{
                                        barPercentage: 0.4,
                                        categoryPercentage: 0.5
                                    }]
                                }
                            }
                        });
                    }

                    @*//@infoData = @ViewBag.reporte2;*@
                    console.log(@ViewBag.reporte);
                    console.log(@infoData);
                    console.log(chData);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }

        

        

        /* large line chart */
        var chLine = document.getElementById("chLine");

        if (chLine) {
            new Chart(chLine, {
                type: 'line',
                data: chartData,
                options: {
                    scales: {
                        xAxes: [{
                            ticks: {
                                beginAtZero: false
                            }
                        }]
                    },
                    legend: {
                        display: false
                    },
                    responsive: true
                }
            });
        }



        
    </script>
End Section