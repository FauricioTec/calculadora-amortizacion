namespace Model;

public class SistemaFrances : SistemaAmortizacion
{
    public TablaAmortizacion CrearTablaAmortizacion(Prestamo prestamo, double tipoCambio)
    {
        double saldoInicial = prestamo.Monto / tipoCambio;
        TablaAmortizacion tablaAmortizacion = new TablaAmortizacion(saldoInicial);
        double interesAnual = prestamo.PorcentajeInteresAnual / 100;
        double cuota = saldoInicial * interesAnual / (1 - Math.Pow(1 + interesAnual, -prestamo.Plazo));

        for (int numeroCuota = 1; numeroCuota <= prestamo.Plazo; numeroCuota++)
        {
            double intereses = saldoInicial * interesAnual;
            double amortizacion = cuota - intereses;
            tablaAmortizacion.AgregarDetalle(numeroCuota, saldoInicial, intereses, amortizacion, cuota);
            saldoInicial -= amortizacion;
        }

        return tablaAmortizacion;
    }
}