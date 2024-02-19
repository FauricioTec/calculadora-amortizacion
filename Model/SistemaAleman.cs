namespace Model;

public class SistemaAleman : SistemaAmortizacion
{
    public TablaAmortizacion CrearTablaAmortizacion(Prestamo prestamo, double tipoCambio)
    {
        double saldoInicial = prestamo.Monto / tipoCambio;
        TablaAmortizacion tablaAmortizacion = new TablaAmortizacion(saldoInicial);
        double saldo = saldoInicial;
        double interesAnual = prestamo.PorcentajeInteresAnual / 100;
        double cuota = saldoInicial / prestamo.Plazo + saldoInicial * interesAnual;
        for (int i = 1; i <= prestamo.Plazo; i++)
        {
            double intereses = (prestamo.Plazo - i + 1) * (interesAnual * saldoInicial / prestamo.Plazo);
            double amortizacion = saldoInicial / prestamo.Plazo;
            tablaAmortizacion.AgregarDetalle(i, saldo, intereses, amortizacion, cuota);
            cuota -= interesAnual * saldoInicial / prestamo.Plazo;
            saldo -= amortizacion;
        }

        return tablaAmortizacion;
    }
}