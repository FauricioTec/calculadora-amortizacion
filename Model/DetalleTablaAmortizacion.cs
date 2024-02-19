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

    public List<string> ToList()
    {
        var lista = new List<string>();
        lista.Add(periodo.ToString());
        lista.Add(saldo.ToString("N2")); // Dos decimales
        lista.Add(intereses.ToString("N2")); // Dos decimales
        lista.Add(amortizacion.ToString("N2")); // Dos decimales
        lista.Add(cuota.ToString("N2")); // Dos decimales
        return lista;
    }
}