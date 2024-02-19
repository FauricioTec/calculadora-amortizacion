namespace Model;

public class TablaAmortizacion
{
    private List<DetalleTablaAmortizacion> detalles;
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
    
    public List<List<object>> Detalles
    {
        get { var lista = new List<List<object>>();
            foreach (var detalle in detalles) lista.Add(detalle.ToList());
            return lista; }
    }

    public double TotalSaldo
    {
        get { return totalSaldo; }
    }
    
    public double TotalAmortizacion
    {
        get { return totalAmortizacion; }
    }
    
    public double TotalCuotas
    {
        get { return totalCuotas; }
    }
    
    public double TotalIntereses
    {
        get { return totalIntereses; }
    }
    
}