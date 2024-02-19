namespace Model;

public class TablaAmortizacion
{
    private readonly List<DetalleTablaAmortizacion> detalles;
    private double totalAmortizacion;
    private double totalCuotas;
    private double totalIntereses;
    private double totalSaldo;

    public TablaAmortizacion(double totalSaldo)
    {
        detalles = new List<DetalleTablaAmortizacion>();
        this.totalSaldo = totalSaldo;
        totalIntereses = 0;
        totalAmortizacion = 0;
        totalCuotas = 0;
        Fecha = DateTime.Now;
    }

    public DateTime Fecha { get; set; }

    public void AgregarDetalle(int periodo, double saldoInicial, double intereses, double amortizacion,
        double cuota)
    {
        var detalle =
            new DetalleTablaAmortizacion(periodo, saldoInicial, intereses, amortizacion, cuota);
        detalles.Add(detalle);
        totalSaldo -= amortizacion;
        totalIntereses += intereses;
        totalAmortizacion += amortizacion;
        totalCuotas += cuota;
    }

    public List<List<string>> ToList()
    {
        var lista = new List<List<string>>();
        foreach (var detalle in detalles) lista.Add(detalle.ToList());

        lista.Add(new List<string>
        {
            "Total", totalSaldo.ToString("N2"), totalIntereses.ToString("N2"),
            totalAmortizacion.ToString("N2"), totalCuotas.ToString("N2")
        });
        return lista;
    }

    public List<List<string>> GetDetalles()
    {
        var lista = new List<List<string>>();
        foreach (var detalle in detalles) lista.Add(detalle.ToList());

        return lista;
    }
}