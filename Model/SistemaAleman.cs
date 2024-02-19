namespace Model;

public class SistemaAleman : SistemaAmortizacion
{
    public TablaAmortizacion CrearTablaAmortizacion(Prestamo prestamo, double tipoCambio)
    {
        var saldoInicial = prestamo.Monto / tipoCambio;
        var tablaAmortizacion = new TablaAmortizacion(saldoInicial);
        var saldo = saldoInicial;
        var interesAnual = prestamo.PorcentajeInteresAnual / 100;
        var cuota = saldoInicial / prestamo.Plazo + saldoInicial * interesAnual;
        for (var i = 1; i <= prestamo.Plazo; i++)
        {
            var intereses = (prestamo.Plazo - i + 1) * (interesAnual * saldoInicial / prestamo.Plazo);
            var amortizacion = saldoInicial / prestamo.Plazo;
            tablaAmortizacion.AgregarDetalle(i, saldo, intereses, amortizacion, cuota);
            cuota -= interesAnual * saldoInicial / prestamo.Plazo;
            saldo -= amortizacion;
        }

        return tablaAmortizacion;
    }
}