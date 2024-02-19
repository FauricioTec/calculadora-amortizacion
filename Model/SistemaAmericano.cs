namespace Model;

public class SistemaAmericano : SistemaAmortizacion
{
    public TablaAmortizacion CrearTablaAmortizacion(Prestamo prestamo, double tipoCambio)
    {
        double saldoInicial = prestamo.Monto / tipoCambio;
        TablaAmortizacion tablaAmortizacion = new TablaAmortizacion(saldoInicial);
        double interesAnual = prestamo.PorcentajeInteresAnual / 100;
        double interes = saldoInicial * interesAnual;

        for (int numeroCuota = 1; numeroCuota <= prestamo.Plazo; numeroCuota++)
            if (numeroCuota == prestamo.Plazo)
                tablaAmortizacion.AgregarDetalle(numeroCuota, saldoInicial, interes, saldoInicial,
                    saldoInicial + interes);
            else
                tablaAmortizacion.AgregarDetalle(numeroCuota, saldoInicial, interes, 0, interes);

        return tablaAmortizacion;
    }
}