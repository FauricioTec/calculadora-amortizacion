namespace Model;

public class DetalleTablaAmortizacion
{
    private readonly double amortizacion;
    private readonly double cuota;
    private readonly double intereses;
    private readonly int periodo;
    private readonly double saldo;

    public DetalleTablaAmortizacion(int periodo, double saldo, double intereses, double amortizacion, double cuota)
    {
        this.periodo = periodo;
        this.saldo = saldo;
        this.intereses = intereses;
        this.amortizacion = amortizacion;
        this.cuota = cuota;
    }

    public List<object> ToList()
    {
        return new List<object>
        {
            periodo, saldo.ToString("N2"), intereses.ToString("N2"), amortizacion.ToString("N2"), cuota.ToString("N2")
        };
    }
}