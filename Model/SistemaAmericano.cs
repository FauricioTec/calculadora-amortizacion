namespace Model;

public class SistemaAmericano : SistemaAmortizacion
{
    public TablaAmortizacion CrearTablaAmortizacion(Prestamo prestamo, double tipoCambio)
    {
        var saldoInicial = prestamo.Monto / tipoCambio;
        var tablaAmortizacion = new TablaAmortizacion(saldoInicial);
        var interesAnual = prestamo.PorcentajeInteresAnual / 100;
        var interes = saldoInicial * interesAnual;

        for (var numeroCuota = 1; numeroCuota <= prestamo.Plazo; numeroCuota++)
            if (numeroCuota == prestamo.Plazo)
                tablaAmortizacion.AgregarDetalle(numeroCuota, saldoInicial, interes, saldoInicial,
                    saldoInicial + interes);
            else
                tablaAmortizacion.AgregarDetalle(numeroCuota, saldoInicial, interes, 0, interes);

        return tablaAmortizacion;
    }
}