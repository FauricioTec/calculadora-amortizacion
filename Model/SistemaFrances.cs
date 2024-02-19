namespace Model;

public class SistemaFrances : SistemaAmortizacion
{
    public TablaAmortizacion CrearTablaAmortizacion(Prestamo prestamo, double tipoCambio)
    {
        var saldoInicial = prestamo.Monto / tipoCambio;
        var tablaAmortizacion = new TablaAmortizacion(saldoInicial);
        var interesAnual = prestamo.PorcentajeInteresAnual / 100;
        var cuota = saldoInicial * interesAnual / (1 - Math.Pow(1 + interesAnual, -prestamo.Plazo));

        for (var numeroCuota = 1; numeroCuota <= prestamo.Plazo; numeroCuota++)
        {
            var intereses = saldoInicial * interesAnual;
            var amortizacion = cuota - intereses;
            tablaAmortizacion.AgregarDetalle(numeroCuota, saldoInicial, intereses, amortizacion, cuota);
            saldoInicial -= amortizacion;
        }

        return tablaAmortizacion;
    }
}