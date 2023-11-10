@Code
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


End Code


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
                <input id="txtBuscar" onkeydown="return (event.keyCode!=13);" onkeyup="ValidaEnterBusqueda(event, '@Url.Action("Index", "Busquedas")');return false;" type="text" class="form-control" placeholder="Ingresa tu busqueda" value="" />
                <span class="input-group-btn input-group-sm">
                    <button class="btn btn-primary btn-block" onclick="Buscar('@Url.Action("Index", "Busquedas")'); return false;"><i class="glyphicon glyphicon-search"></i></button>
                </span>
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

        <form id="frm" method="post" action="@Url.Action("IndexPost", "AdmonConceptosIE")">

            <div class="padding-top-3">
                <a class="btn btn-secondary" href="@regresaUrl"><i class="glyphicon glyphicon-chevron-left"></i> Regresar</a>
                <button class="btn btn-info" onclick="openNav()"><i class="glyphicon glyphicon-search"></i> Buscar</button>
                <a class="btn btn-primary" href="@Url.Action("Index", "AdmonConceptosIE")?nuevo=1"><i class="glyphicon glyphicon-plus"></i> Nuevo</a>
                <button id="btnGuardar" class="btn btn-success"><i class="glyphicon glyphicon-floppy-disk"></i> Guardar</button>
                @* <button id="btnEliminar" class="btn btn-danger"><i class="glyphicon glyphicon-trash"></i> Eliminar</button>*@
            </div>
            <input name="Nuevo" type="hidden" value="@ViewData("Nuevo")" />
            <input name="RegresaUrl" id="RegresaUrl" type="hidden" value="@regresaUrl" />
            <input name="NumReg" id="NumReg" type="hidden" value="@ViewData("NumReg")" />
            <div class="clearfix"></div>

            <div class="clearfix"></div>
            <br>
            <div class="col-md-6 form-horizontal">

                <div class="form-group form-group-sm">
                    <label class="control-label col-md-3">Clave Concepto Contable:</label>
                    <div class="col-md-9">
                        <input ID="txtClaveconceptoIE" name="txtClaveconceptoIE" class="ClaveC form-control" value="@ClaveconceptoIE" readonly />
                    </div>
                </div>

                <div class="form-group form-group-sm">
                    <label class="control-label col-md-3">Nombre Concepto Contable:</label>
                    <div class="col-md-9">
                        <input ID="txtNombreconceptoIE" name="txtNombreconceptoIE" class="txtNombreconceptoIE form-control" value="@NombreconceptoIE" />
                    </div>
                </div>

                <div class="form-group form-group-sm">
                    <label class="control-label col-md-3">Tipo de concepto:</label>
                    <div class="col-md-9">
                        <select ID="cboTipodeConceptoIE" name="cboTipodeConceptoIE" class="form-control">
                            <option Value="INGRESO">INGRESOS</option>
                            <option Value="EGRESO">EGRESOS</option>
                        </select>
                    </div>
                </div>

                <div class="form-group form-group-sm">
                    <label class="control-label col-md-3">Importe Concepto:</label>
                    <div class="col-md-9">
                        <input ID="txtImporteConceptoIE" name="txtImporteConceptoIE" class="txtImporteConceptoIE form-control" value="@ImporteConceptoIE" />
                    </div>
                </div>

                <div class="form-group form-group-sm">
                    <label class="control-label col-md-3">Cuenta Contable:</label>
                    <div class="col-md-9">
                        <input ID="txtCuentaContable" NAME="txtCuentaContable" class="txtCuentaContable form-control" value="@CuentaContable" />
                    </div>
                </div>

                <div class="form-group form-group-sm">
                    <label class="control-label col-md-3">Cuenta Contable:</label>
                    <div class="col-md-9">
                        <input ID="txtNombreCuentaContable" NAME="txtNombreCuentaContable" class="txtNombreCuentaContable form-control" value="@NombreCuentaContable" />
                    </div>
                </div>


            </div>


        </form>
    </div>
</div>


<div class="clearfix"></div>


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
                            <button class="btn btn-sm btn-danger" type="button" onclick="EliminaRegistro('@row("claveconceptoie")')"><i class="glyphicon glyphicon-trash"></i></button>
                        </td>
                        <td>
                            <a class="btn btn-sm btn-info" href="@Url.Action("Index", "AdmonConceptosIE", New With {.clave = row("claveconceptoie")})"><i class="glyphicon glyphicon-edit"></i></a>
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

<div class="clearfix"></div>



<Style>
    #mySidenav {
        color: white;
    }
</Style>



@section head
    @Styles.Render("~/Content/datatablescss")
    @Styles.Render("~/Content/jqueryuicss")
    <script src="~/Content/comunes-busquedas-mvc.js"></script>
    <script src="~/Content/comunes-importes.js"></script>

    <style>
        .reporte-resultados {
            margin-top: 30px;
        }
    </style>
End Section

@*********FINALIZA MOSTRAR REPORTE*@



@section scripts
    @*<script src="~/Scripts/jquery-3.3.1.js"></script>*@
    <script src="~/Content/comunes-busquedas-mvc.js"></script>
    <script src="~/Content/comunes-importes.js"></script>

    @Scripts.Render("~/bundles/datatables")
    @Scripts.Render("~/bundles/jqueryui")



    <script>
        //$('#datepicker2').datepicker({
        //    uiLibrary: 'bootstrap'
        //});
    </script>

    <script>

        function EliminaRegistro(Clave) {
            if (ConfirmaEliminar()) {
                window.location = '@Url.Action("EliminaRegistro", "AdmonConceptosIE")?clave='+ Clave;
            }
        }
    </script>




    @*codigo para onfocus*@
    <script type="text/javascript">

        $(function () {
            var focusedElement;
            $(document).on('focus', 'input', function () {
                if (focusedElement == this) return; //already focused, return so user can now place cursor at specific point in input.
                focusedElement = this;
                setTimeout(function () { focusedElement.select(); }, 100); //select all text in any field on focus for easy re-entry. Delay sightly to allow focus to "stick" before selecting.
            });
        });


    </script>


    @*<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.js"></script>*@
    <script type="text/javascript">
        $(function () {
            $(document).on('click', 'input[type=text]', function () { this.select(); });
        });
    </script>
    @*fin codigo para onfocus*@


End Section